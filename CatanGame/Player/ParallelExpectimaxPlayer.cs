using CatanGame.Action;
using CatanGame.Evaluator;
using CatanGame.Judge;
using CatanGame.System;

namespace CatanGame.Player;

public class ParallelExpectimaxPlayer: IPlayer
{
    private const int MaxDepth = 3;
    
    private readonly IJudge _judge;
    private readonly IEvaluator _evaluator;
    private readonly ISystem _system;

    public ParallelExpectimaxPlayer(IJudge judge, IEvaluator evaluator, ISystem system)
    {
        _judge = judge;
        _evaluator = evaluator;
        _system = system;
    }

    public void Play()
    {
        var action = Expectimax(MaxDepth, _judge.GetActions, out _);
        if (action == null)
        {
            return;
        }
        action.Do();
        action.Show();
    }

    public void PlayStartTurn()
    {
        var action = Expectimax(MaxDepth, _judge.GetFirstSettlementsActions, out _);
        if (action == null)
        {
            throw new Exception("There should be plenty of options for the first turns!");
        }
        action.Do();
        action.Show();
        
        action = Expectimax(MaxDepth, _judge.GetFirstRoadsActions, out _);
        if (action == null)
        {
            throw new Exception("There should be plenty of options for the first turns!");
        }
        action.Do();
        action.Show();
    }

    private IAction? Expectimax(int depth, Func<List<IAction>> getActions, out decimal bestValue)
    {
        bestValue = -1000m;
        IAction? bestAction = null;
        if (depth == 0)
        {
            bestValue = _evaluator.Evaluate();
            return bestAction;
        }

        var possibleActions = getActions();
        if (possibleActions.Count == 0)
        {
            bestValue = _evaluator.Evaluate();
            return bestAction;
        }

        // Use a list to store the results of parallel tasks
        var actionsExpectations = new List<(IAction Action, decimal Expectation)>();

        Parallel.ForEach(possibleActions, action =>
        {
            var changedSystem = action.Do();
            
            var diceRollsExpectations = new List<(int diceRoll, decimal Expectation)>();
            Parallel.For(2, 13, diceRoll =>
            {
                decimal expectation = 0;
                var systemCopy = SystemFactory.CopySystem(changedSystem)!;
                systemCopy.SetDiceRoll(diceRoll);
                var judgeCopyGetActions = JudgeFactory.CopyParallelJudgeChangeSystem(_judge, systemCopy)!.GetActions;
                Expectimax(
                    depth - 1, 
                    judgeCopyGetActions, 
                    out var childValue);

                expectation += childValue * Utils.Utils.Probability(diceRoll);
                diceRollsExpectations.Add((diceRoll, expectation));
            });
            
            action.Undo();

            actionsExpectations.Add((action, diceRollsExpectations
                .Sum(pair => pair.Expectation)));
        });

        // Find the best result outside the parallel loop
        var bestResult = actionsExpectations
            .OrderByDescending(r => r.Expectation)
            .FirstOrDefault();
        if (bestResult == default)
        {
            return bestAction;
        }
        bestAction = bestResult.Action;
        bestValue = bestResult.Expectation;

        return bestAction;
    }
}
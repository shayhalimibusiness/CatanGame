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
        var action = Expectimax(MaxDepth, _judge.GetActions, out _, _system);
        if (action == null)
        {
            return;
        }
        action.Do();
        action.Show();
    }

    public void PlayStartTurn()
    {
        var action = Expectimax(MaxDepth, _judge.GetFirstSettlementsActions, out _, _system);
        if (action == null)
        {
            throw new Exception("There should be plenty of options for the first turns!");
        }
        action.Do();
        action.Show();
        
        action = Expectimax(MaxDepth, _judge.GetFirstRoadsActions, out _, _system);
        if (action == null)
        {
            throw new Exception("There should be plenty of options for the first turns!");
        }
        action.Do();
        action.Show();
    }

    private IAction? Expectimax(int depth, Func<List<IAction>> getActions, out decimal bestValue, ISystem system)
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
            return bestAction;
        }

        foreach (var action in possibleActions)
        {
            decimal expectation = 0;
            action.Do();

            for (var diceRoll = 2; diceRoll <= 12; diceRoll++)
            {
                var systemCopy = SystemFactory.CopySystem(system)!;
                systemCopy.SetDiceRoll(diceRoll);
                Expectimax(
                    depth - 1, 
                    JudgeFactory.CopyJudgeChangeSystem(_judge, systemCopy)!.GetActions, 
                    out var childValue,
                    systemCopy);
                expectation += childValue * Utils.Utils.Probability(diceRoll);
            }

            if (expectation <= bestValue)
            {
                continue;
            }
            
            bestValue = expectation;
            bestAction = action;
        }

        return bestAction;
    }
}
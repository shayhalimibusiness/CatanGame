using CatanGame.Action;
using CatanGame.Enums;
using CatanGame.Evaluator;
using CatanGame.Judge;
using CatanGame.System;
using CatanGame.Utils;

namespace CatanGame.Player;

public class ParallelMinMaxExpectancyPlayer : IPlayer
{
    private const int MaxDepth = 3;
    
    private readonly IJudge _judge;
    private readonly IEvaluator _evaluator;
    private readonly ISystem _system;

    public ParallelMinMaxExpectancyPlayer(IJudge judge, IEvaluator evaluator, ISystem system)
    {
        _judge = judge;
        _evaluator = evaluator;
        _system = system;
    }

    public void Play()
    {
        var result = ParallelMinMaxExpectancy(_system, MaxDepth, EGameStatus.NormalRound);
        if (result == null)
        {
            return;
        }
        result.Action!.Do(_system);
        result.Action.Show();
    }

    public void PlayStartTurn()
    {
        var firstTurn = _system.GetCards(EPlayer.Player1).GetTotalPoints() == 0;
        var status = firstTurn ? EGameStatus.FirstRoundSettlement : EGameStatus.SecondRoundSettlement;
        var result = ParallelMinMaxExpectancy(_system, MaxDepth, status);
        if (result == null)
        {
            throw new Exception("There should be plenty of options for the first turns!");
        }
        result.Action!.Do(_system);
        result.Action.Show();
        
        status = firstTurn ? EGameStatus.FirstRoundRoad : EGameStatus.SecondRoundRoad;
        result = ParallelMinMaxExpectancy(_system, MaxDepth, status);
        if (result == null)
        {
            throw new Exception("There should be plenty of options for the first turns!");
        }
        result.Action!.Do(_system);
        result.Action.Show();
    }

    private ParallelMinMaxExpectancyResult ParallelMinMaxExpectancy(ISystem system, int depth, EGameStatus status)
    {
        IAction? bestAction = null;
        if (depth == 0)
        {
            return new ParallelMinMaxExpectancyResult
            {
                Action = bestAction,
                Value = _evaluator.EvaluateSystem(system)
            };
        }

        var judge = new ParallelJudge(
            system: system,
            ui: UiFactory.CreateUi(), 
            EPlayer.Player1);

        var possibleActions = new List<IAction>();
        switch (status)
        {
            case EGameStatus.NormalRound:
                possibleActions = judge.GetActions();
                break;
            case EGameStatus.FirstRoundRoad:
            case EGameStatus.SecondRoundRoad:
                possibleActions = judge.GetFirstRoadsActions();
                break;
            case EGameStatus.FirstRoundSettlement:
            case EGameStatus.SecondRoundSettlement:
                possibleActions = judge.GetFirstSettlementsActions();
                break;
        }

        if (status != EGameStatus.NormalRound)
        {
            status = (EGameStatus)((int)status + 1);
        }
        
        var actionsExpectations = new List<(IAction Action, decimal Expectation)>();
        
        Parallel.ForEach(possibleActions, action =>
        {
            var changedSystem = action.Do();
            
            if (status == EGameStatus.NormalRound)
            {
                var diceRollsExpectations = new List<(int diceRoll, decimal Expectation)>();

                Parallel.For(2, 13, diceRoll =>
                {
                    decimal expectation = 0;
                    var systemCopy = SystemFactory.CopySystem(changedSystem)!;
                    systemCopy.SetDiceRoll(diceRoll);
                    var result = ParallelMinMaxExpectancy(
                        changedSystem, 
                        depth - 1, 
                        status);

                    expectation += result.Value * Utils.Utils.Probability(diceRoll);
                    diceRollsExpectations.Add((diceRoll, expectation));
                });
                
                actionsExpectations.Add((action, diceRollsExpectations
                    .Sum(pair => pair.Expectation)));
            }
            else
            {
                var result = ParallelMinMaxExpectancy(
                    changedSystem, 
                    depth - 1, 
                    status);

                actionsExpectations.Add((action, result.Value));
            }
        });
        
        var bestResult = actionsExpectations
            .OrderByDescending(r => r.Expectation)
            .FirstOrDefault();
        
        return new ParallelMinMaxExpectancyResult
        {
            Action = bestResult.Action,
            Value = bestResult.Expectation
        };
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
            
            var systemCopy = SystemFactory.CopySystem(changedSystem)!;
            systemCopy.SetExpectancyRoll(10);
            var judgeCopyGetActions = JudgeFactory.CopyParallelJudgeChangeSystem(_judge, systemCopy)!.GetActions;
            Expectimax(
                depth - 1, 
                judgeCopyGetActions, 
                out var expectation);

            actionsExpectations.Add((action, expectation));
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
    
    private class ParallelMinMaxExpectancyResult
    {
        public decimal Value { get; set; }
        public IAction? Action { get; set; }
    }
}
using CatanGame.Action;
using CatanGame.Evaluator;
using CatanGame.Judge;
using CatanGame.System;

namespace CatanGame.Player;

public class ExpectimaxPlayer : IPlayer
{
    private const int MaxDepth = 3;
    
    private IJudge _judge;
    private IEvaluator _evaluator;
    private ISystem _system;

    public ExpectimaxPlayer(IJudge judge, IEvaluator evaluator, ISystem system)
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
        var lockObject = new object();

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

        Parallel.ForEach(possibleActions,
            // Thread-local initializer
            () => (bestAction: (IAction?)null, bestValue: -1000m),

            // Body
            (action, loopState, localBest) =>
            {
                decimal expectation = 0;

                action.Do();
                for (var diceRoll = 2; diceRoll <= 12; diceRoll++)
                {
                    _system.SetDiceRoll(diceRoll);
                    Expectimax(depth - 1, getActions, out var childValue);
                    _system.SetDiceRollUndo(diceRoll);
                    expectation += childValue * Utils.Utils.Probability(diceRoll);
                }
                action.Undo();

                if (expectation > localBest.bestValue)
                {
                    localBest.bestValue = expectation;
                    localBest.bestAction = action;
                }

                return localBest;
            },

            // Finalizer
            localBest =>
            {
                lock (lockObject)
                {
                    if (localBest.bestValue > bestValue)
                    {
                        bestValue = localBest.bestValue;
                        bestAction = localBest.bestAction;
                    }
                }
            }
        );

        return bestAction;
    }
}

using CatanGame.Action;
using CatanGame.Evaluator;
using CatanGame.Judge;

namespace CatanGame.Player;

public class HeuristicPlayer : IPlayer
{
    private readonly IJudge _judge;
    private readonly IEvaluator _evaluator;
    
    
    
    public HeuristicPlayer(IJudge judge, IEvaluator evaluator)
    {
        _judge = judge;
        _evaluator = evaluator;
    }

    public void Play()
    {
        while (true)
        {
            var legalActions = _judge.GetActions();
            var (evaluation, bestAction) = GetSortedActions(legalActions).FirstOrDefault();
            if (bestAction == null || evaluation < _evaluator.Evaluate())
            {
                return;
            }
            bestAction.Do();
            bestAction.Show();
        }
    }

    public void PlayStartTurn()
    {
        var settlementActions = _judge.GetFirstSettlementsActions();
        var (evaluation, bestAction) = GetSortedActions(settlementActions).FirstOrDefault();
        if (bestAction == null || evaluation < _evaluator.Evaluate())
        {
            return;
        }
        bestAction.Do();
        bestAction.Show();
        
        var roadsActions = _judge.GetFirstRoadsActions();
        (evaluation, bestAction) = GetSortedActions(roadsActions).FirstOrDefault();
        if (bestAction == null || evaluation < _evaluator.Evaluate())
        {
            return;
        }
        bestAction.Do();
        bestAction.Show();
    }

    private IEnumerable<(decimal, IAction)> GetSortedActions(IEnumerable<IAction> actions)
    {
        var evaluatedActions = actions.Select(action =>
        {
            action.Do();
            var evaluation = _evaluator.Evaluate();
            action.Undo();
            return (evaluation, action);
        });

        var sortedActions = evaluatedActions.OrderByDescending(x => x.Item1).ToArray();
        return sortedActions;
    }
}
using CatanGame.Evaluator;
using CatanGame.Judge;

namespace CatanGame.Player;

public class ExpectimaxPlayer : IPlayer
{
    private readonly IJudge _judge;
    private readonly IEvaluator _evaluator;
    
    
    
    public ExpectimaxPlayer(IJudge judge, IEvaluator evaluator)
    {
        _judge = judge;
        _evaluator = evaluator;
    }
    
    public void Play()
    {
        
    }

    public void PlayStartTurn()
    {
        throw new NotImplementedException();
    }
}
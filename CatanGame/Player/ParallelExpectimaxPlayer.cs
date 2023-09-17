using CatanGame.Evaluator;
using CatanGame.Judge;
using CatanGame.System;

namespace CatanGame.Player;

public class ParallelExpectimaxPlayer: ExpectimaxPlayer
{
    public ParallelExpectimaxPlayer(IJudge judge, IEvaluator evaluator, ISystem system) 
        : base(judge, evaluator, system)
    {
        
    }
    
    
}
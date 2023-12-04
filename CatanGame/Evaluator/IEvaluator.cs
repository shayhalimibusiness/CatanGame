using CatanGame.System;

namespace CatanGame.Evaluator;

public interface IEvaluator
{
    decimal Evaluate();
    decimal EvaluateSystem(ISystem system);
}
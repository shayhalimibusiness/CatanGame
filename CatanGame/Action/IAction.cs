using CatanGame.System;

namespace CatanGame.Action;

public interface IAction
{
    ISystem Do();
    ISystem Do(ISystem system);
    void Undo();
    void Show();
}
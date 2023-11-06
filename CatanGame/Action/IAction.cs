using CatanGame.System;

namespace CatanGame.Action;

public interface IAction
{
    ISystem Do();
    void Undo();
    void Show();
}
using CatanGame.System;

namespace CatanGame.Action;

public class Pass : IAction
{
    public ISystem System { get; set; }
    
    public Pass(ISystem system)
    {
        System = system;
    }

    public ISystem Do()
    {
        return System;
    }

    public ISystem Do(ISystem system)
    {
        return system;
    }

    public void Undo()
    {
        
    }

    public void Show()
    {
        
    }
}
using CatanGame.Enums;
using CatanGame.System;
using CatanGame.UI;
using CatanGame.Utils;

namespace CatanGame.Judge;

public static class JudgeFactory
{
    public static IJudge? CopyParallelJudgeChangeSystem(IJudge other, ISystem system)
    {
        if (other is not ParallelJudge otherJudge)
        {
            return null;
        }
        
        var ePlayer = otherJudge._ePlayer;
        var ui = UiFactory.CreateUi();
        
        var judge = new ParallelJudge(system, ui, ePlayer);

        return judge;
    }
    
    public static IJudge? CopyJudge(IJudge other)
    {
        if (other is not ParallelJudge otherJudge)
        {
            return null;
        }
        
        var ePlayer = otherJudge._ePlayer;
        var ui = UiFactory.CreateUi();
        var system = SystemFactory.CopySystem(otherJudge._system);
        
        var judge = new Judge(system!, ui, ePlayer);

        return judge;
    }
    
    public static IJudge CreateParallelJudge()
    {
        const EPlayer ePlayer = EPlayer.Player1;
        var ui = UiFactory.CreateUi();
        var system = SystemFactory.CreateSystemAndLinqUi(ui);
        
        var judge = new ParallelJudge(system, ui, ePlayer);

        return judge;
    }
    
    public static IJudge CreateJudge()
    {
        const EPlayer ePlayer = EPlayer.Player1;
        var ui = UiFactory.CreateUi();
        var system = SystemFactory.CreateSystemAndLinqUi(ui);
        
        var judge = new Judge(system, ui, ePlayer);

        return judge;
    }
    
    public static (IJudge, ISystem, IUi) CreateJudgeFullReturn()
    {
        const EPlayer ePlayer = EPlayer.Player1;
        var ui = UiFactory.CreateUi();
        var system = SystemFactory.CreateSystemAndLinqUi(ui);
        
        var judge = new Judge(system, ui, ePlayer);

        return (judge, system, ui);
    }
}
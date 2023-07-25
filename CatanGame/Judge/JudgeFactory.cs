using CatanGame.Enums;
using CatanGame.System;
using CatanGame.UI;
using CatanGame.Utils;

namespace CatanGame.Judge;

public static class JudgeFactory
{
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
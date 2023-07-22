using CatanGame.UI;

namespace CatanGame.Utils;

public class UiFactory
{
    public static IUi CreateUi()
    {
        var names = GeneralFactory.CreatePlayersNames();
        var ui = new Ui(names);
        return ui;
    }
}
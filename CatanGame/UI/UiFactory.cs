using CatanGame.UI;

namespace CatanGame.Utils;

public class UiFactory
{
    public static IUi CreateUi()
    {
        var names = GeneralFactory.CreateNameDictionary();
        var ui = new Ui(names);
        return ui;
    }
}
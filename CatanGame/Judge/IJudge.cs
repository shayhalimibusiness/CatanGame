using CatanGame.Action;
using CatanGame.Enums;

namespace CatanGame.Judge;

public interface IJudge
{
    List<IAction> GetActions(EPlayer ePlayer);
}
using CatanGame.Enums;

namespace CatanGame.System.Cards;

public static class CardsFactory
{
    public static ICards? CreateCardsCopy(ICards other)
    {
        return other is not Cards cards ? null : new Cards(cards);
    }
    
    public static Dictionary<EPlayer, ICards> Create1PlayerBlankAllCards()
    {
        var allCards = new Dictionary<EPlayer, ICards>
        {
            { EPlayer.Player1, new Cards() },
        };

        return allCards;
    }
    
    public static Dictionary<EPlayer, ICards> Create2PlayersBlankAllCards()
    {
        var allCards = new Dictionary<EPlayer, ICards>
        {
            { EPlayer.Player1, new Cards() },
            { EPlayer.Player2, new Cards() }
        };

        return allCards;
    }
    
    public static ICards CreateCardsExample1()
    {
        var cards = new Cards();
        cards.TransferResources(EResource.Iron,2);
        cards.TransferResources(EResource.Sheep,0);
        cards.TransferResources(EResource.Wheat,3);
        cards.TransferResources(EResource.Tin,0);
        cards.TransferResources(EResource.Wood,5);
        cards.TransferResources(EResource.PointCard,2);
        cards.TransferTotalPoints(3);
        return cards;
    }
    
    public static ICards CreateCardsExample2()
    {
        var cards = new Cards();
        cards.TransferResources(EResource.Iron,3);
        cards.TransferResources(EResource.Sheep,2);
        cards.TransferResources(EResource.Wheat,0);
        cards.TransferResources(EResource.Tin,0);
        cards.TransferResources(EResource.Wood,4);
        cards.TransferResources(EResource.PointCard,2);
        cards.TransferTotalPoints(9);
        return cards;
    }

    public static Dictionary<EPlayer, ICards> CreateAllCardsExample()
    {
        var cards1 = CreateCardsExample1();
        var cards2 = CreateCardsExample2();
        var allCards = new Dictionary<EPlayer, ICards>
        {
            { EPlayer.Player1, cards1 },
            { EPlayer.Player2, cards2 }
        };
        
        return allCards;
    }
}
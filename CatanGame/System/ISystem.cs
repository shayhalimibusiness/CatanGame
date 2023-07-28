using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System.Board;
using CatanGame.System.Cards;

namespace CatanGame.System;

public interface ISystem
{
     void RoleDice();
     IBoard GetBoard();
     ICards GetCards(EPlayer ePlayer);
     Dictionary<EPlayer, ICards> GetAllCards();
     void SetSettlement(int x, int y, EPlayer ePlayer);
     void SetSettlementUndo(int x, int y, EPlayer ePlayer);
     void BuildSettlement(int x, int y, EPlayer ePlayer);
     void BuildSettlementUndo(int x, int y, EPlayer ePlayer);
     void BuildCity(int x, int y, EPlayer ePlayer);
     void BuildCityUndo(int x, int y, EPlayer ePlayer);
     void SetRoad(int x, int y, ERoads eRoads, EPlayer ePlayer);
     void SetRoadUndo(int x, int y, ERoads eRoads, EPlayer ePlayer);
     void BuildRoad(int x, int y, ERoads eRoads, EPlayer ePlayer);
     void BuildRoadUndo(int x, int y, ERoads eRoads, EPlayer ePlayer);
     bool BuyCard(EPlayer ePlayer);
     void BuyCardUndo(EPlayer ePlayer);
     void Trade(EPlayer ePlayer, EResource sell, EResource buy, int times);
     void TradeUndo(EPlayer ePlayer, EResource sell, EResource buy, int times);
     bool HasPort(EPlayer ePlayer, EResource eResource);
}
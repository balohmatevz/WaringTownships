using UnityEngine;
using System.Collections;

public class House : Building
{
    public House(Tile location, int playerNumber, bool forceBuilding = false) : base(BuildingType.HOUSE, location, playerNumber, forceBuilding)
    {
        buildingSR.sprite = Refs.obj.BuildingHouse;
        InitialHealth = 20;
        Health = InitialHealth;
    }


    public override void OnTileUpdated()
    {

    }

}

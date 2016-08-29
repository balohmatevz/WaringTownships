using UnityEngine;
using System.Collections;

public class Temple : Building
{
    public Temple(Tile location, int playerNumber, bool forceBuilding = false) : base(BuildingType.HOUSE, location, playerNumber, forceBuilding)
    {
        buildingSR.sprite = Refs.obj.BuildingTemple;
        InitialHealth = 30;
        Health = InitialHealth;
    }


    public override void OnTileUpdated()
    {

    }
}

using UnityEngine;
using System.Collections;

public class Tower : Building
{
    public Tower(Tile location, int playerNumber, bool forceBuilding = false) : base(BuildingType.TOWER, location, playerNumber, forceBuilding)
    {
        buildingSR.sprite = Refs.obj.BuildingTower;
        InitialHealth = 100;
        Health = InitialHealth;
        WorldController.obj.AllTowers.Add(this);
    }


    public override void OnTileUpdated()
    {

    }

}

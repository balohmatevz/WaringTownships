using UnityEngine;
using System.Collections;

public class Stable : Building
{
    public Stable(Tile location, int playerNumber, bool forceBuilding = false) : base(BuildingType.HOUSE, location, playerNumber, forceBuilding)
    {
        buildingSR.sprite = Refs.obj.BuildingStable;
        InitialHealth = 50;
        Health = InitialHealth;
    }


    public override void OnTileUpdated()
    {

    }

    public override void Select()
    {
        Refs.obj.UIStable.SetActive(true);
    }

    public override void Deselect()
    {
        Refs.obj.UIStable.SetActive(false);
    }

}

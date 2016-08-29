using UnityEngine;
using System.Collections;

public class Barracks : Building
{
    public Barracks(Tile location, int playerNumber, bool forceBuilding = false) : base(BuildingType.HOUSE, location, playerNumber, forceBuilding)
    {
        buildingSR.sprite = Refs.obj.BuildingBarracks;
        InitialHealth = 50;
        Health = InitialHealth;
    }


    public override void OnTileUpdated()
    {

    }

    public override void Select()
    {
        if (PlayerNumber == 1)
        {
            Refs.obj.UIBarracks.SetActive(true);
        }
    }

    public override void Deselect()
    {
        if (PlayerNumber == 1)
        {
            Refs.obj.UIBarracks.SetActive(false);
        }
    }

}

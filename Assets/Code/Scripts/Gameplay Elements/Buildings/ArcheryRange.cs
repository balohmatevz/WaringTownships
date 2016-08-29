using UnityEngine;
using System.Collections;

public class ArcheryRange : Building
{
    public ArcheryRange(Tile location, int playerNumber, bool forceBuilding = false) : base(BuildingType.HOUSE, location, playerNumber, forceBuilding)
    {
        buildingSR.sprite = Refs.obj.BuildingArcheryRange;
        InitialHealth = 50;
        Health = InitialHealth;
    }


    public override void OnTileUpdated()
    {

    }

    public override void Select()
    {
        Refs.obj.UIArcheryRange.SetActive(true);
    }

    public override void Deselect()
    {
        Refs.obj.UIArcheryRange.SetActive(false);
    }

}

using UnityEngine;
using System.Collections;

public class Road : Building
{

    public Road(Tile location, bool forceBuilding = false) : base(BuildingType.PATH, location, 0, forceBuilding)
    {

    }

    public override void OnTileUpdated()
    {
        int SpriteNumber = 0;

        if (Location.NORTH != null)
        {
            foreach (Building b in Location.NORTH.Buildings)
            {
                if (b is Road)
                {
                    SpriteNumber |= 1;
                    break;
                }
            }
        }

        if (Location.SOUTH != null)
        {
            foreach (Building b in Location.SOUTH.Buildings)
            {
                if (b is Road)
                {
                    SpriteNumber |= 2;
                    break;
                }
            }
        }

        if (Location.EAST != null)
        {
            foreach (Building b in Location.EAST.Buildings)
            {
                if (b is Road)
                {
                    SpriteNumber |= 4;
                    break;
                }
            }
        }

        if (Location.WEST != null)
        {
            foreach (Building b in Location.WEST.Buildings)
            {
                if (b is Road)
                {
                    SpriteNumber |= 8;
                    break;
                }
            }
        }

        buildingSR.sprite = Refs.obj.Road[SpriteNumber];

    }
}

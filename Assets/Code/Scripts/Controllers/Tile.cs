using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class Tile
{
    public int X;
    public int Y;
    public GameObject tileGO;
    public SpriteRenderer tileSR;
    public Transform tileTransform;
    public UnityEvent ES;

    public static List<TerrainType> CannotBuildTypes = new List<TerrainType> { TerrainType.TREES, TerrainType.WATER};

    public Tile NORTH;
    public Tile SOUTH;
    public Tile EAST;
    public Tile WEST;
    public Tile NORTHEAST;
    public Tile NORTHWEST;
    public Tile SOUTHEAST;
    public Tile SOUTHWEST;

    public List<Building> Buildings;
    public List<Unit> Units;

    private TerrainType _terrain;
    public TerrainType Terrain
    {
        get
        {
            return _terrain;
        }
        set
        {
            _terrain = value;
            switch (Terrain)
            {
                case TerrainType.GRASS:
                    tileSR.sprite = Refs.obj.TerrainGrass;
                    break;
                case TerrainType.SAND:
                    tileSR.sprite = Refs.obj.TerrainSand;
                    break;
                case TerrainType.TREES:
                    tileSR.sprite = Refs.obj.TerrainTrees;
                    break;
                case TerrainType.WATER:
                    tileSR.sprite = Refs.obj.TerrainWater;
                    foreach (Tile t in WorldController.GetNeighbours(this))
                    {
                        if (t.Terrain == TerrainType.GRASS)
                        {
                            t.Terrain = TerrainType.SAND;
                        }
                    }
                    break;
            }
        }
    }

    public Tile(int x, int y, TerrainType terrainType)
    {
        tileGO = GameObject.Instantiate(Prefabs.obj.Tile);
        tileTransform = tileGO.transform;
        tileSR = tileGO.GetComponent<SpriteRenderer>();
        tileTransform.SetParent(Refs.obj.WorldAnchor.transform);
        tileTransform.localPosition = new Vector2(x, y);

        Buildings = new List<Building>();
        Units = new List<Unit>();

        X = x;
        Y = y;
        Terrain = terrainType;
    }

    public void SpreadTerrain(TerrainType terrainType, float weight, float decreaseFactor)
    {
        if (Terrain == terrainType)
        {
            return;
        }

        Terrain = terrainType;
        foreach (Tile t in WorldController.GetCardinalNeighbours(this))
        {
            if (Random.Range(0, 1f) < weight)
            {
                t.SpreadTerrain(terrainType, weight - decreaseFactor, decreaseFactor);
            }
        }
    }

    public bool CanBuildBuilding(BuildingType buildingType)
    {
        switch (buildingType)
        {
            case BuildingType.PATH:
                foreach (Building b in Buildings)
                {
                    if (b is Road)
                    {
                        return false;
                    }
                }
                return !(CannotBuildTypes.Contains(Terrain));
        }

        return false;
    }

    public bool ContainsBuildingOfType(System.Type type)
    {
        foreach (Building b in Buildings)
        {
            if (b.GetType().IsSubclassOf(type))
            {
                return true;
            }
        }

        return false;
    }

    public void OnHoverOut()
    {
        tileSR.color = Color.white;
    }

    public void OnHoverIn()
    {
        tileSR.color = Color.red;
    }

    public void OnHoverOver()
    {

    }

    public void OnPrimaryInteraction()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Units.Count > 0)
        {
            WorldController.obj.SelectedUnit = Units[0];
            WorldController.obj.SelectedBuilding = null;
            return;
        }
        else
        {
            WorldController.obj.SelectedUnit = null;
        }

        if (Buildings.Count > 0)
        {
            WorldController.obj.SelectedUnit = null;
            WorldController.obj.SelectedBuilding = Buildings[0];
            return;
        }
        else
        {
            WorldController.obj.SelectedBuilding = null;
        }
    }

    public void OnSecondaryInteraction()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (WorldController.obj.SelectedUnit != null)
        {
            WorldController.obj.SelectedUnit.OnRightClick(this);
        }
    }

    public int GetTileCost()
    {
        switch (Terrain)
        {
            case TerrainType.GRASS:
            case TerrainType.SAND:
                return 1;
            case TerrainType.WATER:
                if (NORTH != null && NORTH.Terrain != TerrainType.WATER)
                {
                    return 10;
                }
                if (SOUTH != null && SOUTH.Terrain != TerrainType.WATER)
                {
                    return 10;
                }
                if (EAST != null && EAST.Terrain != TerrainType.WATER)
                {
                    return 10;
                }
                if (WEST != null && WEST.Terrain != TerrainType.WATER)
                {
                    return 10;
                }
                return 0;
            case TerrainType.TREES:
            default:
                return 0;
        }
    }

    public int GetPathGenCost()
    {
        switch (Terrain)
        {
            case TerrainType.GRASS:
            case TerrainType.SAND:
                return 1;
            case TerrainType.TREES:
                return 5;
            case TerrainType.WATER:
                if (NORTH != null && NORTH.Terrain != TerrainType.WATER)
                {
                    return 10;
                }
                if (SOUTH != null && SOUTH.Terrain != TerrainType.WATER)
                {
                    return 10;
                }
                if (EAST != null && EAST.Terrain != TerrainType.WATER)
                {
                    return 10;
                }
                if (WEST != null && WEST.Terrain != TerrainType.WATER)
                {
                    return 10;
                }
                return 0;
            default:
                return 0;
        }
    }
}

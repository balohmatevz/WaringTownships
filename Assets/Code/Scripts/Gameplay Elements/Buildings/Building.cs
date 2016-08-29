using UnityEngine;
using System.Collections;

public class Building
{
    public GameObject GO;
    public GameObject HealthRedGO;
    public GameObject HealthGreenGO;
    public Transform HealthGreenTransform;

    public float InitialHealth;
    private float _Health;
    public float Health
    {
        get
        {
            return _Health;
        }
        set
        {
            _Health = value;
            HealthGreenTransform.localScale = new Vector3(Health / (float)InitialHealth, 1, 1);

            if (_Health == InitialHealth)
            {
                HealthRedGO.SetActive(false);
                HealthGreenGO.SetActive(false);
            }
            else
            {
                HealthRedGO.SetActive(true);
                HealthGreenGO.SetActive(true);
            }
            if (value <= 0)
            {
                GameObject.Destroy(buildingGO);
                Location.Buildings.Remove(this);
                WorldController.obj.AllBuildings.Remove(this);
                if (this is Tower)
                {
                    WorldController.obj.AllTowers.Remove((Tower)this);
                }
            }
        }

    }

    public Tile Location;
    public BuildingType TypeOfBuilding;

    public GameObject buildingGO;
    public SpriteRenderer buildingSR;
    public Transform buildingTransform;

    public int PlayerNumber;

    public Building(BuildingType buildingType, Tile location, int playerNumber, bool forceBuilding = false)
    {
        if (!location.CanBuildBuilding(buildingType))
        {
            if (!forceBuilding)
            {
                return;
            }
            else
            {
                location.Terrain = TerrainType.GRASS;
            }
        }

        PlayerNumber = playerNumber;

        Location = location;
        Location.Buildings.Add(this);

        if (this is Road)
        {
            WorldController.obj.AllBuildings.Add(this);
            buildingGO = GameObject.Instantiate(Prefabs.obj.Road);
        }
        else
        {
            buildingGO = GameObject.Instantiate(Prefabs.obj.Building);
        }
        buildingTransform = buildingGO.transform;
        buildingSR = buildingGO.GetComponent<SpriteRenderer>();
        buildingTransform.SetParent(Location.tileGO.transform);
        buildingTransform.localPosition = new Vector2(0, 0);

        if (!(this is Road))
        {
            HealthRedGO = GameObject.Instantiate(Prefabs.obj.HealthRed);
            HealthRedGO.transform.SetParent(buildingTransform);
            HealthRedGO.transform.localPosition = Vector2.zero;

            HealthGreenGO = GameObject.Instantiate(Prefabs.obj.HealthGreen);
            HealthGreenGO.transform.SetParent(buildingTransform);
            HealthGreenGO.transform.localPosition = Vector2.zero;
            HealthGreenTransform = HealthGreenGO.transform;
        }

        TypeOfBuilding = buildingType;

        buildingSR.color = Refs.obj.TeamColors[PlayerNumber];

        UpdateNeighbours();
        OnTileUpdated();
    }

    public void UpdateNeighbours()
    {
        foreach (Tile t in WorldController.GetCardinalNeighbours(Location))
        {
            foreach (Building b in t.Buildings)
            {
                b.OnTileUpdated();
            }
        }
    }

    public virtual void OnTileUpdated()
    {

    }

    public virtual void Select()
    {

    }

    public virtual void Deselect()
    {

    }
}

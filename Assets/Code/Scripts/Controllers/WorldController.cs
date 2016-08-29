using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WorldController : MonoBehaviour
{
    public static WorldController obj;

    public Tile[,] world;

    public static int WORLD_SIZE_X = 100;
    public static int WORLD_SIZE_Y = 100;

    public List<Vector2> Cities;

    public PathFinder PF;

    public List<Unit> AllUnits;
    public List<Building> AllBuildings;
    public List<Tower> AllTowers;

    private Unit _SelectedUnit = null;
    public Unit SelectedUnit
    {
        get
        {
            return _SelectedUnit;
        }
        set
        {
            if (_SelectedUnit != null)
            {
                _SelectedUnit.Deselect();
                Refs.obj.UISelectedUnit.SetActive(false);
            }
            _SelectedUnit = value;
            if (_SelectedUnit != null)
            {
                _SelectedUnit.Select();
                Refs.obj.UISelectedUnit.SetActive(true);
                Refs.obj.UISelectedUnitName.text = _SelectedUnit.UnitName;
                Refs.obj.UISelectedUnitAttack.text = "Attack: " + _SelectedUnit.Attack;
                Refs.obj.UISelectedUnitHP.text = "Hit Points: " + _SelectedUnit.Health + "/" + _SelectedUnit.InitialHealth;
            }
        }
    }

    private Building _SelectedBuilding = null;
    public Building SelectedBuilding
    {
        get
        {
            return _SelectedBuilding;
        }
        set
        {
            if (_SelectedBuilding != null)
            {
                _SelectedBuilding.Deselect();
            }

            if (value != null)
            {
                if (value.PlayerNumber != 1)
                {
                    return;
                }
            }

            _SelectedBuilding = value;
            if (_SelectedBuilding != null)
            {
                _SelectedBuilding.Select();
            }
        }
    }

    void Awake()
    {
        obj = this;
        AllUnits = new List<Unit>();
        AllBuildings = new List<Building>();
        AllTowers = new List<Tower>();
    }

    public bool CoordInWorld(int x, int y)
    {
        return (y >= 0 && y < WORLD_SIZE_Y) && (x >= 0 && x < WORLD_SIZE_X);
    }

    public static List<Tile> GetNeighbours(Tile t)
    {
        List<Tile> result = new List<Tile>();
        if (t.NORTH != null)
        {
            result.Add(t.NORTH);
        }
        if (t.SOUTH != null)
        {
            result.Add(t.SOUTH);
        }
        if (t.EAST != null)
        {
            result.Add(t.EAST);
        }
        if (t.WEST != null)
        {
            result.Add(t.WEST);
        }

        if (t.NORTHEAST != null)
        {
            result.Add(t.NORTHEAST);
        }
        if (t.NORTHWEST != null)
        {
            result.Add(t.NORTHWEST);
        }
        if (t.SOUTHEAST != null)
        {
            result.Add(t.SOUTHEAST);
        }
        if (t.SOUTHWEST != null)
        {
            result.Add(t.SOUTHWEST);
        }

        return result;
    }

    public static List<Tile> GetCardinalNeighbours(Tile t)
    {
        List<Tile> result = new List<Tile>();
        if (t.NORTH != null)
        {
            result.Add(t.NORTH);
        }
        if (t.SOUTH != null)
        {
            result.Add(t.SOUTH);
        }
        if (t.EAST != null)
        {
            result.Add(t.EAST);
        }
        if (t.WEST != null)
        {
            result.Add(t.WEST);
        }
        return result;
    }

    public void CreateWorld()
    {
        Refs.obj.Loading.SetActive(true);
        //create map
        world = new Tile[WORLD_SIZE_X, WORLD_SIZE_Y];

        //create tiles
        for (int x = 0; x < WORLD_SIZE_X; x++)
        {
            for (int y = 0; y < WORLD_SIZE_Y; y++)
            {
                world[x, y] = new Tile(x, y, TerrainType.GRASS);
            }
        }

        //log neighbours
        for (int x = 0; x < WORLD_SIZE_X; x++)
        {
            for (int y = 0; y < WORLD_SIZE_Y; y++)
            {
                if (CoordInWorld(x, y + 1))
                {
                    world[x, y].NORTH = world[x, y + 1];
                }
                if (CoordInWorld(x, y - 1))
                {
                    world[x, y].SOUTH = world[x, y - 1];
                }
                if (CoordInWorld(x + 1, y))
                {
                    world[x, y].EAST = world[x + 1, y];
                }
                if (CoordInWorld(x - 1, y))
                {
                    world[x, y].WEST = world[x - 1, y];
                }
                if (CoordInWorld(x + 1, y + 1))
                {
                    world[x, y].NORTHEAST = world[x + 1, y + 1];
                }
                if (CoordInWorld(x - 1, y + 1))
                {
                    world[x, y].NORTHWEST = world[x - 1, y + 1];
                }
                if (CoordInWorld(x + 1, y - 1))
                {
                    world[x, y].SOUTHEAST = world[x + 1, y - 1];
                }
                if (CoordInWorld(x - 1, y - 1))
                {
                    world[x, y].SOUTHWEST = world[x - 1, y - 1];
                }
            }
        }

        //Create forest
        List<float> forestDecreaseFactors = new List<float> { 0.01f, 0.03f, 0.03f, 0.03f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f }; //Controls sizes of forests, smaller the number, the larger the forests.
        foreach (float decreaseFactor in forestDecreaseFactors)
        {
            int x = Random.Range(0, WORLD_SIZE_X);
            int y = Random.Range(0, WORLD_SIZE_Y);

            world[x, y].SpreadTerrain(TerrainType.TREES, 1f, decreaseFactor);
        }

        //Create small forests
        for (int i = 0; i < 100; i++)
        {
            int x = Random.Range(0, WORLD_SIZE_X);
            int y = Random.Range(0, WORLD_SIZE_Y);

            world[x, y].SpreadTerrain(TerrainType.TREES, 0.5f, 0.1f);
        }

        //Create tiny forests
        for (int i = 0; i < 300; i++)
        {
            int x = Random.Range(0, WORLD_SIZE_X);
            int y = Random.Range(0, WORLD_SIZE_Y);

            world[x, y].SpreadTerrain(TerrainType.TREES, 0.2f, 0.1f);
        }

        //create rivers
        for (int i = 0; i < Random.Range(5, 10); i++)
        {
            float k = Random.Range(-2, 1f);
            float n = Random.Range(0, 2 * WORLD_SIZE_Y);
            int lastY = 0;
            int changeDirectionTiles = Random.Range(5, 15);

            for (int x = 0; x < WORLD_SIZE_X; x++)
            {
                int y = (int)(k * x + n);
                if (y >= 0 && y < WORLD_SIZE_Y)
                {
                    world[x, y].Terrain = TerrainType.WATER;
                }

                for (int dY = Mathf.Min(lastY, y); dY < Mathf.Max(lastY, dY); dY++)
                {
                    if (dY >= 0 && dY < WORLD_SIZE_Y)
                    {
                        world[x, dY].Terrain = TerrainType.WATER;
                    }
                }

                changeDirectionTiles--;
                if (changeDirectionTiles <= 0)
                {
                    k += Random.Range(-0.5f, 0.5f);
                    //y = kx + n;

                    if (k > 1)
                    {
                        k = 1;
                    }

                    n = y - (k * x);
                }

                lastY = y;
            }
        }

        //Create lakes
        List<float> lakeDecreaseFactors = new List<float> { 0.06f, 0.06f, 0.09f, 0.09f, 0.2f, 0.2f, 0.2f };
        foreach (float decreaseFactor in lakeDecreaseFactors)
        {
            int x = Random.Range(0, WORLD_SIZE_X);
            int y = Random.Range(0, WORLD_SIZE_Y);

            world[x, y].SpreadTerrain(TerrainType.WATER, 1f, decreaseFactor);
        }

        //Create outposts

        int[,] mapWater = new int[WORLD_SIZE_X, WORLD_SIZE_Y];
        for (int x = 0; x < WORLD_SIZE_X; x++)
        {
            for (int y = 0; y < WORLD_SIZE_Y; y++)
            {
                mapWater[x, y] = world[x, y].GetPathGenCost();
            }
        }

        int[,] mapBlank = new int[WORLD_SIZE_X, WORLD_SIZE_Y];
        for (int x = 0; x < WORLD_SIZE_X; x++)
        {
            for (int y = 0; y < WORLD_SIZE_Y; y++)
            {
                mapBlank[x, y] = 1;
            }
        }

        PathFinder PF_Roads = new PathFinder(mapWater);
        PF_Roads.Diagonals = false;
        PathFinder PF_Houses = new PathFinder(mapBlank);
        PF_Houses.Diagonals = false;

        Vector2 center = new Vector2(WORLD_SIZE_X / 2, WORLD_SIZE_Y / 2);
        Cities = new List<Vector2>();
        for (int i = 1; i < Random.Range(4, 7); i++)
        {
            Vector2 loc = Vector2.zero;
            int attempts = 2;
            bool pass = true;
            while (true)
            {
                int safety = 1000;
                do
                {
                    safety--;
                    if (safety <= 0)
                    {
                        Debug.Log("Failed to find spot for city");
                        break;
                    }
                    loc = new Vector2(Random.Range(0, WORLD_SIZE_X), Random.Range(0, WORLD_SIZE_Y));
                    if (world[(int)loc.x, (int)loc.y].Terrain == TerrainType.WATER)
                    {
                        continue;
                    }
                } while (Vector2.Distance(center, loc) > 45);   //MAGIC 45 = max distance from center of map to town center

                pass = true;
                foreach (Vector2 v in Cities)
                {
                    if (Vector2.Distance(v, loc) < 25)  //MAGIC 25 = min distance between towns
                    {
                        pass = false;
                        break;
                    }
                }

                if (!pass)
                {
                    continue;
                }

                foreach (Vector2 v in Cities)
                {
                    List<PathFinderNode> path_road = PF_Roads.FindPath(world[(int)loc.x, (int)loc.y], world[(int)v.x, (int)v.y]);
                    if (path_road == null || path_road.Count == 0)
                    {
                        pass = false;
                        break;
                    }
                    else
                    {
                        foreach (PathFinderNode pfn in path_road)
                        {
                            new Road(world[pfn.X, pfn.Y], true);
                        }
                    }
                }

                if (pass)
                {
                    break;
                }

                attempts--;
                if (attempts <= 0)
                {
                    Debug.Log("Failed to generate city");
                    break;
                }
            }
            if (pass)
            {
                int TownSize = Random.Range(10, 100);
                int sizeDelta = (int)(Mathf.Sqrt(TownSize) * 1.2f);

                new Tower(world[(int)loc.x, (int)loc.y], i, true);

                int minXsize = Random.Range(-sizeDelta, 0);
                int maxXsize = Random.Range(0, sizeDelta);

                if ((int)loc.x + minXsize < 1)
                {
                    minXsize = 0;
                }
                if (maxXsize >= WORLD_SIZE_X)
                {
                    minXsize = WORLD_SIZE_X - 1;
                }

                //Spawn horizontal roads
                SpawnRoadFromTo((int)loc.x + minXsize, (int)loc.y - 1, (int)loc.x + maxXsize, (int)loc.y - 1);

                //Spawn vertical roads
                int upBlocked = 0;
                int downBlocked = 0;
                for (int x = (int)loc.x + minXsize; x <= (int)loc.x + maxXsize; x++)
                {
                    if (upBlocked > 0)
                    {
                        upBlocked--;
                    }
                    if (downBlocked > 0)
                    {
                        downBlocked--;
                    }

                    if (Random.Range(0f, 1f) < 0.2f && upBlocked == 0)
                    {
                        SpawnRoadFromTo(x, (int)loc.y - 1, x, (int)loc.y + Random.Range(0, sizeDelta));
                        upBlocked = 3;
                    }
                    if (Random.Range(0f, 1f) < 0.2f && downBlocked == 0)
                    {
                        SpawnRoadFromTo(x, (int)loc.y - 1, x, (int)loc.y - Random.Range(0, sizeDelta));
                        downBlocked = 3;
                    }
                }

                int templeX;
                int templeY;

                int tries = 100;
                while (tries > 0)
                {
                    do
                    {
                        templeX = (int)Random.Range(loc.x - sizeDelta, loc.x + sizeDelta);
                        templeY = (int)Random.Range(loc.y - sizeDelta, loc.y + sizeDelta);
                    } while (Vector2.Distance(loc, new Vector2(templeX, templeY)) > 4);
                    if (!CoordInWorld(templeX, templeY))
                    {
                        continue;
                    }

                    if (!world[templeX, templeY].ContainsBuildingOfType(typeof(Building)))
                    {
                        new Temple(world[templeX, templeY], i, true);
                        break;
                    }
                    tries--;
                }


                tries = 100;
                while (tries > 0)
                {
                    do
                    {
                        templeX = (int)Random.Range(loc.x - sizeDelta, loc.x + sizeDelta);
                        templeY = (int)Random.Range(loc.y - sizeDelta, loc.y + sizeDelta);
                    } while (Vector2.Distance(loc, new Vector2(templeX, templeY)) > 4);
                    if (!CoordInWorld(templeX, templeY))
                    {
                        continue;
                    }

                    if (!world[templeX, templeY].ContainsBuildingOfType(typeof(Building)))
                    {
                        new Stable(world[templeX, templeY], i, true);
                        break;
                    }
                    tries--;
                }

                tries = 100;
                while (tries > 0)
                {
                    do
                    {
                        templeX = (int)Random.Range(loc.x - sizeDelta, loc.x + sizeDelta);
                        templeY = (int)Random.Range(loc.y - sizeDelta, loc.y + sizeDelta);
                    } while (Vector2.Distance(loc, new Vector2(templeX, templeY)) > 4);
                    if (!CoordInWorld(templeX, templeY))
                    {
                        continue;
                    }

                    if (!world[templeX, templeY].ContainsBuildingOfType(typeof(Building)))
                    {
                        new Barracks(world[templeX, templeY], i, true);
                        break;
                    }
                    tries--;
                }


                tries = 100;
                while (tries > 0)
                {
                    do
                    {
                        templeX = (int)Random.Range(loc.x - sizeDelta, loc.x + sizeDelta);
                        templeY = (int)Random.Range(loc.y - sizeDelta, loc.y + sizeDelta);
                    } while (Vector2.Distance(loc, new Vector2(templeX, templeY)) > 4);
                    if (!CoordInWorld(templeX, templeY))
                    {
                        continue;
                    }

                    if (!world[templeX, templeY].ContainsBuildingOfType(typeof(Building)))
                    {
                        new ArcheryRange(world[templeX, templeY], i, true);
                        break;
                    }
                    tries--;
                }


                //Spawn houses
                for (int j = 0; j < TownSize; j++)
                {
                    int houseX;
                    int houseY;

                    do
                    {
                        houseX = (int)Random.Range(loc.x - sizeDelta, loc.x + sizeDelta);
                        houseY = (int)Random.Range(loc.y - sizeDelta, loc.y + sizeDelta);
                    } while (Vector2.Distance(loc, new Vector2(houseX, houseY)) > sizeDelta);
                    if (!CoordInWorld(houseX, houseY))
                    {
                        continue;
                    }

                    if (!world[houseX, houseY].ContainsBuildingOfType(typeof(Building)))
                    {
                        new House(world[houseX, houseY], i, true);
                        List<PathFinderNode> path_house = PF_Houses.FindPath(world[(int)loc.x, (int)loc.y], world[houseX, houseY]);
                        if (path_house != null && path_house.Count > 0)
                        {
                            foreach (PathFinderNode pfn in path_house)
                            {
                                world[pfn.X, pfn.Y].Terrain = TerrainType.GRASS;
                            }
                        }
                    }

                }

                Cities.Add(loc);
            }
        }

        //for (int i = 0; i < Cities.Count - 1; i++)
        //{
        //    Vector2 start = Cities[i];
        //
        //    for (int j = i + 1; j < Cities.Count; j++)
        //    {
        //        Vector2 end = Cities[j];
        //        SpawnRoadFromTo((int)start.x, (int)start.y, (int)end.x, (int)end.y);
        //    }
        //}

        //Spawn units

        //GameObject[] unitPrefabs = new GameObject[] {
        //    Prefabs.obj.UnitCavalryBow,
        //    Prefabs.obj.UnitCavalrySword,
        //    Prefabs.obj.UnitCavalrySpear,
        //    Prefabs.obj.UnitInfantryBow,
        //    Prefabs.obj.UnitInfantrySword,
        //    Prefabs.obj.UnitInfantrySpear,
        //    Prefabs.obj.UnitMilitiaBow,
        //    Prefabs.obj.UnitMilitiaSword,
        //    Prefabs.obj.UnitMilitiaSpear
        //};
        //
        //for (int x = 0; x < unitPrefabs.Length; x++)
        //{
        //    GameObject go = Instantiate(unitPrefabs[x]);
        //    Unit u = go.GetComponent<Unit>();
        //    u.Move(world[10 + x, 10]);
        //}

        //GameObject go = Instantiate(Prefabs.obj.UnitCavalrySpear);
        //Unit u = go.GetComponent<Unit>();
        //u.Move(world[10, 10]);
        //u.TeamNumber = 1;
        //go = Instantiate(Prefabs.obj.UnitCavalrySpear);
        //u = go.GetComponent<Unit>();
        //u.Move(world[14, 10]);
        //u.TeamNumber = 2;


        // go = Instantiate(Prefabs.obj.UnitCavalrySpear);
        // u = go.GetComponent<Unit>();
        // u.Move(world[10, 15]);
        // u.TeamNumber = 1;
        // go = Instantiate(Prefabs.obj.UnitCavalrySpear);
        // u = go.GetComponent<Unit>();
        // u.Move(world[20, 15]);
        // u.TeamNumber = 1;
        // go = Instantiate(Prefabs.obj.UnitCavalrySpear);
        // u = go.GetComponent<Unit>();
        // u.Move(world[15, 10]);
        // u.TeamNumber = 1;
        // go = Instantiate(Prefabs.obj.UnitCavalrySpear);
        // u = go.GetComponent<Unit>();
        // u.Move(world[15, 20]);
        // u.TeamNumber = 1;

        //new Barracks(world[15, 15], 2, true);
        //
        //SpawnRoadFromTo(10, 10, 14, 10);
        //SpawnRoadFromTo(16, 15, 16, 20);
        //SpawnRoadFromTo(16, 15, 16, 10);
        //SpawnRoadFromTo(16, 15, 10, 15);
        //SpawnRoadFromTo(16, 15, 20, 15);

        int[,] map = new int[WORLD_SIZE_X, WORLD_SIZE_Y];
        for (int x = 0; x < WORLD_SIZE_X; x++)
        {
            for (int y = 0; y < WORLD_SIZE_Y; y++)
            {
                map[x, y] = world[x, y].GetTileCost();
            }
        }
        PF = new PathFinder(map);
        PF.Diagonals = false;

        //int offsetX = WORLD_SIZE_X / 6;
        //int offsetY = WORLD_SIZE_Y / 6;
        //
        //Vector2 SpawnPosBottomLeft = new Vector2(offsetX, offsetY);
        //Vector2 SpawnPosTopRight = new Vector2(WORLD_SIZE_X - offsetX, WORLD_SIZE_Y - offsetY);
        //
        //SpawnRoadFromTo((int)SpawnPosBottomLeft.x, (int)SpawnPosBottomLeft.y, (int)SpawnPosTopRight.x, (int)SpawnPosTopRight.y);#

        foreach (Tower tower in AllTowers)
        {
            if (tower.PlayerNumber == 1)
            {
                Camera.main.transform.position = new Vector3(tower.Location.X, tower.Location.Y, -10);
            }
        }

        Refs.obj.Loading.SetActive(false);
        Refs.obj.Help.SetActive(true);
    }


    void SpawnRoadFromTo(int startX, int startY, int endX, int endY)
    {
        float radius = 50f;

        //int X = (int)roads[i].x;
        //int Y = (int)roads[i].y;

        int X = endX;
        int Y = endY;

        float k = (endY - startY) / (((endX - startX) == 0) ? 0.01f : (endX - startX));
        float n = endY - (k * endX);

        if (!CoordInWorld(endX, endY))
        {
            return;
        }
        if (!CoordInWorld(startX, startY))
        {
            return;
        }

        new Road(world[endX, endY], true);
        new Road(world[startX, startY], true);

        int lastX = 0;
        int lastY = 0;

        if (Mathf.Abs(k) > 1)
        {
            for (int y = (int)Mathf.Min(startY, endY); y < (int)Mathf.Max(startY, endY); y++)
            {
                int x = (int)((y - n) / k);
                if (y >= 0 && y < WORLD_SIZE_Y)
                {
                    new Road(world[x, y], true);
                }
                if (lastX != x)
                {
                    new Road(world[x, y - 1], true);
                }
                lastX = x;
                lastY = y;
            }
        }
        else
        {
            for (int x = (int)Mathf.Min(startX, endX); x < (int)Mathf.Max(startX, endX); x++)
            {
                int y = (int)(k * x + n);
                if (y >= 0 && y < WORLD_SIZE_Y)
                {
                    new Road(world[x, y], true);
                }
                if (lastY != y)
                {
                    new Road(world[x - 1, y], true);
                }
                lastX = x;
                lastY = y;
            }
        }
    }

    void SpawnRandomRoads()
    {
        Vector2 center = new Vector2(WORLD_SIZE_X / 2, WORLD_SIZE_Y / 2);

        Vector2 LastRoadPosition = center;
        for (int i = 0; i < 15; i++)
        {
            float radius = 50f;

            //int X = (int)roads[i].x;
            //int Y = (int)roads[i].y;

            int X;
            int Y;

            do
            {
                X = Random.Range(0, WORLD_SIZE_X);
                Y = Random.Range(0, WORLD_SIZE_Y);
            } while ((Vector2.Distance(new Vector2(X, Y), center) > radius) || (Vector2.Distance(new Vector2(X, Y), LastRoadPosition) > 30));

            float k = (Y - LastRoadPosition.y) / (((X - LastRoadPosition.x) == 0) ? 0.1f : (X - LastRoadPosition.x));
            float n = Y - (k * X);

            int lastX = (int)LastRoadPosition.x;
            int lastY = (int)LastRoadPosition.y;

            new Road(world[lastX, lastY], true);
            new Road(world[X, Y], true);

            if (Mathf.Abs(k) > 1)
            {
                for (int y = (int)Mathf.Min(LastRoadPosition.y, Y); y < (int)Mathf.Max(LastRoadPosition.y, Y); y++)
                {
                    int x = (int)((y - n) / k);
                    if (y >= 0 && y < WORLD_SIZE_Y)
                    {
                        new Road(world[x, y], true);
                    }
                    if (lastX != x)
                    {
                        new Road(world[x, y - 1], true);
                    }
                    lastX = x;
                    lastY = y;
                }
            }
            else
            {
                for (int x = (int)Mathf.Min(LastRoadPosition.x, X); x < (int)Mathf.Max(LastRoadPosition.x, X); x++)
                {
                    int y = (int)(k * x + n);
                    if (y >= 0 && y < WORLD_SIZE_Y)
                    {
                        new Road(world[x, y], true);
                    }
                    if (lastY != y)
                    {
                        new Road(world[x - 1, y], true);
                    }
                    lastX = x;
                    lastY = y;
                }
            }
            LastRoadPosition = new Vector2(X, Y);
        }
    }

    // Use this for initialization
    void Start()
    {
        CreateWorld();
    }

    public float spawnTimer = 5f;

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer += 5;

            GameObject[] unitPrefabs = new GameObject[] {
                Prefabs.obj.UnitCavalryBow,
                Prefabs.obj.UnitCavalrySword,
                Prefabs.obj.UnitCavalrySpear,
                Prefabs.obj.UnitInfantryBow,
                Prefabs.obj.UnitInfantrySword,
                Prefabs.obj.UnitInfantrySpear,
                Prefabs.obj.UnitMilitiaBow,
                Prefabs.obj.UnitMilitiaSword,
                Prefabs.obj.UnitMilitiaSpear
            };
            foreach (Tower tower in AllTowers)
            {
                if (tower.PlayerNumber != 1)
                {
                    if (Random.Range(0f, 1f) < 0.25f)
                    {
                        int count = 0;
                        foreach (Unit u in AllUnits)
                        {
                            if (u.TeamNumber == tower.PlayerNumber)
                            {
                                count++;
                            }
                        }

                        if (count < 20)
                        {
                            GameObject go = Instantiate(unitPrefabs[Random.Range(0, unitPrefabs.Length)]);
                            Unit u = go.GetComponent<Unit>();
                            u.Move(tower.Location, true);
                            u.TeamNumber = tower.PlayerNumber;
                        }
                    }
                }
            }
        }

    }


    public Tuple<uint, uint> WorldCoordsToGameCoords(Vector2 worldCoord)
    {
        int xi = Mathf.FloorToInt(worldCoord.x);
        int yi = Mathf.FloorToInt(worldCoord.y);

        if (xi < 0 || yi < 0)
        {
            return null;
        }

        uint x = (uint)xi;
        uint y = (uint)yi;

        if (CoordInWorld((int)x, (int)y))
        {
            //Coordinate is actually in the world, return it!.
            return new Tuple<uint, uint>(x, y);
        }
        return null;
    }

    public void INPUT_OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void INPUT_RestartLevel()
    {
        SceneManager.LoadScene("scene");
    }

}

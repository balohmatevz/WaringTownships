using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    public static float AI_TIMER = 0.5f;  //now often the unit's AI triggers

    public Tile Location;
    public UnitType TypeOfUnit;

    public string UnitName = "";
    public GameObject HealthRedGO;
    public GameObject HealthGreenGO;
    public Transform HealthGreenTransform;

    public int Attack;
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
                Destroy(unitGO);
                Location.Units.Remove(this);
                WorldController.obj.AllUnits.Remove(this);
                if (uiSelectButtonGO != null)
                {
                    Destroy(uiSelectButtonGO);
                }
            }
        }

    }

    public GameObject      unitGO;
    public SpriteRenderer  unitSR;
    public Transform       unitTransform;
    private int _teamNumber;
    public int TeamNumber
    {
        get
        {
            return _teamNumber;
        }
        set
        {
            _teamNumber = value;
            teamSR.color = Refs.obj.TeamColors[_teamNumber];
        }
    }

    public List<PathFinderNode> path;

    public GameObject selectionGO;
    public GameObject teamGO;
    public SpriteRenderer teamSR;

    float AITimer = AI_TIMER;

    public UnitAction Action;
    public float ActionDelay = 0;
    public float MoveDelay = 0.25f;

    public GameObject uiSelectButtonGO;

    protected virtual void Awake()
    {
        unitGO = this.gameObject;
        unitTransform = unitGO.transform;
        unitSR = unitGO.GetComponent<SpriteRenderer>();

        selectionGO = Instantiate(Prefabs.obj.Selection);
        selectionGO.transform.SetParent(unitTransform);
        selectionGO.transform.localPosition = Vector2.zero;
        selectionGO.SetActive(false);

        teamGO = Instantiate(Prefabs.obj.Team);
        teamGO.transform.SetParent(unitTransform);
        teamGO.transform.localPosition = Vector2.zero;
        teamSR = teamGO.GetComponent<SpriteRenderer>();

        HealthRedGO = Instantiate(Prefabs.obj.HealthRed);
        HealthRedGO.transform.SetParent(unitTransform);
        HealthRedGO.transform.localPosition = Vector2.zero;

        HealthGreenGO = Instantiate(Prefabs.obj.HealthGreen);
        HealthGreenGO.transform.SetParent(unitTransform);
        HealthGreenGO.transform.localPosition = Vector2.zero;
        HealthGreenTransform = HealthGreenGO.transform;


        WorldController.obj.AllUnits.Add(this);
        AITimer = Random.Range(0f, AI_TIMER);
    }

    public void InitiateMove(int direction)
    {
        switch (direction)
        {
            case 1:
                Action = UnitAction.WALK_NORTH;
                break;
            case 2:
                Action = UnitAction.WALK_SOUTH;
                break;
            case 4:
                Action = UnitAction.WALK_EAST;
                break;
            case 8:
                Action = UnitAction.WALK_WEST;
                break;
        }
        ActionDelay = MoveDelay;
    }

    public void Move(Tile newLoc, bool force = false)
    {
        if (newLoc == null)
        {
            return;
        }

        if (!force)
        {
            foreach (Unit u in newLoc.Units)
            {
                if (u.TeamNumber != this.TeamNumber)
                {
                    return;
                }
            }

            foreach (Building b in newLoc.Buildings)
            {
                if (b.PlayerNumber != this.TeamNumber && b.PlayerNumber != 0)
                {
                    return;
                }
            }
        }

        if (Location != null)
        {
            Location.Units.Remove(this);
        }
        Location = newLoc;
        Location.Units.Add(this);
        unitTransform.SetParent(Location.tileGO.transform);
        unitTransform.localPosition = new Vector2(0, 0);
        Action = UnitAction.NONE;

        if (path != null && path.Count > 0)
        {
            PathFinderNode pfn = path[0];
            if (WorldController.obj.world[pfn.X, pfn.Y] == Location.NORTH)
            {
                InitiateMove(1);
            }
            else if (WorldController.obj.world[pfn.X, pfn.Y] == Location.SOUTH)
            {
                InitiateMove(2);
            }
            else if (WorldController.obj.world[pfn.X, pfn.Y] == Location.EAST)
            {
                InitiateMove(4);
            }
            else if (WorldController.obj.world[pfn.X, pfn.Y] == Location.WEST)
            {
                InitiateMove(8);
            }
            path.Remove(pfn);
        }
    }

    float TimeToRethinkPosition = 1f;

    void Update()
    {
        TimeToRethinkPosition -= Time.deltaTime;
        if (Action != UnitAction.NONE)
        {
            ActionDelay -= Time.deltaTime;
            //Action being performed
            switch (Action)
            {
                case UnitAction.NONE:
                    unitTransform.localPosition = Vector2.zero;
                    break;
                case UnitAction.WALK_NORTH:
                    unitTransform.localPosition = Vector2.up * 0.33f;
                    foreach (Unit u in Location.NORTH.Units)
                    {
                        if (u.TeamNumber != this.TeamNumber)
                        {
                            u.Health -= Time.deltaTime * Attack;
                            ActionDelay = MoveDelay;
                            break;
                        }
                    }
                    foreach (Building b in Location.NORTH.Buildings)
                    {
                        if (b.PlayerNumber != this.TeamNumber && b.PlayerNumber != 0)
                        {
                            b.Health -= Time.deltaTime * Attack;
                            ActionDelay = MoveDelay;
                            break;
                        }
                    }
                    break;
                case UnitAction.WALK_SOUTH:
                    unitTransform.localPosition = Vector2.down * 0.33f;
                    foreach (Unit u in Location.SOUTH.Units)
                    {
                        if (u.TeamNumber != this.TeamNumber)
                        {
                            u.Health -= Time.deltaTime * Attack;
                            ActionDelay = MoveDelay;
                            break;
                        }
                    }
                    foreach (Building b in Location.SOUTH.Buildings)
                    {
                        if (b.PlayerNumber != this.TeamNumber && b.PlayerNumber != 0)
                        {
                            b.Health -= Time.deltaTime * Attack;
                            ActionDelay = MoveDelay;
                            break;
                        }
                    }
                    break;
                case UnitAction.WALK_EAST:
                    unitTransform.localPosition = Vector2.right * 0.33f;
                    foreach (Unit u in Location.EAST.Units)
                    {
                        if (u.TeamNumber != this.TeamNumber)
                        {
                            u.Health -= Time.deltaTime * Attack;
                            ActionDelay = MoveDelay;
                            break;
                        }
                    }
                    foreach (Building b in Location.EAST.Buildings)
                    {
                        if (b.PlayerNumber != this.TeamNumber && b.PlayerNumber != 0)
                        {
                            b.Health -= Time.deltaTime * Attack;
                            ActionDelay = MoveDelay;
                            break;
                        }
                    }
                    break;
                case UnitAction.WALK_WEST:
                    unitTransform.localPosition = Vector2.left * 0.33f;
                    foreach (Unit u in Location.WEST.Units)
                    {
                        if (u.TeamNumber != this.TeamNumber)
                        {
                            u.Health -= Time.deltaTime * Attack;
                            ActionDelay = MoveDelay;
                            break;
                        }
                    }
                    foreach (Building b in Location.WEST.Buildings)
                    {
                        if (b.PlayerNumber != this.TeamNumber && b.PlayerNumber != 0)
                        {
                            b.Health -= Time.deltaTime * Attack;
                            ActionDelay = MoveDelay;
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }

            if (ActionDelay <= 0)
            {
                //perform action
                switch (Action)
                {
                    case UnitAction.WALK_NORTH:
                        Move(Location.NORTH);
                        break;
                    case UnitAction.WALK_SOUTH:
                        Move(Location.SOUTH);
                        break;
                    case UnitAction.WALK_EAST:
                        Move(Location.EAST);
                        break;
                    case UnitAction.WALK_WEST:
                        Move(Location.WEST);
                        break;
                    default:
                        //Action = UnitAction.NONE;
                        break;
                }
            }
        }

        AITimer -= Time.deltaTime;
        if (Action == UnitAction.NONE)
        {
            if (AITimer <= 0)
            {
                AITimer = AI_TIMER;
                OnAI();
            }
        }
    }

    public virtual void OnAI()
    {
        if (Action == UnitAction.NONE)
        {
            if (path != null && path.Count > 0)
            {
                if (WorldController.obj.world[path[0].X, path[0].Y] == Location.NORTH)
                {
                    InitiateMove(1);
                }
                else if (WorldController.obj.world[path[0].X, path[0].Y] == Location.SOUTH)
                {
                    InitiateMove(2);
                }
                else
                if (WorldController.obj.world[path[0].X, path[0].Y] == Location.EAST)
                {
                    InitiateMove(4);
                }
                else
                if (WorldController.obj.world[path[0].X, path[0].Y] == Location.WEST)
                {
                    InitiateMove(8);
                }
                else
                {
                    path.RemoveAt(0);
                }
            }

            Unit ClosetsUnit = null;
            float ClosestDistance = float.MaxValue;

            foreach (Unit u in WorldController.obj.AllUnits)
            {
                if (u.TeamNumber == this.TeamNumber)
                {
                    continue;
                }

                if (Vector2.Distance(new Vector2(Location.X, Location.Y), new Vector2(u.Location.X, u.Location.Y)) < ClosestDistance)
                {
                    ClosestDistance = Vector2.Distance(new Vector2(Location.X, Location.Y), new Vector2(u.Location.X, u.Location.Y));
                    ClosetsUnit = u;
                }
            }

            if (ClosestDistance < 4)
            {
                List<PathFinderNode> path2 = WorldController.obj.PF.FindPath(Location, ClosetsUnit.Location);
                if (path2 != null && path2.Count > 1)
                {
                    List<PathFinderNode> newPath = new List<PathFinderNode>();
                    foreach (PathFinderNode pfn in path2)
                    {
                        newPath.Add(pfn);
                    }
                    this.path = newPath;
                    if (WorldController.obj.world[path2[1].X, path2[0].Y] == Location.NORTH)
                    {
                        InitiateMove(1);
                    }
                    if (WorldController.obj.world[path2[1].X, path2[0].Y] == Location.SOUTH)
                    {
                        InitiateMove(2);
                    }
                    if (WorldController.obj.world[path2[1].X, path2[0].Y] == Location.EAST)
                    {
                        InitiateMove(4);
                    }
                    if (WorldController.obj.world[path2[1].X, path2[0].Y] == Location.WEST)
                    {
                        InitiateMove(8);
                    }
                }
            }

            if (Action == UnitAction.NONE)
            {
                if (TimeToRethinkPosition <= 0)
                {
                    TimeToRethinkPosition = 20;
                    if (this.TeamNumber != 1)
                    {
                        foreach (Tower tower in WorldController.obj.AllTowers)
                        {
                            if (tower.PlayerNumber == this.TeamNumber)
                            {
                                Vector2 target = new Vector2(tower.Location.X + Random.Range(-15, 15), tower.Location.Y + Random.Range(-15, 15));

                                if (WorldController.obj.CoordInWorld((int)target.x, (int)target.y))
                                {
                                    if (WorldController.obj.world[(int)target.x, (int)target.y].GetTileCost() == 0)
                                    {
                                        continue;
                                    }

                                    Tile t = WorldController.obj.world[(int)target.x, (int)target.y];
                                    List<PathFinderNode> path2 = WorldController.obj.PF.FindPath(Location, t);
                                    if (path2 != null && path2.Count > 1)
                                    {
                                        List<PathFinderNode> newPath = new List<PathFinderNode>();
                                        foreach (PathFinderNode pfn in path2)
                                        {
                                            newPath.Add(pfn);
                                        }
                                        this.path = newPath;
                                        if (WorldController.obj.world[path2[1].X, path2[0].Y] == Location.NORTH)
                                        {
                                            InitiateMove(1);
                                        }
                                        if (WorldController.obj.world[path2[1].X, path2[0].Y] == Location.SOUTH)
                                        {
                                            InitiateMove(2);
                                        }
                                        if (WorldController.obj.world[path2[1].X, path2[0].Y] == Location.EAST)
                                        {
                                            InitiateMove(4);
                                        }
                                        if (WorldController.obj.world[path2[1].X, path2[0].Y] == Location.WEST)
                                        {
                                            InitiateMove(8);
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    public virtual void Select()
    {
        if (TeamNumber != 1)
        {
            return;
        }

        selectionGO.SetActive(true);
    }

    public virtual void Deselect()
    {
        if (TeamNumber != 1)
        {
            return;
        }
        selectionGO.SetActive(false);
    }

    public void OnRightClick(Tile overTile)
    {
        List<PathFinderNode> path = WorldController.obj.PF.FindPath(Location, overTile);
        if (path != null && path.Count > 0)
        {
            List<PathFinderNode> newPath = new List<PathFinderNode>();
            foreach (PathFinderNode pfn in path)
            {
                newPath.Add(pfn);
            }
            this.path = newPath;
            if (WorldController.obj.world[path[0].X, path[0].Y] == Location.NORTH)
            {
                InitiateMove(1);
            }
            if (WorldController.obj.world[path[0].X, path[0].Y] == Location.SOUTH)
            {
                InitiateMove(2);
            }
            if (WorldController.obj.world[path[0].X, path[0].Y] == Location.EAST)
            {
                InitiateMove(4);
            }
            if (WorldController.obj.world[path[0].X, path[0].Y] == Location.WEST)
            {
                InitiateMove(8);
            }
        }
    }

    public void CreateUISelectionButton(GameObject prefab)
    {
        if (this.TeamNumber != 1)
        {
            return;
        }

        GameObject uiGO = Instantiate(prefab);
        uiGO.transform.SetParent(Refs.obj.UnitUIHolder);
        UnitSelectionButton usb = uiGO.GetComponent<UnitSelectionButton>();
        usb.UnitToSelect = this;
        uiSelectButtonGO = uiGO;
    }
}

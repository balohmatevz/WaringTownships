using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Refs : MonoBehaviour
{
    public static Refs obj;

    public GameObject WorldAnchor;


    [Header("Buildings")]
    public Sprite TerrainGrass;
    public Sprite TerrainSand;
    public Sprite TerrainWater;
    public Sprite TerrainTrees;
    public Sprite BuildingHouse;
    public Sprite BuildingTower;
    public Sprite BuildingTemple;
    public Sprite BuildingStable;
    public Sprite BuildingBarracks;
    public Sprite BuildingArcheryRange;

    public Sprite[] Road;

    [Header("Units")]
    public Sprite MilitiaSpear;
    public Sprite MilitiaSword;
    public Sprite MilitiaBow;
    public Sprite InfantrySpear;
    public Sprite InfantrySword;
    public Sprite InfantryBow;
    public Sprite CavalrySpear;
    public Sprite CavalrySword;
    public Sprite CavalryBow;

    [Header("UI")]
    public RectTransform UnitUIHolder;
    public GameObject UIArcheryRange;
    public GameObject UIBarracks;
    public GameObject UIStable;
    public GameObject UISelectedUnit;
    public Text UISelectedUnitName;
    public Text UISelectedUnitHP;
    public Text UISelectedUnitAttack;

    public Color[] TeamColors;

    public GameObject Loading;
    public GameObject Help;

    void Awake()
    {
        obj = this;
    }
}

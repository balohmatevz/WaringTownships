using UnityEngine;
using System.Collections;

public class Prefabs : MonoBehaviour
{
    public static Prefabs obj;

    public GameObject Tile;
    public GameObject Building;
    public GameObject Road;
    public GameObject UnitMilitiaSpear;
    public GameObject UnitMilitiaSword;
    public GameObject UnitMilitiaBow;
    public GameObject UnitInfantrySpear;
    public GameObject UnitInfantrySword;
    public GameObject UnitInfantryBow;
    public GameObject UnitCavalrySpear;
    public GameObject UnitCavalrySword;
    public GameObject UnitCavalryBow;

    public GameObject Selection;
    public GameObject Team;
    public GameObject HealthRed;
    public GameObject HealthGreen;


    public GameObject UIMilitiaSpear;
    public GameObject UIMilitiaSword;
    public GameObject UIMilitiaBow;
    public GameObject UIInfantrySpear;
    public GameObject UIInfantrySword;
    public GameObject UIInfantryBow;
    public GameObject UICavalrySpear;
    public GameObject UICavalrySword;
    public GameObject UICavalryBow;

    void Awake()
    {
        obj = this;
    }
}

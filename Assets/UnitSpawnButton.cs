using UnityEngine;
using System.Collections;

public class UnitSpawnButton : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void INPUT_SpawnUnit(GameObject prefab)
    {
        GameObject go = Instantiate(prefab);
        Unit u = go.GetComponent<Unit>();
        u.Move(WorldController.obj.SelectedBuilding.Location, true);
        u.TeamNumber = 1;
    }
}

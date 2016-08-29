using UnityEngine;
using System.Collections;

public class UnitSelectionButton : MonoBehaviour
{

    public Unit UnitToSelect;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void INPUT_SelectUnit()
    {
        if (UnitToSelect != null)
        {
            WorldController.obj.SelectedUnit = UnitToSelect;
        }
    }
}

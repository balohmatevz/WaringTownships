using UnityEngine;
using System.Collections;

public class InfantryBow : Unit
{
    protected override void Awake()
    {
        base.Awake();
        unitSR.sprite = Refs.obj.InfantryBow;
    }

    void Start()
    {
        MoveDelay = 0.25f;
        InitialHealth = 10;
        Health = InitialHealth;
        Attack = 4;
        CreateUISelectionButton(Prefabs.obj.UIInfantryBow);
        UnitName = "Infantry Archer";
    }

    public override void OnAI()
    {
        base.OnAI();
    }
}

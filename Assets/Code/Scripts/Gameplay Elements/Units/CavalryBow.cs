using UnityEngine;
using System.Collections;

public class CavalryBow : Unit
{
    protected override void Awake()
    {
        base.Awake();
        unitSR.sprite = Refs.obj.CavalryBow;
    }

    void Start()
    {
        MoveDelay = 0.10f;
        InitialHealth = 15;
        Health = InitialHealth;
        Attack = 7;
        CreateUISelectionButton(Prefabs.obj.UICavalryBow);
        UnitName = "Cavalry Archer";
    }

    public override void OnAI()
    {
        base.OnAI();
    }
}

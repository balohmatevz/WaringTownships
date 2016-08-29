using UnityEngine;
using System.Collections;

public class InfantrySpear : Unit
{
    protected override void Awake()
    {
        base.Awake();
        unitSR.sprite = Refs.obj.InfantrySpear;
    }

    void Start()
    {
        MoveDelay = 0.25f;
        InitialHealth = 5;
        Health = InitialHealth;
        Attack = 5;
        CreateUISelectionButton(Prefabs.obj.UIInfantrySpear);
        UnitName = "Infantry Spearman";
    }

    public override void OnAI()
    {
        base.OnAI();
    }
}

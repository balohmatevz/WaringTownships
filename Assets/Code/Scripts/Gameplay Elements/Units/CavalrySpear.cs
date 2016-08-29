using UnityEngine;
using System.Collections;

public class CavalrySpear : Unit
{
    protected override void Awake()
    {
        base.Awake();
        unitSR.sprite = Refs.obj.CavalrySpear;
    }

    void Start()
    {
        MoveDelay = 0.10f;
        InitialHealth = 10;
        Health = InitialHealth;
        Attack = 8;
        CreateUISelectionButton(Prefabs.obj.UICavalrySpear);
        UnitName = "Cavalry Spearman";
    }

    public override void OnAI()
    {
        base.OnAI();
    }
}

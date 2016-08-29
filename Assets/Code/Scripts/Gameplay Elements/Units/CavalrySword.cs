using UnityEngine;
using System.Collections;

public class CavalrySword : Unit
{
    protected override void Awake()
    {
        base.Awake();
        unitSR.sprite = Refs.obj.CavalrySword;
    }

    void Start()
    {
        MoveDelay = 0.10f;
        InitialHealth = 20;
        Health = InitialHealth;
        Attack = 5;
        CreateUISelectionButton(Prefabs.obj.UICavalrySword);
        UnitName = "Cavalry Swordsman";
    }

    public override void OnAI()
    {
        base.OnAI();
    }
}

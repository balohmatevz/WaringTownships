using UnityEngine;
using System.Collections;

public class InfantrySword : Unit
{
    protected override void Awake()
    {
        base.Awake();
        unitSR.sprite = Refs.obj.InfantrySword;
    }

    void Start()
    {
        MoveDelay = 0.25f;
        InitialHealth = 10;
        Health = InitialHealth;
        Attack = 2;
        CreateUISelectionButton(Prefabs.obj.UIInfantrySword);
        UnitName = "Infantry Swordsman";
    }

    public override void OnAI()
    {
        base.OnAI();
    }
}

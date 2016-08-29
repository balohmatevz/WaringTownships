using UnityEngine;
using System.Collections;

public class MilitiaSword : Unit
{
    protected override void Awake()
    {
        base.Awake();
        unitSR.sprite = Refs.obj.MilitiaSword;
    }

    void Start()
    {
        MoveDelay = 0.25f;
        InitialHealth = 5;
        Health = InitialHealth;
        Attack = 1;
        CreateUISelectionButton(Prefabs.obj.UIMilitiaSword);
        UnitName = "Militia Swordsman";
    }

    public override void OnAI()
    {
        base.OnAI();
    }
}

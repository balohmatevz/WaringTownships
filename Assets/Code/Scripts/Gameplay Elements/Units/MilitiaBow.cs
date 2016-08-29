using UnityEngine;
using System.Collections;

public class MilitiaBow : Unit
{
    protected override void Awake()
    {
        base.Awake();
        unitSR.sprite = Refs.obj.MilitiaBow;
    }

    void Start()
    {
        MoveDelay = 0.25f;
        InitialHealth = 4;
        Health = InitialHealth;
        Attack = 2;
        CreateUISelectionButton(Prefabs.obj.UIMilitiaBow);
        UnitName = "Militia Archer";
    }

    public override void OnAI()
    {
        base.OnAI();
    }
}

using UnityEngine;
using System.Collections;

public class MilitiaSpear : Unit
{
    protected override void Awake()
    {
        base.Awake();
        unitSR.sprite = Refs.obj.MilitiaSpear;
    }

    void Start()
    {
        MoveDelay = 0.25f;
        InitialHealth = 3;
        Health = InitialHealth;
        Attack = 2;
        CreateUISelectionButton(Prefabs.obj.UIMilitiaSpear);
        UnitName = "Militia Spearman";
    }

    public override void OnAI()
    {
        base.OnAI();
    }
}

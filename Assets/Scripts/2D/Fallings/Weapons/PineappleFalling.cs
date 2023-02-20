using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleFalling : WeaponFallings
{
    public int HashName { get; protected set; }

    public override Color GetColor()
    {
        return Color.white;
    }

    private void Start()
    {
        string name = "Pineapple";
        HashName = name.GetHashCode();
    }
}

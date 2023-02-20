using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaFalling : WeaponFallings
{
    public int HashName { get; protected set; }

    public override Color GetColor()
    {
        return Color.white;
    }

    private void Start()
    {
        string name = "Banana";
        HashName = name.GetHashCode();
    }
}

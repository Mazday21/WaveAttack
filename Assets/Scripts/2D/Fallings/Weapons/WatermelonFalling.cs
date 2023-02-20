using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

public class WatermelonFalling : WeaponFallings
{
    public int HashName { get ; protected set ; }

    public override Color GetColor()
    {
        return Color.white;
    }

    private void Start()
    {
        string name = "Watermelon";
        HashName = name.GetHashCode();
    }
}

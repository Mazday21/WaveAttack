using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaPeel : Weapon
{
    public int HashName { get; protected set; }

    private void Start()
    {
        string name = "Banana";
        HashName = name.GetHashCode();
    }
}

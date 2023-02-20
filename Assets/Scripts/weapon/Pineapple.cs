using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pineapple : Weapon
{
    public int HashName { get; protected set; }

    private void Start()
    {
        string name = "Pineapple";
        HashName = name.GetHashCode();
    }
}

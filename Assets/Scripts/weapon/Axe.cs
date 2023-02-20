using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon
{
    public int HashName { get; protected set; }

    private void Start()
    {
        string name = "Axe";
        HashName = name.GetHashCode();
    }
}

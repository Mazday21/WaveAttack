using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon : Weapon
{
    public int HashName { get; protected set; }

    private void OnEnable()
    {
        _damage = 2;
    }

    private void Start()
    {
        string name = "Watermelon";
        HashName = name.GetHashCode();
    }
}

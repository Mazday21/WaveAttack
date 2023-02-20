using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusFiveFalling : PowerChanges
{
    public override Color GetColor()
    {
        return Color.red;
    }

    public override int PowerChange(int power)
    {
        return power - 2;
    }
}

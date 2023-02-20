using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusFiveFalling : PowerChanges
{
    public override Color GetColor()
    {
        return Color.green;
    }

    public override int PowerChange(int power)
    {
        return power + 2;
    }
}

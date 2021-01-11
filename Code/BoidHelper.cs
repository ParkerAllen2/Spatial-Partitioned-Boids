using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidHelper
{
    const int numViewDirections = 300 / 5;
    public static readonly Vector3[] directions;
    public static readonly Vector3[] offsets;

    static BoidHelper()
    {
        directions = new Vector3[BoidHelper.numViewDirections];

        float angleIncrement = 5;

        for (int i = 0; i < numViewDirections / angleIncrement; i++)
        {
            int n = ((i % 2 == 1) ? -1 : 1) * ((i + 1) / 2);
            directions[i] = Quaternion.Euler(0, 0, n * angleIncrement) * Vector2.up;
        }

        offsets = new Vector3[2000];
        for(int i = 0; i < offsets.Length; i++)
        {
            offsets[i] = (Vector3)Random.insideUnitCircle;
        }
    }

    public static Vector3 GetOffset()
    {
        return offsets[Random.Range(0, 2000)];
    }
}

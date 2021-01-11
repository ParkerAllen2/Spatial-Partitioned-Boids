using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBorder : MonoBehaviour
{
    [SerializeField] BoxCollider2D[] borders;

    public void Initialize(Vector3 origin, float size)
    {
        //top
        borders[0].offset = new Vector2(0, origin.y);
        borders[0].size = new Vector2(size, 1);

        //bot
        borders[1].offset = new Vector2(0, -origin.y);
        borders[1].size = new Vector2(size, 1);

        //left
        borders[2].offset = new Vector2(-origin.x, 0);
        borders[2].size = new Vector2(1, size);

        //right
        borders[3].offset = new Vector2(origin.x, 0);
        borders[3].size = new Vector2(1, size);
    }
}

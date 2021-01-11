using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public bool followMouse;
    private bool lockPos;

    // Update is called once per frame
    void Update()
    {
        if (followMouse && !lockPos)
        {
            Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            camPos.z = 0;
            transform.position = camPos;
        }
        if (followMouse && Input.GetMouseButton(1))
        {
            lockPos = !lockPos;
        }
    }

    public void SetFollowMouse(bool b)
    {
        followMouse = b;
    }

    public void ResetPosition()
    {
        transform.position = Vector3.zero;
    }
}

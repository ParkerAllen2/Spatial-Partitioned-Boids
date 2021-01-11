using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorMaker : MonoBehaviour
{
    public InputField r, g, b;
    public Image display;
    public UIController uIController;

    public Color c;
    public bool background;

    public void Awake()
    {
        r.text = (int)(c.r * 255) + "";
        g.text = (int)(c.g * 255) + "";
        b.text = (int)(c.b * 255) + "";
        SetColor();
    }

    public void SetColor()
    {
        try
        {
            c.r = int.Parse(r.text) / 255f;
        }
        catch { }
        try
        {
            c.g = int.Parse(g.text) / 255f;
        }
        catch { }
        try
        {
            c.b = int.Parse(b.text) / 255f;
        }
        catch { }

        display.color = c;
        if (background)
            Camera.main.backgroundColor = c;
        else
            uIController.UpdateSpawnerColor(c);
    }
}

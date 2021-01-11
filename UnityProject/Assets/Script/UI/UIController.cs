using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public FollowMouse followMouse;
    public Spawner2 spawner;
    public BoidSettings boidSettings;
    public BoidManager2 boidManager;

    public Text NumOfBoidsT;
    public InputField spawnAmount;
    public BlockPlacer blockPlacer;
    public BoidSettingsUI boidSettingsUI;
    public GameObject spawnerPanel;

    public void Awake()
    {
        boidSettingsUI.boidSettings = boidSettings;
        boidSettingsUI.followMouse = followMouse;
    }

    public void OpenSettings(int i)
    {
        if(i == 0)
        {
            spawnerPanel.SetActive(true);
            OpenSpawnerSettings();
            boidSettingsUI.gameObject.SetActive(false);
            blockPlacer.gameObject.SetActive(false);
        }
        if (i == 1)
        {
            spawnerPanel.SetActive(false);
            OpenBoidSettings();
            blockPlacer.gameObject.SetActive(false);
        }
        if (i == 2)
        {
            spawnerPanel.SetActive(false);
            boidSettingsUI.gameObject.SetActive(false);
            OpenBlockPlacer();
        }
        if (i == 3)
        {
            spawnerPanel.SetActive(false);
            boidSettingsUI.gameObject.SetActive(false);
            blockPlacer.gameObject.SetActive(false);
        }
    }

    public void OpenBoidSettings()
    {
        boidSettingsUI.gameObject.SetActive(true);
        boidSettingsUI.SetValues();
    }

    public void OpenSpawnerSettings()
    {
        spawnAmount.text = spawner.spawnCount + "";
    }

    public void OpenBlockPlacer()
    {
        blockPlacer.gameObject.SetActive(true);
        blockPlacer.SetValues();
    }

    public void UpdateNumOfBoids()
    {
        NumOfBoidsT.text = "Boids: " + boidManager.numBoids;
    }

    public void UpdateSpawnAmount()
    {
        try
        {
            spawner.spawnCount = int.Parse(spawnAmount.text);
        }
        catch { print("hit"); }
    }

    public void UpdateSpawnerColor(Color c)
    {
        spawner.currentColor = c;
    }

    public void ToggleBoidSpawn(bool b)
    {
        spawner.spawnBoids = !b;
    }
}

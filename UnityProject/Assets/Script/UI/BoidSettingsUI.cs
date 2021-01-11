using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoidSettingsUI : MonoBehaviour
{
    public BoidSettings boidSettings;
    public FollowMouse followMouse;

    public InputField[] inputs;
    public Toggle followMouseT;

    public void Start()
    {
        inputs = GetComponentsInChildren<InputField>();
        SetValues();
    }

    public void SetValues()
    {
        inputs = GetComponentsInChildren<InputField>();
        inputs[0].text = boidSettings.minSpeed + "";
        inputs[1].text = boidSettings.maxSpeed + "";
        inputs[2].text = boidSettings.perceptionRadius + "";
        inputs[3].text = boidSettings.avoidanceRadius + "";
        inputs[4].text = boidSettings.maxSteerForce + "";
        inputs[5].text = boidSettings.alignWeight + "";
        inputs[6].text = boidSettings.cohesionWeight + "";
        inputs[7].text = boidSettings.seperateWeight + "";
        inputs[8].text = boidSettings.targetWeight + "";
        followMouseT.isOn = followMouse.followMouse;
    }

    public void SetBoidSettiongs()
    {
        try
        {
            boidSettings.minSpeed = float.Parse(inputs[0].text);
            boidSettings.maxSpeed = float.Parse(inputs[1].text);
            boidSettings.perceptionRadius = float.Parse(inputs[2].text);
            boidSettings.avoidanceRadius = float.Parse(inputs[3].text);
            boidSettings.maxSteerForce = float.Parse(inputs[4].text);
            boidSettings.alignWeight = float.Parse(inputs[5].text);
            boidSettings.cohesionWeight = float.Parse(inputs[6].text);
            boidSettings.seperateWeight = float.Parse(inputs[7].text);
            boidSettings.targetWeight = float.Parse(inputs[8].text);
            followMouse.followMouse = followMouseT.isOn;
        }
        catch { }
    }    
}

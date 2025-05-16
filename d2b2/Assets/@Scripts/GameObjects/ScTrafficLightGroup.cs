using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

[Serializable]
public class ScTrafficLightGroup
{
    [LabelText("group")]
    public List<ScTrafficLight> items = new List<ScTrafficLight>();



    public void SetLight(ScDefine.ScTrafficLightType lightType)
    {
        if (items?.Count > 0)
        {
            foreach (ScTrafficLight trafficLight in items)
                trafficLight.SetLight(lightType);
        }
    }

    public void InvertColor()
    {
        if (items?.Count > 0)
        {
            foreach (ScTrafficLight trafficLight in items)
                trafficLight.InvertColor();
        }
    }
}

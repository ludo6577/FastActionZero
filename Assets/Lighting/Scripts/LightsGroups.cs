using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class LightsGroups
{
    [SerializeField]
    public List<LightScript> Lights;

    public LightsGroups()
    {
        Lights = new List<LightScript>();
    }
}

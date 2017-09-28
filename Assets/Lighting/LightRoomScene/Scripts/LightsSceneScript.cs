using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LightsSceneScript : MonoBehaviour {

    public int CurrentScenario = 0;
    private int _currentScenario;

    [Range(0.01f, 1f)]
    public float IntensityChangeSpeed = 0.1f;
    private float _intensityChangeSpeed;

    public List<Scenario> Scenarios;

    private LightScript[] lights;

	// Use this for initialization
	void Start () {
        //lights = GetComponentsInChildren<LightScript>();

        //var i = 0;
        //foreach (var light in lights)
        //{
        //    if (light == null)
        //        continue;
        //    light.Intensity = Scenarios[CurrentScenario].LightsIntensity[i];
        //    i++;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //if (_currentScenario != CurrentScenario)
        //{
        //    _currentScenario = CurrentScenario;
        //    if (CurrentScenario >= Scenarios.Count)
        //        return;

        //    var i = 0;
        //    foreach (var light in lights)
        //    {
        //        light.Intensity = Scenarios[CurrentScenario].LightsIntensity[i];
        //        i++;
        //    }
        //}

        //if (_intensityChangeSpeed != IntensityChangeSpeed)
        //{
        //    _intensityChangeSpeed = IntensityChangeSpeed;
        //    foreach (var light in lights)
        //    {
        //        light.IntensityChangeSpeed = IntensityChangeSpeed;
        //    }
        //}
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Scenario
{
    [SerializeField]
    public string ScenarioName;

    [SerializeField]
    [Range(0, 20)]
    public float LightsIntensity;

    [SerializeField]
    public Color LightsColor;
}


public class SceneManagement : MonoBehaviour
{
    public Transform LightSelection;
    public Transform GroupSelection;
    public Transform SceneSelection;

    public List<Button> GroupButtons;

    public List<LightScript> Lights;
    public List<Scenario> Scenarios;

    private LightsGroups[] Groups;
    private int currentLightIndex;
    private int currentGroupIndex;
    private int currentSceneIndex;

    // 1) We assign all lights to a choosen group (the light is flickering)
    public void LightGroupSelectClicked(int index)
    {
        Groups[index - 1].Lights.Add(Lights[currentLightIndex]);
        FlashNextLight();

        if (Lights.Count == 0)
        { 
            LightSelection.gameObject.SetActive(false);
            GroupSelection.gameObject.SetActive(true);
        }
    }

    private void FlashNextLight()
    {
        var light = Lights[currentLightIndex];
        light.Flash(false);
        Lights.Remove(light);

        if (Lights.Count > 0)
        {
            currentLightIndex = UnityEngine.Random.Range(0, Lights.Count);
            Lights[currentLightIndex].Flash(true);
        }
    }

    // 2) We choose a group
    public void GroupClicked(int index)
    {
        currentGroupIndex = index;

        invertScene();
    }

    // 3) Assign a scene to the previously selected group
    public void SceneClicked(int index)
    {
        currentSceneIndex = index;

        var lightButton = GroupButtons[currentGroupIndex - 1];
        var lights = Groups[currentGroupIndex - 1].Lights;
        var scenario = Scenarios[currentSceneIndex - 1];

        lightButton.GetComponent<Image>().color = new Color(scenario.LightsColor.r, scenario.LightsColor.g, scenario.LightsColor.b, 0.25f);

        foreach (var light in lights)
        {
            light.Color = scenario.LightsColor;
            light.Intensity = scenario.LightsIntensity;
        }

        invertScene();
    }
    
    private void invertScene()
    {
        var ligthActive = LightSelection.gameObject.activeSelf;
        LightSelection.gameObject.SetActive(!ligthActive);
        SceneSelection.gameObject.SetActive(ligthActive);
    }


    
    void Start () {
        Groups = new LightsGroups[4];
        for(var i=0; i<Groups.Length; i++)
        {
            Groups[i] = new LightsGroups();
        }

        LightSelection.gameObject.SetActive(true);
        GroupSelection.gameObject.SetActive(false);
        SceneSelection.gameObject.SetActive(false);

        foreach (var light in GroupButtons)
        {
            light.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.25f);
        }

        Lights[0].Flash(true);
    }
	
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
	}
}

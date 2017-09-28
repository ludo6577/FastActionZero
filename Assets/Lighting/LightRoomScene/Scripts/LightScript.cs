using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {

    [HideInInspector]
    public float Intensity;
    [HideInInspector]
    public float IntensityChangeSpeed = 0.1f;

    [HideInInspector]
    public Color Color;
    [HideInInspector]
    public float ColorChangeSpeed = 0.1f;

    private Light light;
    private bool isFlashing;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        light.intensity = 0f;
        Intensity = 0f;
        Color = light.color;

        isFlashing = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (isFlashing)
            return;

		if(Mathf.Abs(light.intensity - Intensity) >= 0.0001f)
        {
            light.intensity = Mathf.Lerp(light.intensity, Intensity, IntensityChangeSpeed);
        }

        if (light.color != Color)
        {
            light.color = Color.Lerp(light.color, Color, ColorChangeSpeed);
        }
    }

    public void Flash(bool flash)
    {
        isFlashing = flash;
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        while (isFlashing)
        {
            light.intensity = light.intensity==0f ? 1f : 0f;
            yield return new WaitForSeconds(1);
        }
    }
}

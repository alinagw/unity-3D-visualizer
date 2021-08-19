using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringLightsController : MonoBehaviour
{
    public GameObject lightbulb;

    private Material lightbulbMaterial;
    private GameObject[] lightbulbLights;


    // Start is called before the first frame update
    void Start()
    {
        if (lightbulb == null) GameObject.FindWithTag("StringLightsBulb");
        lightbulbMaterial = lightbulb.GetComponent<Renderer>().sharedMaterial;
        lightbulbLights = GameObject.FindGameObjectsWithTag("StringLightsLight");
    }

    public void ToggleLights(bool isOn) {
        if (isOn) lightbulbMaterial.EnableKeyword("_EMISSION");
        else lightbulbMaterial.DisableKeyword("_EMISSION");
        foreach (GameObject light in lightbulbLights) {
            light.SetActive(isOn);
        }
    }
}

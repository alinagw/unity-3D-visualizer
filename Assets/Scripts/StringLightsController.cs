using UnityEngine;

// Handles turning string lights on/off
public class StringLightsController : MonoBehaviour
{
    // Any one "Bulb" object on a string light lightbulb (used to get material)
    public GameObject lightbulb;

    // Bulb material
    private Material lightbulbMaterial;
    // Array of the active lightbulb point lights
    private GameObject[] lightbulbLights;

    private bool m_stringLightsActive;

    public void ToggleLights(bool isOn) {
        // Enable/disable emissive property on lightbulb material
        if (isOn) lightbulbMaterial.EnableKeyword("_EMISSION");
        else lightbulbMaterial.DisableKeyword("_EMISSION");
        
        // Enable/disable all point lights
        foreach (GameObject light in lightbulbLights) {
            light.SetActive(isOn);
        }

        m_stringLightsActive = isOn;
    }

    void Awake()
    {
        if (lightbulb == null) GameObject.FindWithTag("StringLightsBulb");
        lightbulbMaterial = lightbulb.GetComponent<Renderer>().sharedMaterial;
        lightbulbLights = GameObject.FindGameObjectsWithTag("StringLightsLight");

        ToggleLights(false);
    }
}

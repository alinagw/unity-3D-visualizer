using UnityEngine;

public class StringLightsController : MonoBehaviour
{
    public GameObject lightbulb;

    private Material lightbulbMaterial;
    private GameObject[] lightbulbLights;

    private bool m_stringLightsActive;
    public bool isOn { get { return m_stringLightsActive; } }

    public void ToggleLights(bool isOn) {
        if (isOn) lightbulbMaterial.EnableKeyword("_EMISSION");
        else lightbulbMaterial.DisableKeyword("_EMISSION");
        foreach (GameObject light in lightbulbLights) {
            light.SetActive(isOn);
        }

        m_stringLightsActive = isOn;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (lightbulb == null) GameObject.FindWithTag("StringLightsBulb");
        lightbulbMaterial = lightbulb.GetComponent<Renderer>().sharedMaterial;
        lightbulbLights = GameObject.FindGameObjectsWithTag("StringLightsLight");
    }
}

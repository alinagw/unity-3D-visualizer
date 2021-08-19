using UnityEngine;

public class StateManager : MonoBehaviour
{
    public OptionsManager optionsManager;
    public ModelController modelController;
    public GameObject environmentLight;
    public StringLightsController stringLightsController;
    public GameObject fireflies;

    private GameObject m_activeModel;
    private Material m_activeMaterial;
    private TimeOfDay m_activeTime;
    private bool m_stringLightsActive;
    private bool m_firefliesActive;

    public void SetModel(GameObject prefab)
    {
        Helper.DestroyChildren(modelController.gameObject);
        Helper.SpawnPrefab(prefab, modelController.gameObject);
        m_activeModel = prefab;
        SetMaterial(m_activeMaterial);
    }

    public void SetMaterial(Material newMaterial)
    {
        m_activeMaterial = newMaterial;
        if (newMaterial == null) newMaterial = GetOriginalModelMat(m_activeModel);

        MeshRenderer[] renderers = modelController.gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            if (renderer) renderer.material = newMaterial;
        }
    }

    public void SetTimeOfDay(TimeOfDay newTime)
    {
        RenderSettings.skybox = newTime.Skybox;

        Helper.DestroyChildren(environmentLight);
        Helper.SpawnPrefab(newTime.Light, environmentLight);
        Helper.SpawnPrefab(newTime.GlobalVolume, environmentLight);

        RenderSettings.ambientSkyColor = newTime.SkyColor;
        RenderSettings.ambientEquatorColor = newTime.EquatorColor;
        RenderSettings.ambientGroundColor = newTime.GroundColor;

        m_activeTime = newTime;
    }

    public void ToggleFireflies(bool isOn)
    {
        m_firefliesActive = isOn;
        fireflies.SetActive(m_firefliesActive);

        ParticleSystem system = fireflies.GetComponent<ParticleSystem>();
        if (m_firefliesActive) system.Play();
        else system.Stop();
    }

    public void SetOption(OptionsManager.OptionType type, dynamic option, bool isOn)
    {
        if (type == OptionsManager.OptionType.Lights) SetLights((OptionsManager.LightEffect)option);
        else if (isOn)
        {
            if (type == OptionsManager.OptionType.Models) SetModel(((Option<GameObject>)option).Item);
            else if (type == OptionsManager.OptionType.Materials) SetMaterial(((Option<Material>)option).Item);
            else if (type == OptionsManager.OptionType.TimesOfDay) SetTimeOfDay((TimeOfDay)option);
        }
    }

    public void SetLights(OptionsManager.LightEffect effect)
    {
        if (effect == OptionsManager.LightEffect.StringLights) ToggleStringLights(!m_stringLightsActive);
        else if (effect == OptionsManager.LightEffect.Fireflies) ToggleFireflies(!m_firefliesActive);
    }

    public void ToggleStringLights(bool isOn)
    {
        m_stringLightsActive = isOn;
        stringLightsController.ToggleLights(isOn);
    }

    public Material GetOriginalModelMat(GameObject model)
    {
        return model.GetComponentInChildren<MeshRenderer>(true).sharedMaterial;
    }

    public void InitializeState()
    {
        m_activeMaterial = null;
        SetModel(optionsManager.Models[0].Item);
        SetTimeOfDay(optionsManager.TimesOfDay[0]);
        ToggleStringLights(false);
        ToggleFireflies(false);
    }
}

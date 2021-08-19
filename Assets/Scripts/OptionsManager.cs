using System;
using System.Linq;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public enum OptionType { Models, Materials, TimesOfDay, Lights };

    [ColorUsage(true, true)]
    public Color daySkyColor;
    [ColorUsage(true, true)]
    public Color dayEquatorColor;
    [ColorUsage(true, true)]
    public Color dayGroundColor;

    [ColorUsage(true, true)]
    public Color nightSkyColor;
    [ColorUsage(true, true)]
    public Color nightEquatorColor;
    [ColorUsage(true, true)]
    public Color nightGroundColor;

    private static Option<GameObject>[] m_models;
    public static Option<GameObject>[] Models { get { return m_models; } }

    private static Option<Material>[] m_materials;
    public static Option<Material>[] Materials { get { return m_materials; } }

    private static TimeOfDay[] m_timesOfDay;
    public static TimeOfDay[] TimesOfDay { get { return m_timesOfDay; } }

    private ToggleIconSet[] m_tabIcons;
    public ToggleIconSet[] TabIcons { get { return m_tabIcons; } }

        private ToggleIconSet[] m_transformTabIcons;
    public ToggleIconSet[] TransformTabIcons { get { return m_transformTabIcons; } }

    public enum LightEffect { StringLights, Fireflies };

    public ModelController modelController;
    public FirefliesController firefliesController;
    public StringLightsController stringLightsController;
    public EnvironmentController environmentController;

    public void LoadModels()
    {
        GameObject[] loadedModels = Resources.LoadAll<GameObject>("Models");
        m_models = loadedModels.Select(model => new Option<GameObject>(model.name, model)).ToArray();
    }

    public void LoadMaterials()
    {
        Material[] loadedMats = Resources.LoadAll<Material>("Materials");
        Option<Material>[] convertedMats = loadedMats.Select(material => new Option<Material>(material.name, material)).ToArray();
        
        m_materials = new Option<Material>[convertedMats.Length + 1];
        m_materials[0] = new Option<Material>("Original", null);
        convertedMats.CopyTo(m_materials, 1);
    }

    public void LoadTimesOfDay()
    {
        m_timesOfDay = new TimeOfDay[] {
            new TimeOfDay("Day", daySkyColor, dayEquatorColor, dayGroundColor),
            new TimeOfDay("Night", nightSkyColor, nightEquatorColor, nightGroundColor)
        };
    }

    public ToggleIconSet[] LoadTabIcons(dynamic types)
    {
        ToggleIconSet[] icons = new ToggleIconSet[types.Length];
        foreach (var type in types)
        {
            icons[(int)type] = new ToggleIconSet("TabIcons/" + (type).ToString());
        }
        return icons;
    }

    public dynamic GetOptionsOfType(OptionType type)
    {
        if (type == OptionType.Models) return Models;
        else if (type == OptionType.Materials) return Materials;
        else if (type == OptionType.TimesOfDay) return TimesOfDay;
        if (type == OptionType.Lights) return Helper.GetEnumValues<LightEffect>();
        else return null;
    }

    public string GetOptionName(OptionType type, dynamic option)
    {
        string name;
        if (type == OptionType.Models) name = ((Option<GameObject>)option).Name;
        else if (type == OptionType.Materials) name = ((Option<Material>)option).Name;
        else if (type == OptionType.TimesOfDay) name = ((TimeOfDay)option).Name;
        else if (type == OptionType.Lights) name = ((LightEffect)option).ToString();
        else return null;

        return Helper.AddSpacesToString(name);
    }

    public void SetOption(OptionType type, dynamic option, bool isOn)
    {
        if (type == OptionType.Lights) {
            option = (LightEffect)option;
            if (option == LightEffect.StringLights) stringLightsController.ToggleLights(isOn);
            else if (option == LightEffect.Fireflies) firefliesController.ToggleFireflies(isOn);
        } else if (isOn)
        {
            if (type == OptionType.Models) modelController.SetModel(((Option<GameObject>)option).Item);
            else if (type == OptionType.Materials) modelController.SetMaterial(((Option<Material>)option).Item);
            else if (type == OptionType.TimesOfDay) environmentController.SetTimeOfDay((TimeOfDay)option);
        }
    }

    void Awake()
    {
        LoadModels();
        LoadMaterials();
        LoadTimesOfDay();
        m_tabIcons = LoadTabIcons(Helper.GetEnumValues<OptionType>());
        m_transformTabIcons = LoadTabIcons(Helper.GetEnumValues<ModelController.TransformType>());

        stringLightsController.ToggleLights(false);
        firefliesController.ToggleFireflies(false);
        environmentController.SetTimeOfDay(TimesOfDay[0]);
        modelController.SetModel(Models[0].Item);
    }
}

using System;
using System.Linq;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public enum OptionType { Models, Materials, TimesOfDay };

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

    private Option<GameObject>[] m_models;
    public Option<GameObject>[] Models { get { return m_models; } }

    private Option<Material>[] m_materials;
    public Option<Material>[] Materials { get { return m_materials; } }

    private TimeOfDay[] m_timesOfDay;
    public TimeOfDay[] TimesOfDay { get { return m_timesOfDay; } }

    private ToggleIconSet[] m_tabIcons;
    public ToggleIconSet[] TabIcons { get { return m_tabIcons; } }

        private ToggleIconSet[] m_transformTabIcons;
    public ToggleIconSet[] TransformTabIcons { get { return m_transformTabIcons; } }

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
        else return null;
    }

    public string GetOptionName(OptionType type, dynamic option)
    {
        string name;
        if (type == OptionType.Models) name = ((Option<GameObject>)option).Name;
        else if (type == OptionType.Materials) name = ((Option<Material>)option).Name;
        else if (type == OptionType.TimesOfDay) name = ((TimeOfDay)option).Name;
        else return null;

        return Helper.AddSpacesToString(name);
    }

    void Awake()
    {
        LoadModels();
        LoadMaterials();
        LoadTimesOfDay();
        m_tabIcons = LoadTabIcons(Helper.GetEnumValues<OptionType>());
        m_transformTabIcons = LoadTabIcons(Helper.GetEnumValues<ModelController.TransformType>());
    }
}

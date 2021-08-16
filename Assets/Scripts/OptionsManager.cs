using System;
using System.Linq;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public enum OptionType { Models, Materials };

    private Option<GameObject>[] m_models;
    public Option<GameObject>[] Models { get { return m_models; } }

    private Option<Material>[] m_materials;
    public Option<Material>[] Materials { get { return m_materials; } }

    private ToggleIconSet[] m_tabIcons;
    public ToggleIconSet[] TabIcons { get { return m_tabIcons; } }

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
        else return null;
    }

    void Awake()
    {
        LoadModels();
        LoadMaterials();
        m_tabIcons = LoadTabIcons((OptionType[])System.Enum.GetValues(typeof(OptionType)));
    }
}

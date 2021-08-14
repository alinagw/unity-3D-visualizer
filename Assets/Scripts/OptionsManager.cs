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

    public void LoadModels()
    {
        GameObject[] loadedModels = Resources.LoadAll<GameObject>("Models");
        m_models = loadedModels.Select(model => new Option<GameObject>(model.name, model)).ToArray();
    }

    public void LoadMaterials()
    {
        Material[] loadedMats = Resources.LoadAll<Material>("Materials");
        Option<Material>[] convertedMats = loadedMats.Select(material => new Option<Material>(material.name, material)).ToArray();
    }

    void Awake()
    {
        LoadModels();
        LoadMaterials();
    }
}

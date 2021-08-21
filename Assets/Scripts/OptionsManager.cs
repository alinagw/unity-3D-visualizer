using System.Linq;
using UnityEngine;

// Handles loading all of the resources used as customization options, initializing the current options, and setting new options
public class OptionsManager : MonoBehaviour
{
    // List of the different types of customization options
    public enum OptionType { Models, Materials, TimesOfDay, Lights };
    // List of different light effects
    public enum LightEffect { StringLights, Fireflies };

    // Store the different scene controllers to trigger their state update functions
    public ModelController modelController;
    public EnvironmentController environmentController;
    public StringLightsController stringLightsController;
    public FirefliesController firefliesController;

    // HDR color inputs for storing the environment lighting gradient colors for each different time of day option
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

    // Array of Options for models
    private static Option<GameObject>[] m_models;
    public static Option<GameObject>[] Models { get { return m_models; } }

    // Array of Options for materials
    private static Option<Material>[] m_materials;
    public static Option<Material>[] Materials { get { return m_materials; } }

    // Array of TimeOfDays for the different times of day
    private static TimeOfDay[] m_timesOfDay;
    public static TimeOfDay[] TimesOfDay { get { return m_timesOfDay; } }

    // Array of ToggleIconSets of menu tab icons
    private ToggleIconSet[] m_tabIcons;
    public ToggleIconSet[] TabIcons { get { return m_tabIcons; } }

    // Array of ToggleIconSets of transform toggle icons
        private ToggleIconSet[] m_transformTabIcons;
    public ToggleIconSet[] TransformTabIcons { get { return m_transformTabIcons; } }

    // Load the prefabs for each model from the Resources/Models folder
    void LoadModels()
    {
        GameObject[] loadedModels = Resources.LoadAll<GameObject>("Models");
        // Create an Option instance for each model to store the model's name and object
        m_models = loadedModels.Select(model => new Option<GameObject>(model.name, model)).ToArray();
    }

    // Load the materials from the Resources/Materials folder
    void LoadMaterials()
    {
        Material[] loadedMats = Resources.LoadAll<Material>("Materials");
        // Create an Option instance for each material to store the material's name and object
        Option<Material>[] convertedMats = loadedMats.Select(material => new Option<Material>(material.name, material)).ToArray();
        
        // Create new array with the first item storing a null material named "Original" used for setting the active model's material back to its original material
        m_materials = new Option<Material>[convertedMats.Length + 1];
        m_materials[0] = new Option<Material>("Original", null);
        convertedMats.CopyTo(m_materials, 1);
    }

    // Create two TimeOfDay classes (Day and Night) using the HDR colors
    void LoadTimesOfDay()
    {
        m_timesOfDay = new TimeOfDay[] {
            new TimeOfDay("Day", daySkyColor, dayEquatorColor, dayGroundColor),
            new TimeOfDay("Night", nightSkyColor, nightEquatorColor, nightGroundColor)
        };
    }

    // Load all the toggle icons from the "Resources/TabIcons" folder
    ToggleIconSet[] LoadTabIcons(dynamic types)
    {
        ToggleIconSet[] icons = new ToggleIconSet[types.Length];
        foreach (var type in types)
        {
            icons[(int)type] = new ToggleIconSet("TabIcons/" + (type).ToString());
        }
        return icons;
    }

    // Return the appropriate array of options for the given option type
    public dynamic GetOptionsOfType(OptionType type)
    {
        if (type == OptionType.Models) return Models;
        else if (type == OptionType.Materials) return Materials;
        else if (type == OptionType.TimesOfDay) return TimesOfDay;
        if (type == OptionType.Lights) return Helper.GetEnumValues<LightEffect>();
        else return null;
    }

    // Return the correct name for the option for the given option type
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

    // Trigger appropriate controller function to update its state for the given option type
    public void SetOption(OptionType type, dynamic option, bool isOn)
    {
        // Light effects are not in a toggle group and can individually be toggled on/off
        if (type == OptionType.Lights) {
            option = (LightEffect)option;
            if (option == LightEffect.StringLights) stringLightsController.ToggleLights(isOn);
            else if (option == LightEffect.Fireflies) firefliesController.ToggleFireflies(isOn);
        } 
        // Models, materials, and time of day are in their own toggle groups so we update the state only when toggled on
        else if (isOn)
        {
            if (type == OptionType.Models) modelController.SetModel(((Option<GameObject>)option).Item);
            else if (type == OptionType.Materials) modelController.SetMaterial(((Option<Material>)option).Item);
            else if (type == OptionType.TimesOfDay) environmentController.SetTimeOfDay((TimeOfDay)option);
        }
    }

    void Awake()
    {
        // Load all the resources
        LoadModels();
        LoadMaterials();
        LoadTimesOfDay();
        m_tabIcons = LoadTabIcons(Helper.GetEnumValues<OptionType>());
        m_transformTabIcons = LoadTabIcons(Helper.GetEnumValues<ModelController.TransformType>());

        // Set the initial states of the lights
        stringLightsController.ToggleLights(false);
        firefliesController.ToggleFireflies(false);  
    }
}

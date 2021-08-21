using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Scene managers/controllers used to get lists of options and update the state
    public OptionsManager optionsManager;
    public ModelController modelController;

    // Menu UI "containers" where different UI elements will be spawned/updated
    // Tabs for switching between option types
    public ToggleGroup tabList;
    // Tabs for switching between model transform modes
    public ToggleGroup transformTabList;
    public GameObject tabContentArea;
    public GameObject tabTitle;

    // Prefabs to spawn
    public GameObject tabButtonPrefab;
    public GameObject tabPanePrefab;
    public GameObject optionTogglePrefab;

    // Menu animator component to control the menu slide in/out animation
    private Animator menuAnimator;
    
    // Menu is open state
    private bool m_menuIsOpen = false;

    // Show/Hide the menu
    public void ToggleMenu(bool isOpen) {
        // Set the bool in the animator controller that triggers the slide animation
        menuAnimator.SetBool("isOpen", isOpen);
        m_menuIsOpen = isOpen;
    }

    // Create a menu tab used for switching between lists of options for each option type
    public void CreateTab(OptionsManager.OptionType type)
    {
        // Spawn a new tab button
        GameObject tab = Helper.SpawnPrefab(tabButtonPrefab, tabList.gameObject);
        Toggle tabToggle = tab.GetComponent<Toggle>();
        // Set the tab toggle group to the tab list toggle group (only one tab can be active at a time)
        tabToggle.group = tabList;
        // Set the active/inactive sprites for the toggle button
        SetTabImages(optionsManager.TabIcons[(int)type], tabToggle);

        // Spawn a corresponding tab content pane for this tab button
        GameObject tabPane = Helper.SpawnPrefab(tabPanePrefab, tabContentArea);

        // Add a listener for when the toggle's value changes to switch tabs
        tabToggle.onValueChanged.AddListener(delegate
        {
            // Close the menu if all toggles were toggled off (clicking the currently active tab will toggle all toggles off)
            if (!tabList.AnyTogglesOn() && m_menuIsOpen) ToggleMenu(false);
            // Open the menu if a tab is toggled on for the first time
            if (tabToggle.isOn && !m_menuIsOpen) ToggleMenu(true);
            // Update the title text for the tab to match the option type name
            if (tabToggle.isOn) tabTitle.GetComponent<Text>().text = type.ToString();
            // Set the corresponding tab pane GameObject to the toggle's state
            tabPane.SetActive(tabToggle.isOn);
        });

        // Create the appropriate list of option toggles in the tab pane for this option type 
        CreateOptionToggles(type, tabPane);
    }

    // Create a menu tab used for switching between the model controller's transform modes
    public void CreateTransformTab(ModelController.TransformType type)
    {
        // Spawn a new tab button
        GameObject tab = Helper.SpawnPrefab(tabButtonPrefab, transformTabList.gameObject);
        Toggle tabToggle = tab.GetComponent<Toggle>();
        // Set the active/inactive sprites for the toggle button
        SetTabImages(optionsManager.TransformTabIcons[(int)type], tabToggle);

        // The reset transform button doesn't behave like a tab
        if (type == ModelController.TransformType.Reset)
        {
            // Add a listener for when the reset button is clicked
            tabToggle.onValueChanged.AddListener(delegate
            {
                // Reset the model's transforms
                modelController.ResetAllTransforms();
                // Change the value of the reset toggle to false without triggering the event listener
                tabToggle.SetIsOnWithoutNotify(false);
            });
        }
        // For translate, rotate, and scale transform modes
        else
        {
            // Set the toggle group (can only perform one type of transform at a time)
            tabToggle.group = transformTabList;
            // Add a listener to update the type of transform when a transform toggle is toggled on
            tabToggle.onValueChanged.AddListener(delegate
        {
            if (tabToggle.isOn) modelController.UpdateTransformType(type);
        });
        }
    }

    // Set the active and inactive sprites for a tab
    public void SetTabImages(ToggleIconSet tabIcons, Toggle tabToggle)
    {
        foreach (Transform child in tabToggle.transform)
        {
            Image img = child.gameObject.GetComponent<Image>();
            // Set the GameObjects' icons based on their name (active vs inactive)
            if (child.name == "Inactive Icon") img.sprite = tabIcons.InactiveIcon;
            else if (child.name == "Active Icon") img.sprite = tabIcons.ActiveIcon;
        }
    }

    // Create the list of option toggles for the given option type
    public void CreateOptionToggles(OptionsManager.OptionType type, GameObject toggleList)
    {
        // Get the array of options for the given type
        var options = optionsManager.GetOptionsOfType(type);
        // Create a toggle for each option
        foreach (var option in options)
        {
            // Spawn a new toggle
            GameObject optionToggle = Helper.SpawnPrefab(optionTogglePrefab, toggleList);
            Toggle toggleComponent = optionToggle.GetComponent<Toggle>();
            
            // Add all options to their tab pane's toggle group except lights
            // Lights can be toggled on/off independently and shouldn't be added to a toggle group
            if (type != OptionsManager.OptionType.Lights)
            {
                toggleComponent.group = toggleList.GetComponent<ToggleGroup>();
            }

            // Set the toggle's label text
            optionToggle.GetComponentInChildren<Text>().text = optionsManager.GetOptionName(type, option);
            
            // Add a listener to update the state with this option for the given option type
            toggleComponent.onValueChanged.AddListener(delegate
                {
                    optionsManager.SetOption(type, option, toggleComponent.isOn);
                });
        }
        // Set the first toggle active for each option type except lights
        if (type != OptionsManager.OptionType.Lights)
            {
                toggleList.transform.GetChild(0).GetComponent<Toggle>().isOn = true;
            }
    }

    void Start()
    {
        menuAnimator = GetComponent<Animator>();

        // Create menu option tabs for each option type
        foreach (OptionsManager.OptionType type in Helper.GetEnumValues<OptionsManager.OptionType>())
        {
            CreateTab(type);
        }

        // Create menu transform tabs for each transform mode
        foreach (ModelController.TransformType type in Helper.GetEnumValues<ModelController.TransformType>())
        {
            CreateTransformTab(type);
        }

        // Toggle all the menu toggles off so the menu starts closed
        tabList.SetAllTogglesOff();
    }
}

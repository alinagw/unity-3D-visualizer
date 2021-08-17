using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public OptionsManager optionsManager;
    public StateManager stateManager;
    public ModelController modelController;

    public GameObject tabList;
    public GameObject tabButtonPrefab;

        public GameObject transformTabList;
    
    
    public GameObject tabContentArea;
    public GameObject tabPanePrefab;

    public GameObject tabTitle;
    public GameObject optionTogglePrefab;

    private Animator menuAnimator;
    private ToggleGroup tabGroup;
    private ToggleGroup transformTabGroup;
    
    private bool m_menuIsOpen = false;

    public void ToggleMenu(bool isOpen) {
        menuAnimator.SetBool("isOpen", isOpen);
        m_menuIsOpen = isOpen;
    }

    public void CreateTab(OptionsManager.OptionType type)
    {
        GameObject tab = Helper.SpawnPrefab(tabButtonPrefab, tabList);
        Toggle tabToggle = tab.GetComponent<Toggle>();
        tabToggle.group = tabGroup;

        SetTabImages(optionsManager.TabIcons[(int)type], tab);

        GameObject tabPane = Helper.SpawnPrefab(tabPanePrefab, tabContentArea);

        tabToggle.onValueChanged.AddListener(delegate
        {
            if (!tabGroup.AnyTogglesOn() && m_menuIsOpen) ToggleMenu(false);
            if (tabToggle.isOn && !m_menuIsOpen) ToggleMenu(true);
            if (tabToggle.isOn) tabTitle.GetComponent<Text>().text = type.ToString();
            tabPane.SetActive(tabToggle.isOn);
        });

        CreateOptionToggles(type, tabPane);
    }

    public void CreateTransformTab(ModelController.TransformType type)
    {
        GameObject tab = Helper.SpawnPrefab(tabButtonPrefab, transformTabList);
        Toggle tabToggle = tab.GetComponent<Toggle>();
        SetTabImages(optionsManager.TransformTabIcons[(int)type], tab);

        if (type == ModelController.TransformType.Reset)
        {
            tabToggle.onValueChanged.AddListener(delegate
        {
            modelController.ResetAllTransforms();
            tabToggle.SetIsOnWithoutNotify(false);
        });
        }
        else
        {
            tabToggle.group = transformTabGroup;
            tabToggle.onValueChanged.AddListener(delegate
        {
            if (tabToggle.isOn) modelController.UpdateTransformType(type);
        });
        }
    }

    public void SetTabImages(ToggleIconSet tabIcons, GameObject tab)
    {
        Toggle tabToggle = tab.GetComponent<Toggle>();
        foreach (Transform child in tab.transform)
        {
            Image img = child.gameObject.GetComponent<Image>();
            if (child.name == "Inactive Icon") img.sprite = tabIcons.InactiveIcon;
            else if (child.name == "Active Icon") img.sprite = tabIcons.ActiveIcon;
        }
    }

    public void CreateOptionToggles(OptionsManager.OptionType type, GameObject toggleList)
    {
        var options = optionsManager.GetOptionsOfType(type);
        foreach (var option in options)
        {
            GameObject optionToggle = Helper.SpawnPrefab(optionTogglePrefab, toggleList);
            Toggle toggleComponent = optionToggle.GetComponent<Toggle>();
            toggleComponent.group = toggleList.GetComponent<ToggleGroup>();
            
            optionToggle.GetComponentInChildren<Text>().text = option.Name;
            toggleComponent.onValueChanged.AddListener(delegate
                {
                    stateManager.SetOption(type, option, toggleComponent.isOn);
                });
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stateManager.InitializeState();
        menuAnimator = GetComponent<Animator>();
        tabGroup = tabList.GetComponent<ToggleGroup>();
        transformTabGroup = transformTabList.GetComponent<ToggleGroup>();

        foreach (OptionsManager.OptionType type in Helper.GetEnumValues<OptionsManager.OptionType>())
        {
            CreateTab(type);
        }

        foreach (ModelController.TransformType type in Helper.GetEnumValues<ModelController.TransformType>())
        {
            CreateTransformTab(type);
        }

        tabGroup.SetAllTogglesOff();
    }
}

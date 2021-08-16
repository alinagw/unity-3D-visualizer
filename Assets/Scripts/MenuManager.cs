using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public OptionsManager optionsManager;
    public StateManager stateManager;

    public GameObject tabList;
    public GameObject tabButtonPrefab;
    
    public GameObject tabContentArea;
    public GameObject tabPanePrefab;

    public GameObject tabTitle;
    public GameObject optionTogglePrefab;

    private Animator menuAnimator;
    private ToggleGroup tabGroup;
    
    private bool m_menuIsOpen = false;

    public void ToggleMenu(bool isOpen) {
        menuAnimator.SetBool("isOpen", isOpen);
        m_menuIsOpen = isOpen;
    }

    public void CreateTab(OptionsManager.OptionType type)
    {
        GameObject tab = SpawnPrefab(tabButtonPrefab, tabList);
        Toggle tabToggle = tab.GetComponent<Toggle>();
        tabToggle.group = tabGroup;

        SetTabImages(optionsManager.TabIcons[(int)type], tab);

        GameObject tabPane = SpawnPrefab(tabPanePrefab, tabContentArea);

        tabToggle.onValueChanged.AddListener(delegate
        {
            if (!tabGroup.AnyTogglesOn() && m_menuIsOpen) ToggleMenu(false);
            if (tabToggle.isOn && !m_menuIsOpen) ToggleMenu(true);
            if (tabToggle.isOn) tabTitle.GetComponent<Text>().text = type.ToString();
            tabPane.SetActive(tabToggle.isOn);
        });

        CreateOptionToggles(type, tabPane);
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
            GameObject optionToggle = SpawnPrefab(optionTogglePrefab, toggleList);
            Toggle toggleComponent = optionToggle.GetComponent<Toggle>();
            toggleComponent.group = toggleList.GetComponent<ToggleGroup>();
            
            optionToggle.GetComponentInChildren<Text>().text = option.Name;
            toggleComponent.onValueChanged.AddListener(delegate
                {
                    stateManager.SetOption(type, option, toggleComponent.isOn);
                });
        }
    }

    public GameObject SpawnPrefab(GameObject prefab, GameObject parent)
    {
        GameObject newObject = GameObject.Instantiate(prefab);
        newObject.transform.SetParent(parent.transform);
        newObject.transform.localScale = Vector3.one;
        newObject.transform.localRotation = Quaternion.identity;
        return newObject;
    }

    public void DestroyChildren(GameObject parent)
    {
        if (parent.transform.childCount > 0)
        {
            foreach (Transform child in parent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stateManager.InitializeState();
        menuAnimator = GetComponent<Animator>();
        tabGroup = tabList.GetComponent<ToggleGroup>();

        foreach (OptionsManager.OptionType type in (OptionsManager.OptionType[])System.Enum.GetValues(typeof(OptionsManager.OptionType)))
        {
            CreateTab(type);
        }

        tabGroup.SetAllTogglesOff();
    }
}

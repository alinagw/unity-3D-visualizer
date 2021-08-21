using UnityEngine;

// Stores the active and inactive sprite set for a tab button
public class ToggleIconSet
{
    private Sprite m_activeIcon;
    public Sprite ActiveIcon { get { return m_activeIcon; } }

    private Sprite m_inactiveIcon;
    public Sprite InactiveIcon { get { return m_inactiveIcon; } }

    // Load tab icons from "Resources/*" folder
    public ToggleIconSet(string path)
    {
        m_activeIcon = Resources.Load<Sprite>(path + "/ActiveIcon");
        m_inactiveIcon = Resources.Load<Sprite>(path + "/InactiveIcon");
    }
}
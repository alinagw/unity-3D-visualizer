using UnityEngine;

public class TimeOfDay
{
    private string m_name;
    public string Name { get { return m_name; } }

    private Material m_skybox;
    public Material Skybox { get { return m_skybox; } }

    private GameObject m_light;
    public GameObject Light { get { return m_light; } }

    private GameObject m_globalVolume;
    public GameObject GlobalVolume { get { return m_globalVolume; } }

    private Color m_skyColor;
    public Color SkyColor { get { return m_skyColor; } }

    private Color m_equatorColor;
    public Color EquatorColor { get { return m_equatorColor; } }

    private Color m_groundColor;
    public Color GroundColor { get { return m_groundColor; } }

    public TimeOfDay(string name, Color skyColor, Color equatorColor, Color groundColor)
    {
        string path = "TimesOfDay/" + name + "/" + name + " ";
        m_name = name;
        m_skybox = Resources.Load<Material>(path + "Skybox");
        m_light = Resources.Load<GameObject>(path + "Light");
        m_globalVolume = Resources.Load<GameObject>(path + "Global Volume");
        m_skyColor = skyColor;
        m_equatorColor = equatorColor;
        m_groundColor = groundColor;
    }
}

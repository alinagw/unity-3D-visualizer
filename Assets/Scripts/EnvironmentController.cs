using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public ReflectionProbe refProbe;
    private TimeOfDay m_activeTime;
    public TimeOfDay ActiveTime { get { return m_activeTime; } }

    public void SetTimeOfDay(TimeOfDay newTime)
    {
        RenderSettings.skybox = newTime.Skybox;

        Helper.DestroyChildren(this.gameObject);
        Helper.SpawnPrefab(newTime.Light, this.gameObject);
        Helper.SpawnPrefab(newTime.GlobalVolume, this.gameObject);

        RenderSettings.ambientSkyColor = newTime.SkyColor;
        RenderSettings.ambientEquatorColor = newTime.EquatorColor;
        RenderSettings.ambientGroundColor = newTime.GroundColor;

        refProbe.RenderProbe();

        m_activeTime = newTime;
    }
}

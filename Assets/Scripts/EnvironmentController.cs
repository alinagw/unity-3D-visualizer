using UnityEngine;

// Handles updating the time of day
public class EnvironmentController : MonoBehaviour
{ 
    public ReflectionProbe refProbe;
    private TimeOfDay m_activeTime;

    // Update the time of day
    public void SetTimeOfDay(TimeOfDay newTime)
    {
        // Set new skybox
        RenderSettings.skybox = newTime.Skybox;

        // Instantiate new directional light and global volume GameObjects
        Helper.DestroyChildren(this.gameObject);
        Helper.SpawnPrefab(newTime.Light, this.gameObject);
        Helper.SpawnPrefab(newTime.GlobalVolume, this.gameObject);

        // Set the environment lighting gradient colors
        RenderSettings.ambientSkyColor = newTime.SkyColor;
        RenderSettings.ambientEquatorColor = newTime.EquatorColor;
        RenderSettings.ambientGroundColor = newTime.GroundColor;

        // Re-render the reflection probe to update the environment reflections
        refProbe.RenderProbe();
        
        m_activeTime = newTime;
    }
}

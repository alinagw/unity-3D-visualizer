using UnityEngine;

// Handles playing/stopping fireflies particle effect
public class FirefliesController : MonoBehaviour
{
    private ParticleSystem particles;
    
    private bool m_firefliesActive;

    // Toggle fireflies effect on/off
    public void ToggleFireflies(bool isOn)
    {
        // Enable/Disable GameObject
        gameObject.SetActive(isOn);
        // Control particle system
        if (isOn) particles.Play();
        else particles.Stop();
        
        m_firefliesActive = isOn;
    }

    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        ToggleFireflies(false);
    }
}

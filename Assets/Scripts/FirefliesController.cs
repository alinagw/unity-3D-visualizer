using UnityEngine;

public class FirefliesController : MonoBehaviour
{
    private ParticleSystem particles;
    
    private bool m_firefliesActive;
    public bool isOn { get { return m_firefliesActive; } }

    public void ToggleFireflies(bool isOn)
    {
        gameObject.SetActive(isOn);
        if (isOn) particles.Play();
        else particles.Stop();

        m_firefliesActive = isOn;
    }

    // Start is called before the first frame update
    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }
}

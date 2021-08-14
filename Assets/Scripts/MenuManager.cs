using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private Animator menuAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        menuAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMenu(bool isOpen) {
        menuAnimator.SetBool("isOpen", isOpen);
    }
}

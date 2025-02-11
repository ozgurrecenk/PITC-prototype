using System;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;
    public bool isActive = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isActive)
            {
                Off();
            }
            else
            {
                On();
            }
        }
    }

    void On()
    {
        isActive = true;
        flashlight.enabled = true;   
    }

    void Off()
    {
        isActive = false;
        flashlight.enabled = false;
    }
}

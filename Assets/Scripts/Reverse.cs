using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            instance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/MotorOn");
            instance.start();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();
        }
    }
}
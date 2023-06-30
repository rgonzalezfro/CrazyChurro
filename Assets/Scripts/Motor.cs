using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            instance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/MotorOn");
            instance.start();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();
        }
    }
}
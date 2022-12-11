using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private float fov = 90f;
    private float mouseSensitivity = 400f;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SetMouseSensitivity(float value)
    {
        mouseSensitivity = value;
    }

    public float GetMouseSensitivity()
    {
        return mouseSensitivity;
    }

    public void SetFOV(float value)
    {
        fov = value;
    }

    public float GetFOV()
    {
        return fov;
    }

}

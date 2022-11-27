using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Time Slow Down Settings")]
    public GameObject slowDownUIObj;

    private void Start()
    {
        Debug.Log(Environment.UserName);
        Physics.gravity = new Vector3(0, -4.5F, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Time.timeScale = 0.2f;
            slowDownUIObj.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Time.timeScale = 1f;
            slowDownUIObj.SetActive(false);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    enum GameStates
    {
        Wave,
        Prepare,
        GameOver

    }

    [Header("UI GameObjects")]
    [SerializeField] Slider healthSliderUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] TextMeshProUGUI timerUI;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] Animator waveTextAnimator;

    [Header("Time Slow Down Settings")]
    public GameObject slowDownUIObj;

    [Header("GameObject References")]
    [SerializeField] GameObject playerObj;
    [SerializeField] PlayerCam playerCam;
    [SerializeField] EnemySpawner spawner;
    [SerializeField] FlockingManger flockSpawner;

    private GameStates currentState;
    public float waitTime;
    public float timer;

    private int waveNum;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log(Environment.UserName);
        Physics.gravity = new Vector3(0, -4.5F, 0);

        PrepareForNextWave();
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameStates.Wave:
                break;
            case GameStates.Prepare:
                UpdatePrepare();
                break;
            case GameStates.GameOver:
                break;
        }

        SwitchState();

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

    private void SwitchState()
    {

    }

    public void UpdateHealth(float health)
    {
        healthSliderUI.value = health;
    }

    private void StartWave()
    {
        currentState = GameStates.Wave;
        waveNum++;
        waveText.text = "Wave " + waveNum;
        waveTextAnimator.SetTrigger("Fade");
        timerUI.enabled = false;

        flockSpawner.SpawnEnemies(5);
        spawner.SpawnEnemies(5);
    }

    private void PrepareForNextWave()
    {
        currentState = GameStates.Prepare;
        waveText.text = "Prepare for Incoming Wave";
        waveTextAnimator.SetTrigger("Fade");
        timer = waitTime;
    }

    private void UpdatePrepare()
    {
        timer -= Time.deltaTime;
        string formatText = Mathf.Floor(timer / 60).ToString("00") + ":" + Mathf.FloorToInt(timer % 60).ToString("00");
        timerUI.text = formatText;

        if(timer <= 0)
        {
            currentState = GameStates.Wave;
            StartWave();
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerCam.enabled = false;
        //playerObj.GetComponent<>();
    }
}

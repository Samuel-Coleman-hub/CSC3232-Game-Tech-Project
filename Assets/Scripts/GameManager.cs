using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        Wave,
        Prepare,
        GameOver,
        Waiting

    }

    [Header("Game Difficulty Settings")]
    [SerializeField] int flockingEnemyNumStart;
    [SerializeField] int goapEnemyNumStart;
    [SerializeField] float flockingEnemyMultiplier;
    [SerializeField] float goapEnemyMultiplier;
    [SerializeField] float waitTime;

    private int flockingEnemyNum;
    private int goapEnemyNum;

    [Header("UI GameObjects")]
    [SerializeField] Slider healthSliderUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] TextMeshProUGUI gameOverWaveCountTextUI;
    [SerializeField] TextMeshProUGUI timerUI;
    [SerializeField] TextMeshProUGUI timerTitleUI;
    [SerializeField] TextMeshProUGUI waveTextUI;
    [SerializeField] Animator waveTextAnimator;

    [Header("Time Slow Down Settings")]
    public GameObject slowDownUIObj;

    [Header("GameObject References")]
    [SerializeField] GameObject playerObj;
    [SerializeField] PlayerCam playerCam;
    [SerializeField] EnemySpawner spawner;
    [SerializeField] FlockingManger flockSpawner;
    [SerializeField] Inventory inventory;
    [SerializeField] PlayerSelector playerSelector;
    [SerializeField] PlayerBase playerBase;

    public GameStates currentState;
    public float timer;
    public bool timerPaused = false;
    private int waveNum;

    private void Awake()
    {
        Settings settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>();
        Camera.main.fieldOfView = settings.GetFOV();
        playerCam.SetSensitvity(settings.GetMouseSensitivity());
        Physics.gravity = new Vector3(0, -4.5F, 0);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log(Environment.UserName);
        

        PrepareForNextWave();
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameStates.Wave:
                if (flockSpawner.IsAlive() && spawner.enemiesAlive == 0)
                {
                    EndWave();
                }
                break;
            case GameStates.Prepare:
                UpdatePrepare();
                break;
            case GameStates.GameOver:
                break;
            case GameStates.Waiting:
                break;
        }

        SlowMoHandler();
    }

    private void SlowMoHandler()
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

    public void UpdateHealth(float health)
    {
        healthSliderUI.value = health;
    }

    private void StartWave()
    {
        currentState = GameStates.Wave;
        playerSelector.enabled = false;
        waveNum++;
        FadeText("Wave " + waveNum);

        timerUI.enabled = false;
        timerTitleUI.enabled = false;

        if(waveNum == 1)
        {
            flockSpawner.SpawnEnemies(flockingEnemyNumStart);
            flockingEnemyNum = flockingEnemyNumStart;
        }
        else if(waveNum == 2)
        {
            spawner.SpawnEnemies(goapEnemyNumStart);
            goapEnemyNum = goapEnemyNumStart;
        }
        else
        {
            flockingEnemyNum = Random.Range(Mathf.FloorToInt(flockingEnemyNum * flockingEnemyMultiplier), Mathf.CeilToInt(flockingEnemyNum * flockingEnemyMultiplier));
            goapEnemyNum = Random.Range(Mathf.FloorToInt(goapEnemyNum * goapEnemyMultiplier), Mathf.CeilToInt(goapEnemyNum * goapEnemyMultiplier));
            Debug.Log("FLOCK ENEMIES " +flockingEnemyNum);
            Debug.Log("GOAP ENEMIES" + goapEnemyNum);
            flockSpawner.SpawnEnemies(flockingEnemyNum);
            spawner.SpawnEnemies(goapEnemyNum);
        }
    }

    private void EndWave()
    {
        currentState = GameStates.Waiting;
        playerSelector.enabled = true;
        FadeText("Wave Completed");
        StartCoroutine(WaitToChangeState());
        
    }

    IEnumerator WaitToChangeState()
    {
        yield return new WaitForSeconds(8f);
        currentState = GameStates.Prepare;
        PrepareForNextWave();
    }

    private void PrepareForNextWave()
    {
        timerUI.enabled = true;
        timerTitleUI.enabled = true;
        currentState = GameStates.Prepare;
        FadeText("Prepare for Incoming Wave");
        timer = waitTime;
    }

    private void UpdatePrepare()
    {
        if (!timerPaused)
        {
            timer -= Time.deltaTime;
            string formatText = Mathf.Floor(timer / 60).ToString("00") + ":" + Mathf.FloorToInt(timer % 60).ToString("00");
            timerUI.text = formatText;

            if (timer <= 0)
            {
                currentState = GameStates.Wave;
                StartWave();
            }
        }
        else
        {
            timerUI.text = "Paused";
        }
       
    }

    public void GameOver()
    {
        currentState = GameStates.GameOver;

        gameOverUI.SetActive(true);
        gameOverWaveCountTextUI.text = "You Survived for " + (waveNum - 1) + " Waves";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerCam.enabled = false;
    }

    private void FadeText(string text)
    {
        waveTextUI.text = text;
        waveTextAnimator.SetTrigger("Fade");
    }

    public void AddDeathMoney(int minMoney, int maxMoney)
    {
        int money = Random.Range(minMoney, maxMoney);
        inventory.UpdateMoney(money, true);
    }

    public void SubtractMoney(int money)
    {
        inventory.UpdateMoney(money, false);
    }

    public int GetTotalMoney()
    {
        return inventory.GetMoney();
    }

    public void ResetHealth()
    {
        playerBase.health = playerBase.maxhealth;
        healthSliderUI.value = playerBase.health;
    }

    public bool CheckIfHealthMax()
    {
        return playerBase.health == playerBase.maxhealth;
    }
}

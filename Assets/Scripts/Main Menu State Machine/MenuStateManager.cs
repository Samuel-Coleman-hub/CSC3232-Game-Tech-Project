using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuStateManager : MonoBehaviour
{
    public MenuBaseState currentState;
    public HomeState homeState = new HomeState();
    public SettingsState settingsState = new SettingsState();
    public ControlsState controlsState = new ControlsState();
    public InstructionsState instructionsState = new InstructionsState();

    [SerializeField] public Settings settings;
    [SerializeField] public Transform cameraPosition;
    [SerializeField] public Button startGameButton;
    [SerializeField] public Button howToPlayButton;
    [SerializeField] public Button controlsButton;
    [SerializeField] public Button settingsButton;
    [SerializeField] public ManageScenes sceneManager;

    [SerializeField] public Slider fovSlider;
    [SerializeField] public Slider mouseSensitivitySlider;

    [SerializeField] public TextMeshProUGUI fovText;
    [SerializeField] public TextMeshProUGUI mouseSensitivityText;

    [Serializable] public struct CameraLocation
    {
        public string name;
        public Transform position;
    }
    [SerializeField] private CameraLocation[] cameraLocations;
    public Dictionary<string, Transform> cameraPositionsForUI = new Dictionary<string, Transform>();

    private void Start()
    {
        currentState = homeState;
        currentState.EnterState(this);

        homeState.StateName = "Home";
        settingsState.StateName = "Settings";
        controlsState.StateName = "Controls";
        instructionsState.StateName = "Instructions";

        foreach(CameraLocation location in cameraLocations)
        {
            cameraPositionsForUI.Add(location.name, location.position);
        }
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(MenuBaseState state)
    {
        currentState.ExitState(this);
        StartCoroutine(LerpPosition(cameraPositionsForUI[state.StateName].position, 3f));
        currentState = state;
        currentState.EnterState(this);

    }

    public void ReturnToHomeState()
    {
        SwitchState(homeState);
    }

    private IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = Camera.main.transform.position;
        while (time < duration)
        {
            Camera.main.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = targetPosition;
    }
}

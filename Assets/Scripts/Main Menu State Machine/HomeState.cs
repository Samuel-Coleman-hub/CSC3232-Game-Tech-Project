using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeState : MenuBaseState
{
    
    public override void EnterState(MenuStateManager menu)
    {
        menu.startGameButton.onClick.AddListener(delegate { StartGame(menu); });
        menu.howToPlayButton.onClick.AddListener(delegate { ChangeToHowToPlay(menu); });
        menu.controlsButton.onClick.AddListener(delegate { ChangeToControls(menu); });
        menu.settingsButton.onClick.AddListener(delegate { ChangeToSettings(menu); });

        

    }

    public override void ExitState(MenuStateManager menu)
    {
        menu.startGameButton.onClick.RemoveListener(delegate { StartGame(menu); });
        menu.howToPlayButton.onClick.RemoveListener(delegate { ChangeToHowToPlay(menu); });
        menu.controlsButton.onClick.RemoveListener(delegate { ChangeToControls(menu); });
        menu.settingsButton.onClick.RemoveListener(delegate { ChangeToSettings(menu); });
    }

    public override void UpdateState(MenuStateManager menu){}

    private void StartGame(MenuStateManager menu)
    {
        menu.sceneManager.StartGame();
    }

    private void ChangeToHowToPlay(MenuStateManager menu)
    {
        menu.SwitchState(menu.instructionsState);
    }

    private void ChangeToControls(MenuStateManager menu)
    {
        menu.SwitchState(menu.controlsState);
    }

    private void ChangeToSettings(MenuStateManager menu)
    {
        menu.SwitchState(menu.settingsState);
    }
}

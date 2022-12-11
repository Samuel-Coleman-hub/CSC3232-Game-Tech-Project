using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsState : MenuBaseState
{
    public override void EnterState(MenuStateManager menu)
    {
        menu.fovSlider.onValueChanged.AddListener(delegate { FovChanged(menu); });
        menu.mouseSensitivitySlider.onValueChanged.AddListener(delegate { SensitivityChanged(menu); });
    }

    public override void ExitState(MenuStateManager menu)
    {
        
    }

    public override void UpdateState(MenuStateManager menu){}

    private void FovChanged(MenuStateManager menu)
    {
        menu.settings.SetFOV(menu.fovSlider.value);
        menu.fovText.text = menu.fovSlider.value.ToString();
    }

    private void SensitivityChanged(MenuStateManager menu) 
    {
        menu.settings.SetMouseSensitivity(menu.mouseSensitivitySlider.value);
        menu.mouseSensitivityText.text = menu.mouseSensitivitySlider.value.ToString();
    }
}

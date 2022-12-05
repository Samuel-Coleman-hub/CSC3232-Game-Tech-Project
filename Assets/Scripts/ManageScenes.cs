using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManageScenes : MonoBehaviour
{
    
    // Start is called before the first frame update
    
    public void StartQuickGame()
    {
        SceneManager.LoadScene(sceneName: "GameScene");
    }

    public void StartEndlessGame()
    {

    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(sceneName: "StartScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    #region Unity_functions
    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Scene_transitions
    public void StartGame()
    {
        /* TODO: Change the scene when this function is called to the appropriate scene using SceneManager.LoadScene() */
    }

    public void LoseGame()
    {
        /* TODO: Change the scene when this function is called to the appropriate scene using SceneManager.LoadScene() */
    }

    public void WinGame()
    {
        /* TODO: Change the scene when this function is called to the appropriate scene using SceneManager.LoadScene() */
    }

    public void MainMenu()
    {
        /* TODO: Change the scene when this function is called to the appropriate scene using SceneManager.LoadScene() */
    }
    #endregion
}

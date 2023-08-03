using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    
    public static ScenesManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void GameStart()
   {
        SceneManager.LoadScene("Game");
   }

    public void GameEnd()
    {
        SceneManager.LoadScene("GameEnd");
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("Title");
    }
}

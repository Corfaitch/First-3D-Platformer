using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public int score;

    public bool paused;

    //Instantiating.
    public static GameManager2 instance;    
    

    void Awake () {
        if (instance != null && instance != this) 
            Destroy(gameObject);
        
        else 
            instance = this;
            DontDestroyOnLoad(gameObject);
    }

    void Update () {
        if(Input.GetButtonDown("Cancel")) 
            TogglePauseGame();
    }

    public void TogglePauseGame () {
        paused = !paused;

        if(paused)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;

        GameUI2.instance.TogglePauseScreen(paused);
    }

    public void AddScore (int scoreToGive) {
        score += scoreToGive;
        GameUI2.instance.UpdateScoreText();
    }

    public void LevelEnd (){
        //Last level?
        if(SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1) {
            WinGame();
        }
        else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void WinGame() {
        GameUI2.instance.SetEndScreen(true);
        Time.timeScale = 0.0f;
    }

    public void GameOver() {
        GameUI2.instance.SetEndScreen(false);
        Time.timeScale = 0.0f;
    }
}

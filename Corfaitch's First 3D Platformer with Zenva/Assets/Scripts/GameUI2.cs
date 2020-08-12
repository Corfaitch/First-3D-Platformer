    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI2 : MonoBehaviour {
    public TextMeshProUGUI scoreText;

    public GameObject endScreen;
    public TextMeshProUGUI endScreenHeader;
    public TextMeshProUGUI endScreenScoreText;
    public GameObject pauseScreen;  

    //instance
    public static GameUI2 instance;

    void Awake() {
        instance = this;
    }

    void Start() {
        UpdateScoreText();
    }

    public void UpdateScoreText() {
        scoreText.text = "Score: " + GameManager2.instance.score;
    }

    public void SetEndScreen (bool hasWon) {
        endScreen.SetActive(true);

        endScreenScoreText.text = "<b>Score</b>\n" + GameManager2.instance.score;

        if(hasWon) {
            endScreenHeader.text = "You Win";
            endScreenHeader.color = Color.green;
        }
        else {
            endScreenHeader.text = "Game Over";
            endScreenHeader.color = Color.red;
        }
    }

    public void OnRestartButton(){
        SceneManager.LoadScene(1);
    }

    public void OnMenuButton(){
        SceneManager.LoadScene(0);
    }

    public void TogglePauseScreen (bool paused) {
        pauseScreen.SetActive(paused);
    }
    public void OnResumeButton(){
        GameManager2.instance.TogglePauseGame();
    }

    
}

//Author : Qasim Ziauddin

//Controller that handles the "Game-Over" scenario. Provides final score and options to restart/quit the game.
using Assets.Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public GameObject _PlayAgainObject;
    private Button _PlayAgainButton;

    public GameObject _QuitObject;
    private Button _QuitButton;

    private GameObject _gameControllerObject;
    private GameController _gameController;

    public Text _GameSummaryText;
    public Text _GameSummaryScoreText;

    private int waveReached = 0;
    private int score = 0;


    private void Start()
    {
        _PlayAgainObject = GameObject.FindWithTag("PlayAgainButton");
        _QuitObject = GameObject.FindWithTag("QuitGameButton");
        _gameControllerObject = GameObject.FindWithTag("GameController");

        _PlayAgainButton = _PlayAgainObject.GetComponent<Button>();
        _QuitButton = _QuitObject.GetComponent<Button>();
        _QuitButton.onClick.AddListener(QuitButtonClicked);
        _PlayAgainButton.onClick.AddListener(PlayAgainButtonClicked);

    }

    private void PlayAgainButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void QuitButtonClicked()
    {
        Application.Quit();
    }

    public void updateWaveReachedAndScore(int wave, int thisscore)
    {
        waveReached = wave;
        score = thisscore;
        double coinsEarnt = Math.Round((double)score/10);
        

        int highScore = PlayerPrefs.GetInt("HighScore");
        _GameSummaryText.text = "You survived for " + waveReached + " nights \nYou earned "+coinsEarnt+" coins!";

        UserProfile.addCoins((int)coinsEarnt);

        if (score>highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            _GameSummaryScoreText.color = Color.green;
            _GameSummaryScoreText.text = "New High Score! \n High Score : " + score;
        }
        else
        {
            _GameSummaryScoreText.color = Color.red;
            _GameSummaryScoreText.text = "High Score : " + highScore + " \n Score: " + score;
                      
        }
       
    }

}

   
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
        _GameSummaryText.text = "You survived for " + waveReached + " nights \n Score: " + score;
    }

}

   
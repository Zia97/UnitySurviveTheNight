using Assets.Game.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndOfRoundController : MonoBehaviour
{
    public GameObject confirmButtonObject;
    public Button _confirmButton;

    public GameObject _repairButtonObject;
    public Button _repairButtonDecrease;


    public InputField _repairsHoursSelected;


    private GameObject _gameControllerObject;
    private GameController _gameController;




    private void Start()
    {

        confirmButtonObject = GameObject.FindWithTag("ConfirmButton");
        _gameControllerObject = GameObject.FindWithTag("GameController");
        _repairButtonObject = GameObject.FindWithTag("RepairButtonDecrease");


        _confirmButton = confirmButtonObject.GetComponent<Button>();
        _confirmButton.onClick.AddListener(ConfirmButtonClicked);

        _repairButtonDecrease = _repairButtonObject.GetComponent<Button>();
        _repairButtonDecrease.onClick.AddListener(repairDecreaseButtonClicked);

        

        if (_gameControllerObject != null)
        {
             _gameController = _gameControllerObject.GetComponent<GameController>();
        }

    }

    private void ConfirmButtonClicked()
    {
        _gameController.StartNextWave();
    }

    private void repairDecreaseButtonClicked()
    {
        Debug.Log("decrease button clicked");
    }
}

   
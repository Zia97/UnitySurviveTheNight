using Assets.Game.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndOfRoundController : MonoBehaviour
{
    public GameObject _confirmButtonObject;
    private Button _confirmButton;

    public GameObject _repairButtonDecreaseObject;
    private Button _repairButtonDecrease;

    public GameObject _repairButtonIncreaseObject;
    private Button _repairButtonIncrease;

    public GameObject _suppliesButtonIncreaseObject;
    private Button _suppliesButtonIncrease;

    public GameObject _suppliesButtonDecreaseObject;
    private Button _suppliesButtonDecrease;


    public InputField _repairsHoursSelected;

    private GameObject _gameControllerObject;
    private GameController _gameController;




    private void Start()
    {

        _confirmButtonObject = GameObject.FindWithTag("ConfirmButton");
        _gameControllerObject = GameObject.FindWithTag("GameController");
        _repairButtonDecreaseObject = GameObject.FindWithTag("RepairButtonDecrease");
        _repairButtonIncreaseObject = GameObject.FindWithTag("RepairButtonIncrease");
        _suppliesButtonIncreaseObject = GameObject.FindWithTag("SuppliesButtonIncrease");
        _suppliesButtonDecreaseObject = GameObject.FindWithTag("SuppliesButtonDecrease");


        _confirmButton = _confirmButtonObject.GetComponent<Button>();
        _confirmButton.onClick.AddListener(ConfirmButtonClicked);

        _repairButtonDecrease = _repairButtonDecreaseObject.GetComponent<Button>();
        _repairButtonDecrease.onClick.AddListener(repairDecreaseButtonClicked);

        _repairButtonIncrease = _repairButtonIncreaseObject.GetComponent<Button>();
        _repairButtonIncrease.onClick.AddListener(repairIncreaseButtonClicked);

        _suppliesButtonIncrease = _suppliesButtonIncreaseObject.GetComponent<Button>();
        _suppliesButtonIncrease.onClick.AddListener(suppliesIncreaseButtonClicked);

        _suppliesButtonDecrease = _suppliesButtonDecreaseObject.GetComponent<Button>();
        _suppliesButtonDecrease.onClick.AddListener(suppliesDecreaseButtonClicked);



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
        Debug.Log("repair decrease button clicked");
    }

    private void repairIncreaseButtonClicked()
    {
        Debug.Log("repair Increase button clicked");
    }

    private void suppliesIncreaseButtonClicked()
    {
        Debug.Log("supplies Increase button clicked");
    }

    private void suppliesDecreaseButtonClicked()
    {
        Debug.Log("supplies Decrease button clicked");
    }
}

   
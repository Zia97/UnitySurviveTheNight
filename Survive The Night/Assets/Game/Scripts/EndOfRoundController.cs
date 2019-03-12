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

    public GameObject _repairsHoursObject;
    public InputField _repairsHoursSelected;

    public GameObject _suppliesHoursObject;
    public InputField _suppliesHoursSelected;

    private GameObject _gameControllerObject;
    private GameController _gameController;

    private Canvas _summaryCanvas;
    public GameObject _summaryCanvasController;

    private GameObject _summaryControllerObject;
    private SummaryController _summaryController;


    private int _suppliesHoursSelectedValue = 6;
    private int _repairsHoursSelectedValue = 6;

    private void Start()
    {

        _confirmButtonObject = GameObject.FindWithTag("ConfirmButton");
        _gameControllerObject = GameObject.FindWithTag("GameController");
        _summaryCanvasController = GameObject.FindWithTag("SummaryCanvas");
        _summaryControllerObject = GameObject.FindWithTag("SummaryController");
        _repairButtonDecreaseObject = GameObject.FindWithTag("RepairButtonDecrease");
        _repairButtonIncreaseObject = GameObject.FindWithTag("RepairButtonIncrease");
        _suppliesButtonIncreaseObject = GameObject.FindWithTag("SuppliesButtonIncrease");
        _suppliesButtonDecreaseObject = GameObject.FindWithTag("SuppliesButtonDecrease");
        _suppliesHoursObject = GameObject.FindWithTag("SuppliesHoursSelected");
        _repairsHoursObject = GameObject.FindWithTag("RepairHoursSelected");


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

        _suppliesHoursSelected = _suppliesHoursObject.GetComponent<InputField>();
        _repairsHoursSelected = _repairsHoursObject.GetComponent<InputField>();

        _suppliesHoursSelected.text = _suppliesHoursSelectedValue.ToString();
        _repairsHoursSelected.text = _repairsHoursSelectedValue.ToString();

        
        _summaryCanvasController.SetActive(true);
        _summaryCanvas = _summaryCanvasController.GetComponent<Canvas>();
        _summaryCanvas.enabled = false;
        _summaryControllerObject.SetActive(true);
        if (_summaryController != null)
        {
            _summaryController = _summaryControllerObject.GetComponent<SummaryController>();
        }

        if (_gameControllerObject != null)
        {
             _gameController = _gameControllerObject.GetComponent<GameController>();
        }

    }

    private void ConfirmButtonClicked()
    {
        SearchForSupplies();
        _gameController.disableEndOfRoundlayer();
        _summaryCanvas.enabled = true;
        RepairBase();
    }

    private void SearchForSupplies()
    {
        if (_summaryController==null)
        {
            _summaryController = _summaryControllerObject.GetComponent<SummaryController>();
        }

            _summaryController.addWeaponToAvaliableWeapons("Qasim test");
    }

    private void repairDecreaseButtonClicked()
    {
        _repairsHoursSelectedValue = _repairsHoursSelectedValue - 1;
        if(_repairsHoursSelectedValue<0)
        {
            _repairsHoursSelectedValue = 0;
        }
        _repairsHoursSelected.text = _repairsHoursSelectedValue.ToString();

        _suppliesHoursSelectedValue = 12 - _repairsHoursSelectedValue;
        _suppliesHoursSelected.text = _suppliesHoursSelectedValue.ToString();
    }

    private void repairIncreaseButtonClicked()
    {
        _repairsHoursSelectedValue = _repairsHoursSelectedValue + 1;
        if (_repairsHoursSelectedValue > 12)
        {
            _repairsHoursSelectedValue = 12;
        }
        _repairsHoursSelected.text = _repairsHoursSelectedValue.ToString();

        _suppliesHoursSelectedValue = 12 - _repairsHoursSelectedValue;
        _suppliesHoursSelected.text = _suppliesHoursSelectedValue.ToString();
    }

    private void suppliesIncreaseButtonClicked()
    {
        _suppliesHoursSelectedValue = _suppliesHoursSelectedValue + 1;
        if (_suppliesHoursSelectedValue > 12)
        {
            _suppliesHoursSelectedValue = 12;
        }
        _suppliesHoursSelected.text = _suppliesHoursSelectedValue.ToString();

        _repairsHoursSelectedValue = 12 - _suppliesHoursSelectedValue;
        _repairsHoursSelected.text = _repairsHoursSelectedValue.ToString();
    }

    private void suppliesDecreaseButtonClicked()
    {
        _suppliesHoursSelectedValue = _suppliesHoursSelectedValue - 1;
        if (_suppliesHoursSelectedValue < 0)
        {
            _suppliesHoursSelectedValue = 0;
        }
        _suppliesHoursSelected.text = _suppliesHoursSelectedValue.ToString();

        _repairsHoursSelectedValue = 12 - _suppliesHoursSelectedValue;
        _repairsHoursSelected.text = _repairsHoursSelectedValue.ToString();
    }

    private void RepairBase()
    {
        if (_gameController == null)
        {
            Debug.Log("Game controller is null");
        }
        else
        {
            _gameController.RepairBase(_repairsHoursSelectedValue * 3);
        }
    }
}

   
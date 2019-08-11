//Author : Qasim Ziauddin

//This controller controls the actions at the end of each wave. Allows the user to repair base and search for supplies.
// Provides the results of the users decisions to the summary controller.

using System.Collections;
using System.Collections.Generic;
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

    public Text hammerText;

    private Dictionary<string, int> _avaliableWeapons = new Dictionary<string, int>();


    private int _suppliesHoursSelectedValue = 6;
    private int _repairsHoursSelectedValue = 6;

    private bool goldenHammer = false;
    private bool superHammer = false;

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
       _avaliableWeapons =_gameController.getAllAvaliableWeapons();
        SearchForSupplies();
        _gameController.disableEndOfRoundlayer();
        RepairBase();
        _summaryController.updateDropdownWeaponList();
        _summaryCanvas.enabled = true;
       
    }

    private void SearchForSupplies()
    {
        bool weaponFound = false;
        if (_summaryController == null)
        {
            _summaryController = _summaryControllerObject.GetComponent<SummaryController>();
        }

        int weaponProbability = 84 / 12;


        int random1 = Random.Range(0, 100);


        if (random1 < weaponProbability * _suppliesHoursSelectedValue)
        {
            int random2 = Random.Range(0, 100);
            if (random2 <= 25)
            {
                int random3 = Random.Range(50, 350);

                weaponFound = true;

                if (!_avaliableWeapons.ContainsKey("MP-5"))
                {
                    _summaryController.updateSuppliesFoundSummaryText("MP-5 (Submachine gun)"+ System.Environment.NewLine+"Building materials: "+random3);
                }
                else
                {                 
                    _summaryController.updateSuppliesFoundSummaryText("MP-5 (Submachine gun) - Duplicate"+ System.Environment.NewLine+"Building materials: " + random3);
                }

                _summaryController.addWeaponToAvaliableWeapons("MP-5");
                _gameController.addBuildingMaterials(random3);
            }
            else if (random2 > 25 && random2 <= 40)
            {
                int random3 = Random.Range(20, 250);
                weaponFound = true;

                if (!_avaliableWeapons.ContainsKey("Shotgun"))
                {
                    _summaryController.updateSuppliesFoundSummaryText("Shotgun" + System.Environment.NewLine + "Building materials: " + random3);
                }
                else
                {
                    _summaryController.updateSuppliesFoundSummaryText("Shotgun - Duplicate" + System.Environment.NewLine + "Building materials: " + random3);
                }

                _gameController.addBuildingMaterials(random3);
                _summaryController.addWeaponToAvaliableWeapons("Shotgun");
            }
            else if (random2 > 40 && random2 <= 50)
            {
                int random3 = Random.Range(10, 160);
                weaponFound = true;
                _summaryController.addWeaponToAvaliableWeapons("Scout");

                if (!_avaliableWeapons.ContainsKey("Scout"))
                {
                    _summaryController.updateSuppliesFoundSummaryText("Scout (Sniper Rifle)" + System.Environment.NewLine + "Building materials: " + random3);
                }
                else
                {
                    _summaryController.updateSuppliesFoundSummaryText("Scout (Sniper Rifle) - Duplicate" + System.Environment.NewLine + "Building materials: " + random3);
                }
                _gameController.addBuildingMaterials(random3);
            }
            else if(random2>50)
            {
                int random3 = Random.Range(80, 400);
                _summaryController.updateSuppliesFoundSummaryText("Building materials: " + random3);
                _gameController.addBuildingMaterials(random3);
            }
        }
        else
        {
            int random3 = Random.Range(150, 600);
            _summaryController.updateSuppliesFoundSummaryText("Building materials: " + random3);
            _gameController.addBuildingMaterials(random3);
        }

        if (_gameController.getNoOfNPCS() == 0)
        {
            int highProb = Random.Range(0, 100);

            if (highProb <= 45)
            {
                _gameController.setNoOfNPCS(1);
                _summaryController.updateSurvivorsFoundText("Surviors found: 1");
            }
            else
            {
                _summaryController.updateSurvivorsFoundText("Surviors found: 0");
            }
        }
        else if (_gameController.getNoOfNPCS() == 1)
        {
            int highProb = Random.Range(0, 100);
            if (highProb <= 25)
            {
                _gameController.setNoOfNPCS(2);
                _summaryController.updateSurvivorsFoundText("Surviors found: 1");
            }
            else
            {
                _summaryController.updateSurvivorsFoundText("Surviors found: 0");
            }
        }
        else if (_gameController.getNoOfNPCS() == 2)
        {
            int highProb = Random.Range(0, 100);
            if (highProb <= 10)
            {
                _gameController.setNoOfNPCS(3);
                _summaryController.updateSurvivorsFoundText("Surviors found: 1");
            }
            else
            {
                _summaryController.updateSurvivorsFoundText("Surviors found: 0");
            }
        }

        if(_gameController.getNoOfNPCS()==3)
        {
            _summaryController.updateSurvivorsFoundText("Surviors found: Maximum survivors found");
        }


        int hammerProb = Random.Range(0, 100);

        if(hammerProb==0)
        {
            goldenHammer = true;
           
        }
        if(hammerProb>=1 && hammerProb<=6)
        {
            superHammer = true;
        }

        if(superHammer)
        {
            hammerText.text = "Your base will currently be repaired at 6% per hour.      (Super Hammer)";
        }
        if(goldenHammer)
        {
            hammerText.text = "Your base will currently be repaired at 9% per hour.      (Golden Hammer)";
        }
    }

    public bool isGavaliable()
    {
        return goldenHammer;
    }

    public bool isSavaliable()
    {
        return superHammer;
    }

    private void repairDecreaseButtonClicked()
    {
        _repairsHoursSelectedValue = _repairsHoursSelectedValue - 1;
        if (_repairsHoursSelectedValue < 0)
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
            double previousHealth = _gameController.getWallHealth();
            
            if(goldenHammer)
            {
                _gameController.RepairBase(_repairsHoursSelectedValue * 9);
            }
            else if(superHammer)
            {
                _gameController.RepairBase(_repairsHoursSelectedValue * 6);
            }
            else
            {
                _gameController.RepairBase(_repairsHoursSelectedValue * 3);
            }

            double newHealth = _gameController.getWallHealth();

            _summaryController.updateBaseRepairsText("Your base was repaired from " + previousHealth + "% -> " + newHealth + "%");
        }
    }
}


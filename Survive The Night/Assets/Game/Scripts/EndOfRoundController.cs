﻿//Author : Qasim Ziauddin

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

    public GameObject _roundEndInfoButtonObject;
    private Button _roundEndButton;

    public GameObject _resumeButtonObject;
    private Button _resumeButton;

    public GameObject BlurCanvasObject;
    Canvas BlurCanvas;

    public GameObject roundEndInfo;
    Canvas PausedCanvas;

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

        BlurCanvasObject = GameObject.Find("BlurCanvas");
        BlurCanvas = BlurCanvasObject.GetComponent<Canvas>();
        BlurCanvas.enabled = false;

        roundEndInfo = GameObject.Find("roundEndInfo");
        PausedCanvas = roundEndInfo.GetComponent<Canvas>();
        PausedCanvas.enabled = false;

        _resumeButton = _resumeButtonObject.GetComponent<Button>();
        _resumeButton.onClick.AddListener(ResumeButtonClicked);

        _roundEndInfoButtonObject = GameObject.Find("roundEndInfoButton");
        _roundEndButton = _roundEndInfoButtonObject.GetComponent<Button>();
        _roundEndButton.onClick.AddListener(infoButtonClicked);


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
        _avaliableWeapons = _gameController.getAllAvaliableWeapons();
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
            Debug.Log("should find something");
            int random2 = Random.Range(0, 100);
            if (random2 <= 25)
            {
                int random3 = Random.Range(50, 350);

                weaponFound = true;


                _summaryController.updateSuppliesFoundSummaryText("MP-5 (Submachine gun)" + System.Environment.NewLine + "Building materials: " + random3);

                _summaryController.addWeaponToAvaliableWeapons("MP-5");
                _gameController.addBuildingMaterials(random3);
            }
            else if (random2 > 25 && random2 <= 35)
            {
                int random3 = Random.Range(20, 250);
                weaponFound = true;

                _summaryController.updateSuppliesFoundSummaryText("Revolver" + System.Environment.NewLine + "Building materials: " + random3);


                _gameController.addBuildingMaterials(random3);
                _summaryController.addWeaponToAvaliableWeapons("Revolver");
            }
            else if (random2 > 35 && random2 <= 45)
            {
                int random3 = Random.Range(10, 160);
                weaponFound = true;
                _summaryController.addWeaponToAvaliableWeapons("Shotgun");


                _summaryController.updateSuppliesFoundSummaryText("Shotgun" + System.Environment.NewLine + "Building materials: " + random3);

                _gameController.addBuildingMaterials(random3);
            }
            else if (random2 > 45 && random2 <= 55)
            {
                int random3 = Random.Range(10, 160);
                weaponFound = true;
                _summaryController.addWeaponToAvaliableWeapons("Scout");


                _summaryController.updateSuppliesFoundSummaryText("Scout (Sniper)" + System.Environment.NewLine + "Building materials: " + random3);

                _gameController.addBuildingMaterials(random3);
            }
            else if (random2 > 55 && random2 <= 60)
            {
                int random3 = Random.Range(10, 160);
                weaponFound = true;
                _summaryController.addWeaponToAvaliableWeapons("M-249");

                _summaryController.updateSuppliesFoundSummaryText("M-249" + System.Environment.NewLine + "Building materials: " + random3);

                _gameController.addBuildingMaterials(random3);
            }
            else if (random2 > 60 && random2 <= 65)
            {
                int random3 = Random.Range(10, 160);
                weaponFound = true;
                _summaryController.addWeaponToAvaliableWeapons("M-4Laser");


                _summaryController.updateSuppliesFoundSummaryText("M4-Lazer" + System.Environment.NewLine + "Building materials: " + random3);

                _gameController.addBuildingMaterials(random3);
            }
            else if (random2 > 65 && random2 <= 70)
            {
                int random3 = Random.Range(10, 160);
                weaponFound = true;
                _summaryController.addWeaponToAvaliableWeapons("RocketLauncher");


                _summaryController.updateSuppliesFoundSummaryText("RocketLauncher" + System.Environment.NewLine + "Building materials: " + random3);

                _gameController.addBuildingMaterials(random3);
            }
            else if (random2 > 70 && random2 <= 72)
            {
                int random3 = Random.Range(10, 160);
                weaponFound = true;
                _summaryController.addWeaponToAvaliableWeapons("RPG");


                _summaryController.updateSuppliesFoundSummaryText("RPG" + System.Environment.NewLine + "Building materials: " + random3);

                _gameController.addBuildingMaterials(random3);
            }
            else if (random2 > 72)
            {
                int random3 = Random.Range(80, 400);

                _gameController.addBuildingMaterials(random3);

                int hammerProb = Random.Range(0, 100);

                if (hammerProb == 0)
                {
                    goldenHammer = true;
                    _summaryController.updateSuppliesFoundSummaryText("Golden Hammer (repairs)" + System.Environment.NewLine + "Building materials: " + random3);

                }
                else if (hammerProb >= 1 && hammerProb <= 6)
                {
                    superHammer = true;
                    _summaryController.updateSuppliesFoundSummaryText("Super Hammer (repairs)" + System.Environment.NewLine + "Building materials: " + random3);
                }
                else
                {
                    _summaryController.updateSuppliesFoundSummaryText("Building materials: " + random3);
                }
            }
        }
        else
        {
            Debug.Log("Didnt find anything");
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

        if (_gameController.getNoOfNPCS() == 3)
        {
            _summaryController.updateSurvivorsFoundText("Surviors found: Maximum survivors found");
        }

        if (superHammer)
        {
            hammerText.text = "Your base will currently be repaired at 6% per hour.      (Super Hammer)";
        }
        if (goldenHammer)
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

            if (goldenHammer)
            {
                _gameController.RepairBase(_repairsHoursSelectedValue * 9);
            }
            else if (superHammer)
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

    private void ResumeButtonClicked()
    {
        if (Pause.isPaused)
        {
            Time.timeScale = 1;
            Pause.isPaused = false;
            BlurCanvas.enabled = false;
            PausedCanvas.enabled = false;
        }
    }

    private void infoButtonClicked()
    {
        if (Pause.isPaused)
        {
            Time.timeScale = 1;
            Pause.isPaused = false;
            BlurCanvas.enabled = false;
            PausedCanvas.enabled = false;
        }
        else if (!Pause.isPaused)
        {
            Time.timeScale = 0;
            Pause.isPaused = true;
            BlurCanvas.enabled = true;
            PausedCanvas.enabled = true;
        }

    }
}


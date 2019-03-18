using Assets.Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SummaryController : MonoBehaviour
{
    public GameObject _startNextWaveObject;
    private Button _startNextWaveButton;

    public GameObject _workshopButtonObject;
    public GameObject _workshopController;
    public Button _workshopButton;

    private GameObject _gameControllerObject;

    private GameController _gameController;

    private ArrayList _avaliableWeapons = new ArrayList();

    private string _primaryWeapon;
    private string _secondaryWeapon;

    public Dropdown primaryWeaponDropdown;
    public Dropdown secondaryWeaponDropdown;

    public Canvas workshopCanvas;
    public GameObject workshopObject;

    public Canvas mainGameCanvas;

    public Canvas summaryCanvas;

    public Text baseRepairsText;
    public Text suppliesFoundSummary;

    private void Start()
    {
        _startNextWaveObject = GameObject.FindWithTag("StartNextWave");
        _gameControllerObject = GameObject.FindWithTag("GameController");
        _workshopController = GameObject.FindWithTag("WorkshopController");
        _workshopButtonObject = GameObject.FindWithTag("WorkshopButton");

        _startNextWaveButton = _startNextWaveObject.GetComponent<Button>();
        _startNextWaveButton.onClick.AddListener(ConfirmButtonClicked);

        _workshopButton = _workshopButtonObject.GetComponent<Button>();
        _workshopButton.onClick.AddListener(WorkshopButtonClicked);

        if (_gameControllerObject != null)
        {
             _gameController = _gameControllerObject.GetComponent<GameController>();
            _avaliableWeapons = _gameController.getAllAvaliableWeapons();
        }

        workshopObject = GameObject.FindWithTag("WorkshopCanvas");
        workshopObject.SetActive(true);
        workshopCanvas = workshopObject.GetComponent<Canvas>();

        workshopObject = GameObject.FindWithTag("WorkshopCanvas");
        workshopObject.SetActive(true);
        workshopCanvas = workshopObject.GetComponent<Canvas>();
    }

    private void WorkshopButtonClicked()
    {
        mainGameCanvas.enabled = false;
        workshopCanvas.enabled = true;
        summaryCanvas.enabled = false;
        _workshopController.GetComponent<WorkshopController>().InstantiateTurrets();
       
    }

    private void ConfirmButtonClicked()
    {
        workshopCanvas.enabled = false;
        mainGameCanvas.enabled = true;
        _primaryWeapon = primaryWeaponDropdown.options[primaryWeaponDropdown.value].text;
        _secondaryWeapon = secondaryWeaponDropdown.options[secondaryWeaponDropdown.value].text;
        _gameController.updateSelectedWeapons(_primaryWeapon,_secondaryWeapon);
        _gameController.StartNextWave();
    }

    public void addWeaponToAvaliableWeapons(string weapon)
    {
        _avaliableWeapons.Add(weapon);
        _gameController.updateAllAvaliableWeapons(_avaliableWeapons);
    }

    public void updateBaseRepairsText(string newText)
    {
        baseRepairsText.text = newText;
    }

    public void updateSuppliesFoundSummary(string newText)
    {
        suppliesFoundSummary.text = newText;
    }

    public void updateDropdownWeaponList()
    {
        _avaliableWeapons = _gameController.getAllAvaliableWeapons();
        List<string> results = _avaliableWeapons.Cast<string>().Distinct().ToList();
        primaryWeaponDropdown.ClearOptions();
        secondaryWeaponDropdown.ClearOptions();
        primaryWeaponDropdown.AddOptions(results);
        secondaryWeaponDropdown.AddOptions(results);
    }

}

   
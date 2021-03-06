﻿//Author : Qasim Ziauddin

//Controller handling the summary screen. Values passed to it from the EndOfRoundController.cs. Allows the user to navigate to workshop
//or NPC armoury. Lets the player select their weapons. 
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

    public GameObject _armoryButtonObject;
    public Button _armoryButton;
    public Canvas armoryCanvas;
    public GameObject armoryObject;
    public NPCArmoryController npcArmoryController;

    private GameObject _gameControllerObject;

    private GameController _gameController;

    private Dictionary<string,int> _avaliableWeapons = new Dictionary<string, int>();

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

    public Text survivorsFoundText;
    private object primaryWeaponDropdownValue;
    private object secondaryWeaponDropdownValue;


    public GameObject _roundEndInfoButtonObject;
    private Button _roundEndButton;

    public GameObject _resumeButtonObject;
    private Button _resumeButton;

    public GameObject BlurCanvasObject;
    Canvas BlurCanvas;

    public GameObject summaryEndInfo;
    Canvas PausedCanvas;

    private void Start()
    {

        BlurCanvasObject = GameObject.Find("BlurCanvas");
        BlurCanvas = BlurCanvasObject.GetComponent<Canvas>();
        BlurCanvas.enabled = false;

        summaryEndInfo = GameObject.Find("summaryInfo");
        PausedCanvas = summaryEndInfo.GetComponent<Canvas>();
        PausedCanvas.enabled = false;

        _resumeButton = _resumeButtonObject.GetComponent<Button>();
        _resumeButton.onClick.AddListener(ResumeButtonClicked);

        _roundEndInfoButtonObject = GameObject.Find("summaryInfoButton");
        _roundEndButton = _roundEndInfoButtonObject.GetComponent<Button>();
        _roundEndButton.onClick.AddListener(infoButtonClicked);


        _startNextWaveObject = GameObject.FindWithTag("StartNextWave");
        _gameControllerObject = GameObject.FindWithTag("GameController");
        _workshopController = GameObject.FindWithTag("WorkshopController");
        _workshopButtonObject = GameObject.FindWithTag("WorkshopButton");

        npcArmoryController = GameObject.FindWithTag("ArmoryController").GetComponent<NPCArmoryController>();

        _armoryButtonObject = GameObject.FindWithTag("ArmoryButton");
        _armoryButton = _armoryButtonObject.GetComponent<Button>();
        _armoryButton.onClick.AddListener(ArmoryButtonClicked);

        _startNextWaveButton = _startNextWaveObject.GetComponent<Button>();
        _startNextWaveButton.onClick.AddListener(ConfirmButtonClicked);

        _workshopButton = _workshopButtonObject.GetComponent<Button>();
        _workshopButton.onClick.AddListener(WorkshopButtonClicked);

        if (_gameControllerObject != null)
        {
             _gameController = _gameControllerObject.GetComponent<GameController>();
            _avaliableWeapons = _gameController.getAllAvaliableWeapons();
        }

        primaryWeaponDropdown.onValueChanged.AddListener(delegate {
            primaryWeaponDropdownChanged(primaryWeaponDropdownValue);
        });

        secondaryWeaponDropdown.onValueChanged.AddListener(delegate {
            secondaryWeaponDropdownChanged(secondaryWeaponDropdownValue);
        });

        workshopObject = GameObject.FindWithTag("WorkshopCanvas");
        workshopObject.SetActive(true);
        workshopCanvas = workshopObject.GetComponent<Canvas>();

        updateDropdownWeaponList();
    }

    private void secondaryWeaponDropdownChanged(object secondaryWeaponDropdownValue)
    {
        _primaryWeapon = primaryWeaponDropdown.options[primaryWeaponDropdown.value].text;
        _secondaryWeapon = secondaryWeaponDropdown.options[secondaryWeaponDropdown.value].text;
        _gameController.updateSelectedWeapons(_primaryWeapon, _secondaryWeapon);
    }

    private void primaryWeaponDropdownChanged(object primaryWeaponDropdownValue)
    {
        _primaryWeapon = primaryWeaponDropdown.options[primaryWeaponDropdown.value].text;
        _secondaryWeapon = secondaryWeaponDropdown.options[secondaryWeaponDropdown.value].text;
        _gameController.updateSelectedWeapons(_primaryWeapon, _secondaryWeapon);
    }

    private void ArmoryButtonClicked()
    {
        mainGameCanvas.enabled = false;
        npcArmoryController.updateWeaponsDropdown();
        npcArmoryController.updateLayout();
        npcArmoryController.InstantiateNPCS();
        armoryCanvas.enabled = true;
        summaryCanvas.enabled = false;
    }

    private void WorkshopButtonClicked()
    {
        mainGameCanvas.enabled = false;
        workshopCanvas.enabled = true;
        summaryCanvas.enabled = false;
        _workshopController.GetComponent<WorkshopController>().updateAvaliableMaterialsText();
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
        if(_avaliableWeapons.ContainsKey(weapon))
        {
            int old = _avaliableWeapons[weapon];
            _avaliableWeapons[weapon] = 1+old;
        }
        else
        {
            _avaliableWeapons.Add(weapon, 1);
        }
        
        _gameController.updateAllAvaliableWeapons(_avaliableWeapons);
    }

    public void updateBaseRepairsText(string newText)
    {
        baseRepairsText.text = newText;
    }

    public void updateSuppliesFoundSummaryText(string newText)
    {
        suppliesFoundSummary.text = newText;
    }

    public void updateSurvivorsFoundText(string newText)
    {
        survivorsFoundText.text = newText;
    }

    public void updateDropdownWeaponList()
    {
        _avaliableWeapons = _gameController.getAllAvaliableWeapons();
        List<string> results = _avaliableWeapons.Keys.Cast<string>().Distinct().ToList();

        primaryWeaponDropdown.ClearOptions();
        secondaryWeaponDropdown.ClearOptions();
        primaryWeaponDropdown.AddOptions(results);
        secondaryWeaponDropdown.AddOptions(results);
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

   
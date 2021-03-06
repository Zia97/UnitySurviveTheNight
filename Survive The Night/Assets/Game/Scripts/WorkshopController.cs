﻿//Author : Qasim Ziauddin

//Controller handling the workshop. Responsible for allowing the user to build turrrets and deploy them in set locations on the battlefield.
using Assets.Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopController : MonoBehaviour
{
    public GameObject _workshopBackButtonObject;
    public Button _workshopBackButton;

    public GameObject _buildTurret1Object;
    public Button _buildTurret1Button;

    public GameObject _buildTurret2Object;
    public Button _buildTurret2Button;

    public GameObject _buildTurret3Object;
    public Button _buildTurret3Button;

    public GameObject _buildTurret6Object;
    public Button _buildTurret6Button;

    private GameObject _gameControllerObject;
    private GameController _gameController;

    public GameObject turret1;
    public GameObject turret2;
    public GameObject turret3;
    public GameObject turret6;

    public GameObject turret1Ref;
    public GameObject turret2Ref;
    public GameObject turret3Ref;
    public GameObject turret6Ref;

    private Dictionary<string, int> _avaliableWeapons = new Dictionary<string, int>();

    private Dictionary<string, int> avaliableTurrets = new Dictionary<string, int>();

    private string _primaryWeapon;
    private string _secondaryWeapon;

    private int avaliableMaterials = 0;

    public Dropdown turret1Dropdown;
    public Dropdown turret2Dropdown;
    public Dropdown turret3Dropdown;

    private int turret1Count = 0;
    private int turret2Count = 0;
    private int turret3Count = 0;
    private int turret6Count = 0;


    public Canvas workshopCanvas;
    public GameObject workshopObject;

    public Canvas summaryCanvas;

    public Text avaliableMaterialsText;
    public Text workshopText;

    public Text basicBuilt;
    public Text mediumBuilt;
    public Text heavyBuilt;
    public Text superBuilt;
    private object turret1Value;
    private object turret2Value;
    private object turret3Value;

    public GameObject workshopInfoButtonObject;
    private Button _workshopInfoButton;

    public GameObject _resumeButtonObject;
    private Button _resumeButton;

    public GameObject BlurCanvasObject;
    Canvas BlurCanvas;

    public GameObject workshopCanvasObject;
    Canvas PausedCanvas;

    private void Start()
    {
        _gameControllerObject = GameObject.FindWithTag("GameController");

        _workshopBackButtonObject = GameObject.FindWithTag("WorkshopBackButton");
        _workshopBackButton = _workshopBackButtonObject.GetComponent<Button>();
        _workshopBackButton.onClick.AddListener(WorkshopBackButtonClicked);

        _buildTurret1Object = GameObject.FindWithTag("Turret1Button");
        _buildTurret1Button = _buildTurret1Object.GetComponent<Button>();
        _buildTurret1Button.onClick.AddListener(BuildTurret1ButtonClicked);

        _buildTurret2Object = GameObject.FindWithTag("Turret2Button");
        _buildTurret2Button = _buildTurret2Object.GetComponent<Button>();
        _buildTurret2Button.onClick.AddListener(BuildTurret2ButtonClicked);

        _buildTurret3Object = GameObject.FindWithTag("Turret3Button");
        _buildTurret3Button = _buildTurret3Object.GetComponent<Button>();
        _buildTurret3Button.onClick.AddListener(BuildTurret3ButtonClicked);

        _buildTurret6Object = GameObject.FindWithTag("Turret6Button");
        _buildTurret6Button = _buildTurret6Object.GetComponent<Button>();
        _buildTurret6Button.onClick.AddListener(BuildTurret6ButtonClicked);

        BlurCanvasObject = GameObject.Find("BlurCanvas");
        BlurCanvas = BlurCanvasObject.GetComponent<Canvas>();
        BlurCanvas.enabled = false;

        workshopCanvasObject = GameObject.Find("workshopInfo");
        PausedCanvas = workshopCanvasObject.GetComponent<Canvas>();
        PausedCanvas.enabled = false;

        _resumeButton = _resumeButtonObject.GetComponent<Button>();
        _resumeButton.onClick.AddListener(ResumeButtonClicked);

        workshopInfoButtonObject = GameObject.Find("workshopInfoButton");
        _workshopInfoButton = workshopInfoButtonObject.GetComponent<Button>();
        _workshopInfoButton.onClick.AddListener(infoButtonClicked);

        if (_gameControllerObject != null)
        {
             _gameController = _gameControllerObject.GetComponent<GameController>();
            _avaliableWeapons = _gameController.getAllAvaliableWeapons();
        }

        workshopObject = GameObject.FindWithTag("WorkshopCanvas");
        workshopObject.SetActive(true);
        workshopCanvas = workshopObject.GetComponent<Canvas>();

        turret1Dropdown.onValueChanged.AddListener(delegate {
            turret1DropdownValueChanged(turret1Value);
        });

        turret2Dropdown.onValueChanged.AddListener(delegate {
            turret2DropdownValueChanged(turret2Value);
        });

        turret3Dropdown.onValueChanged.AddListener(delegate {
            turret3DropdownValueChanged(turret3Value);
        });

        basicBuilt.color = Color.red;
        mediumBuilt.color = Color.red;
        heavyBuilt.color = Color.red;
        superBuilt.color = Color.red;

        basicBuilt.text = "0";
        mediumBuilt.text = "0";
        heavyBuilt.text = "0";
        superBuilt.text = "0";

        updateDropdownTurretList();
    }

    private void turret3DropdownValueChanged(object turret3Value)
    {
        int turret1temp = 0;
        int turret2temp = 0;
        int turret3temp = 0;
        int turret6temp = 0;

        if (turret1Dropdown.options[turret1Dropdown.value].text.Equals("Basic Turret"))
        {
            turret1temp++;
        }
        if (turret1Dropdown.options[turret1Dropdown.value].text.Equals("Medium Turret"))
        {
            turret2temp++;
        }
        if (turret1Dropdown.options[turret1Dropdown.value].text.Equals("Heavy Turret"))
        {
            turret3temp++;
        }
        if (turret1Dropdown.options[turret1Dropdown.value].text.Equals("Super Turret"))
        {
            turret6temp++;
        }

        if (turret2Dropdown.options[turret2Dropdown.value].text.Equals("Basic Turret"))
        {
            turret1temp++;
        }
        if (turret2Dropdown.options[turret2Dropdown.value].text.Equals("Medium Turret"))
        {
            turret2temp++;
        }
        if (turret2Dropdown.options[turret2Dropdown.value].text.Equals("Heavy Turret"))
        {
            turret3temp++;
        }
        if (turret2Dropdown.options[turret2Dropdown.value].text.Equals("Super Turret"))
        {
            turret6temp++;
        }
        avaliableTurrets = _gameController.getTurrets();

        if (turret3Dropdown.options[turret3Dropdown.value].text.Equals("Basic Turret"))
        {
            var tempOptions = turret3Dropdown.options;

            if (avaliableTurrets["Turret 1"] <= turret1temp)
            {
                foreach(var option in tempOptions)
                {
                    if(option.text.Equals("Basic Turret"))
                    {
                        turret3Dropdown.value = 0;
                        workshopText.text = "Build more basic turrets!";
                        workshopText.color = Color.red;
                    }
                }
            }
            else
            {
                turret1Count++;
            }
            
        }

        if (turret3Dropdown.options[turret3Dropdown.value].text.Equals("Medium Turret"))
        {
            var tempOptions = turret3Dropdown.options;

            if (avaliableTurrets["Turret 2"] <= turret2temp)
            {
                foreach (var option in tempOptions)
                {
                    if (option.text.Equals("Medium Turret"))
                    {
                        turret3Dropdown.value = 0;
                        workshopText.text = "Build more medium turrets!";
                        workshopText.color = Color.red;
                    }
                }
            }
            else
            {
                turret2Count++;
            }

        }

        if (turret3Dropdown.options[turret3Dropdown.value].text.Equals("Heavy Turret"))
        {
            var tempOptions = turret3Dropdown.options;

            if (avaliableTurrets["Turret 3"] <= turret3temp)
            {
                foreach (var option in tempOptions)
                {
                    if (option.text.Equals("Heavy Turret"))
                    {
                        turret3Dropdown.value = 0;
                        workshopText.text = "Build more heavy turrets!";
                        workshopText.color = Color.red;
                    }
                }
            }
            else
            {
                turret3Count++;
            }

        }

        if (turret3Dropdown.options[turret3Dropdown.value].text.Equals("Super Turret"))
        {
            var tempOptions = turret3Dropdown.options;

            if (avaliableTurrets["Turret 6"] <= turret6temp)
            {
                foreach (var option in tempOptions)
                {
                    if (option.text.Equals("Super Turret"))
                    {
                        turret3Dropdown.value = 0;
                        workshopText.text = "Build more super turrets!";
                        workshopText.color = Color.red;
                    }
                }
            }
            else
            {
                turret6Count++;
            }

        }
        _gameController.selectTurrets(turret1Dropdown.options[turret1Dropdown.value].text, turret2Dropdown.options[turret2Dropdown.value].text, turret3Dropdown.options[turret3Dropdown.value].text);
    }

    private void turret2DropdownValueChanged(object turret2Value)
    {
        int turret1temp = 0;
        int turret2temp = 0;
        int turret3temp = 0;
        int turret6temp = 0;

        if (turret1Dropdown.options[turret1Dropdown.value].text.Equals("Basic Turret"))
        {
            turret1temp++;
        }
        if (turret1Dropdown.options[turret1Dropdown.value].text.Equals("Medium Turret"))
        {
            turret2temp++;
        }
        if (turret1Dropdown.options[turret1Dropdown.value].text.Equals("Heavy Turret"))
        {
            turret3temp++;
        }
        if (turret1Dropdown.options[turret1Dropdown.value].text.Equals("Super Turret"))
        {
            turret6temp++;
        }

        if (turret3Dropdown.options[turret3Dropdown.value].text.Equals("Basic Turret"))
        {
            turret1temp++;
        }
        if (turret3Dropdown.options[turret3Dropdown.value].text.Equals("Medium Turret"))
        {
            turret2temp++;
        }
        if (turret3Dropdown.options[turret3Dropdown.value].text.Equals("Heavy Turret"))
        {
            turret3temp++;
        }
        if (turret3Dropdown.options[turret3Dropdown.value].text.Equals("Super Turret"))
        {
            turret6temp++;
        }

        avaliableTurrets = _gameController.getTurrets();
        if (turret2Dropdown.options[turret2Dropdown.value].text.Equals("Basic Turret"))
        {
            if (avaliableTurrets["Turret 1"] <= turret1temp)
            {
                var tempOptions = turret2Dropdown.options;

                foreach (var option in tempOptions)
                {
                    if (option.text.Equals("Basic Turret"))
                    {
                        turret2Dropdown.value = 0;
                        workshopText.text = "Build more basic turrets!";
                        workshopText.color = Color.red;
                    }
                }
               
            }
            else
            {
                turret1Count++;
            }
        }

        if (turret2Dropdown.options[turret2Dropdown.value].text.Equals("Medium Turret"))
        {
            if (avaliableTurrets["Turret 2"] <= turret2temp)
            {
                var tempOptions = turret2Dropdown.options;

                foreach (var option in tempOptions)
                {
                    if (option.text.Equals("Medium Turret"))
                    {
                        turret2Dropdown.value = 0;
                        workshopText.text = "Build more medium turrets!";
                        workshopText.color = Color.red;
                    }
                }

            }
            else
            {
                turret2Count++;
            }
        }

        if (turret2Dropdown.options[turret2Dropdown.value].text.Equals("Heavy Turret"))
        {
            if (avaliableTurrets["Turret 3"] <= turret3temp)
            {
                var tempOptions = turret2Dropdown.options;

                foreach (var option in tempOptions)
                {
                    if (option.text.Equals("Heavy Turret"))
                    {
                        turret2Dropdown.value = 0;
                        workshopText.text = "Build more heavy turrets!";
                        workshopText.color = Color.red;
                    }
                }

            }
            else
            {
                turret3Count++;
            }
        }

        if (turret2Dropdown.options[turret2Dropdown.value].text.Equals("Super Turret"))
        {
            if (avaliableTurrets["Turret 6"] <= turret6temp)
            {
                var tempOptions = turret2Dropdown.options;

                foreach (var option in tempOptions)
                {
                    if (option.text.Equals("Super Turret"))
                    {
                        turret2Dropdown.value = 0;
                        workshopText.text = "Build more super turrets!";
                        workshopText.color = Color.red;
                    }
                }

            }
            else
            {
                turret6Count++;
            }
        }
        _gameController.selectTurrets(turret1Dropdown.options[turret1Dropdown.value].text, turret2Dropdown.options[turret2Dropdown.value].text, turret3Dropdown.options[turret3Dropdown.value].text);
    }

    private void turret1DropdownValueChanged(object turret1Value)
    {

        int turret1temp = 0;
        int turret2temp = 0;
        int turret3temp = 0;
        int turret6temp = 0;

        if (turret2Dropdown.options[turret2Dropdown.value].text.Equals("Basic Turret"))
        {
            turret1temp++;
        }
        if (turret2Dropdown.options[turret2Dropdown.value].text.Equals("Medium Turret"))
        {
            turret2temp++;
        }
        if (turret2Dropdown.options[turret2Dropdown.value].text.Equals("Heavy Turret"))
        {
            turret3temp++;
        }
        if (turret2Dropdown.options[turret2Dropdown.value].text.Equals("Super Turret"))
        {
            turret6temp++;
        }

        if (turret3Dropdown.options[turret3Dropdown.value].text.Equals("Basic Turret"))
        {
            turret1temp++;
        }
        if (turret3Dropdown.options[turret3Dropdown.value].text.Equals("Medium Turret"))
        {
            turret2temp++;
        }
        if (turret3Dropdown.options[turret3Dropdown.value].text.Equals("Heavy Turret"))
        {
            turret3temp++;
        }
        if (turret3Dropdown.options[turret3Dropdown.value].text.Equals("Super Turret"))
        {
            turret6temp++;
        }
        avaliableTurrets = _gameController.getTurrets();
        if (turret1Dropdown.options[turret1Dropdown.value].text.Equals("Basic Turret"))
        {
            var tempOptions = turret1Dropdown.options;
           
            if (avaliableTurrets["Turret 1"] <= turret1temp)
            {
                foreach (var option in tempOptions)
                {
                    if (option.text.Equals("Basic Turret"))
                    {
                        turret1Dropdown.value = 0;
                        workshopText.text = "Build more basic turrets!";
                        workshopText.color = Color.red;
                    }
                }
            }
            else
            {
                turret1Count++;
            }
           
        }

        if (turret1Dropdown.options[turret1Dropdown.value].text.Equals("Medium Turret"))
        {
            var tempOptions = turret1Dropdown.options;

            if (avaliableTurrets["Turret 2"] <= turret2temp)
            {
                foreach (var option in tempOptions)
                {
                    if (option.text.Equals("Medium Turret"))
                    {
                        turret1Dropdown.value = 0;
                        workshopText.text = "Build more medium turrets!";
                        workshopText.color = Color.red;
                    }
                }
            }
            else
            {
                turret2Count++;
            }

        }

        if (turret1Dropdown.options[turret1Dropdown.value].text.Equals("Heavy Turret"))
        {
            var tempOptions = turret1Dropdown.options;

            if (avaliableTurrets["Turret 3"] <= turret3temp)
            {
                foreach (var option in tempOptions)
                {
                    if (option.text.Equals("Heavy Turret"))
                    {
                        turret1Dropdown.value = 0;
                        workshopText.text = "Build more heavy turrets!";
                        workshopText.color = Color.red;
                    }
                }
            }
            else
            {
                turret3Count++;
            }

        }

        if (turret1Dropdown.options[turret1Dropdown.value].text.Equals("Super Turret"))
        {
            var tempOptions = turret1Dropdown.options;

            if (avaliableTurrets["Turret 6"] <= turret6temp)
            {
                foreach (var option in tempOptions)
                {
                    if (option.text.Equals("Super Turret"))
                    {
                        turret1Dropdown.value = 0;
                        workshopText.text = "Build more super turrets!";
                        workshopText.color = Color.red;
                    }
                }
            }
            else
            {
                turret6Count++;
            }

        }
        _gameController.selectTurrets(turret1Dropdown.options[turret1Dropdown.value].text, turret2Dropdown.options[turret2Dropdown.value].text, turret3Dropdown.options[turret3Dropdown.value].text);
    }

    private void BuildTurret6ButtonClicked()
    {
        if(avaliableMaterials>=7000) 
        {
            turret6Count++;
            avaliableMaterials = avaliableMaterials - 7000;
            avaliableMaterialsText.text = "Materials avaliable: " + avaliableMaterials;
            _gameController.updateBuildingMaterials(avaliableMaterials);
            _gameController.addTurret("Turret 6");
            workshopText.text = "Super turret built!";
            workshopText.color = Color.green;
            updateDropdownTurretList();
            updateTurretCountText();
        }
        else
        {
            workshopText.text = "Not enough materials!";
            workshopText.color = Color.red;
        }
        
    }

    private void BuildTurret3ButtonClicked()
    {
        if (avaliableMaterials >= 5000) 
        {
            turret3Count++;
            avaliableMaterials = avaliableMaterials - 5000;
            avaliableMaterialsText.text = "Materials avaliable: " + avaliableMaterials;
            _gameController.updateBuildingMaterials(avaliableMaterials);
            _gameController.addTurret("Turret 3");
            workshopText.text = "Heavy turret built!";
            workshopText.color = Color.green;
            updateDropdownTurretList();
            updateTurretCountText();
        }
        else
        {
            workshopText.text = "Not enough materials!";
            workshopText.color = Color.red;
        }
      
    }

    private void updateTurretCountText()
    {
        basicBuilt.text = turret1Count.ToString();
        mediumBuilt.text = turret2Count.ToString();
        heavyBuilt.text = turret3Count.ToString();
        superBuilt.text = turret6Count.ToString();

        if(turret1Count>0)
        {
            basicBuilt.color = Color.green;
        }
        if (turret2Count > 0)
        {
            mediumBuilt.color = Color.green;
        }
        if (turret3Count > 0)
        {
            heavyBuilt.color = Color.green;
        }
        if (turret6Count > 0)
        {
            superBuilt.color = Color.green;
        }
    }

    private void BuildTurret2ButtonClicked()
    {
        if (avaliableMaterials >= 3000) 
        {
            turret2Count++;
            avaliableMaterials = avaliableMaterials - 3000;
            avaliableMaterialsText.text = "Materials avaliable: " + avaliableMaterials;
            _gameController.updateBuildingMaterials(avaliableMaterials);
            _gameController.addTurret("Turret 2");
            workshopText.text = "Medium turret built!";
            workshopText.color = Color.green;
            updateDropdownTurretList();
            updateTurretCountText();
        }
        else
        {
            workshopText.text = "Not enough materials!";
            workshopText.color = Color.red;
        }
        
    }

    //TODO
    private void BuildTurret1ButtonClicked()
    {
        if (avaliableMaterials >= 1500) 
        {
            turret1Count++;
            avaliableMaterials = avaliableMaterials - 1500;
            avaliableMaterialsText.text = "Materials avaliable: " + avaliableMaterials;
            _gameController.updateBuildingMaterials(avaliableMaterials);
            _gameController.addTurret("Turret 1");
            workshopText.text = "Basic turret built!";
            workshopText.color = Color.green;
            updateDropdownTurretList();
            updateTurretCountText();
        }
        else
        {
            workshopText.text = "Not enough materials!";
            workshopText.color = Color.red;
        }
 
    }

    private void getTurrets()
    {
        avaliableTurrets = _gameController.getTurrets();
    }

    IEnumerator clearTextDelay()
    {
        yield return new WaitForSecondsRealtime(5);
        workshopText.text = "";
    }

    public void InstantiateTurrets()
    {
        turret1Ref = turret1;
        turret2Ref = turret2;
        turret3Ref = turret3;
        turret6Ref = turret6;

        Vector3 turret1Vector = new Vector3(-260, 90, -400);
        Vector3 turret2Vector = new Vector3(-260, 35, -400);
        Vector3 turret3Vector = new Vector3(-260, -25, -400);
        Vector3 turret6Vector = new Vector3(-260, -90, -400);

        var panel = GameObject.Find("WorkshopPanel");
        if (panel != null) 
        {
            GameObject a = Instantiate(turret1Ref, turret1Vector, Quaternion.identity);
            a.transform.SetParent(panel.transform, false);
            a.transform.localScale = new Vector3(15, 15, 15);

            GameObject b = Instantiate(turret2Ref, turret2Vector, Quaternion.identity);
            b.transform.SetParent(panel.transform, false);
            b.transform.localScale = new Vector3(15, 15, 15);

            GameObject c = Instantiate(turret3Ref, turret3Vector, Quaternion.identity);
            c.transform.SetParent(panel.transform, false);
            c.transform.localScale = new Vector3(15, 15, 15);

            GameObject d = Instantiate(turret6Ref, turret6Vector, Quaternion.identity);
            d.transform.SetParent(panel.transform, false);
            d.transform.localScale = new Vector3(15, 15 ,15);

            turret1Ref.GetComponent<AnimatedExampleWeapon>().SetState(ExampleWeapon.State.Waiting);
            turret2Ref.GetComponent<AnimatedExampleWeapon>().SetState(ExampleWeapon.State.Waiting);
            turret3Ref.GetComponent<AnimatedExampleWeapon>().SetState(ExampleWeapon.State.Waiting);
            turret6Ref.GetComponent<AnimatedExampleWeapon>().SetState(ExampleWeapon.State.Waiting);

        }

    }

    public void DestroyTurrets()
    {
        Destroy(GameObject.FindWithTag("Turret1"));
        Destroy(GameObject.FindWithTag("Turret2"));
        Destroy(GameObject.FindWithTag("Turret3"));
        Destroy(GameObject.FindWithTag("Turret6"));
    }

    private void WorkshopBackButtonClicked()
    {
        DestroyTurrets();
        workshopCanvas.enabled = false;
        summaryCanvas.enabled = true;        
    }


    public void updateAvaliableMaterialsText()
    {
        avaliableMaterialsText.text = "Avaliable materials: "+avaliableMaterials;
    }

    public void setNumberOfAvaliableMaterials(int numbOfMaterials)
    {
        avaliableMaterials = numbOfMaterials;
    }

    public void decreaseAvaliableMaterials(int numbOfMaterials)
    {
        avaliableMaterials = avaliableMaterials -numbOfMaterials;
    }

    public void increaseAvaliableMaterials(int numbOfMaterials)
    {
        avaliableMaterials = avaliableMaterials + numbOfMaterials;
    }



    public void updateDropdownTurretList()
    {
        turret1Dropdown.ClearOptions();
        turret2Dropdown.ClearOptions();
        turret3Dropdown.ClearOptions();

        avaliableTurrets = _gameController.getTurrets();

        List<string> results = new List<string>();

        results.Add("None");

        foreach (KeyValuePair<string, int> entry in avaliableTurrets)
        {
            if(entry.Value>0)
            {
                if(entry.Key.Equals("Turret 1"))
                {
                    results.Add("Basic Turret");
                }
                if (entry.Key.Equals("Turret 2"))
                {
                    results.Add("Medium Turret");
                }
                if (entry.Key.Equals("Turret 3"))
                {
                    results.Add("Heavy Turret");
                }
                if (entry.Key.Equals("Turret 6"))
                {
                    results.Add("Super Turret");
                }

            }
        }
   
        turret1Dropdown.AddOptions(results);
        turret2Dropdown.AddOptions(results);
        turret3Dropdown.AddOptions(results);
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

   
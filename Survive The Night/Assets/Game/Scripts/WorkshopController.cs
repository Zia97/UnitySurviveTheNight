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

    private ArrayList _avaliableWeapons = new ArrayList();

    private Dictionary<string, int> avaliableTurrets = new Dictionary<string, int>();

    private string _primaryWeapon;
    private string _secondaryWeapon;

    private int avaliableMaterials;

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
    private object turret1Value;
    private object turret2Value;
    private object turret3Value;

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

        updateDropdownTurretList();
    }

    private void turret3DropdownValueChanged(object turret3Value)
    {
        Debug.Log("Turret 3 dropdown changed "+turret3Dropdown.options[turret3Dropdown.value].text);
    }

    private void turret2DropdownValueChanged(object turret2Value)
    {
        Debug.Log("Turret 2 dropdown changed");
    }

    private void turret1DropdownValueChanged(object turret1Value)
    {
        Debug.Log("Turret 1 dropdown changed");
    }

    private void BuildTurret6ButtonClicked()
    {
        if(avaliableMaterials>=1)
        {
            //avaliableMaterials = avaliableMaterials - 5000;
            _gameController.updateBuildingMaterials(avaliableMaterials);
            _gameController.addTurret("Turret 6");
            workshopText.text = "Super turret built!";
            workshopText.color = Color.green;
            updateDropdownTurretList();
        }
        else
        {
            workshopText.text = "Not enough materials!";
            workshopText.color = Color.red;
        }
        
    }

    private void BuildTurret3ButtonClicked()
    {
        if (avaliableMaterials >= 1)
        {
            //avaliableMaterials = avaliableMaterials - 3500;
            _gameController.updateBuildingMaterials(avaliableMaterials);
            _gameController.addTurret("Turret 3");
            workshopText.text = "Heavy turret built!";
            workshopText.color = Color.green;
            updateDropdownTurretList();
        }
        else
        {
            workshopText.text = "Not enough materials!";
            workshopText.color = Color.red;
        }
      
    }

    private void BuildTurret2ButtonClicked()
    {
        if (avaliableMaterials >= 1)
        {
            //avaliableMaterials = avaliableMaterials - 2000;
            _gameController.updateBuildingMaterials(avaliableMaterials);
            _gameController.addTurret("Turret 2");
            workshopText.text = "Medium turret built!";
            workshopText.color = Color.green;
            updateDropdownTurretList();
        }
        else
        {
            workshopText.text = "Not enough materials!";
            workshopText.color = Color.red;
        }
        
    }

    private void BuildTurret1ButtonClicked()
    {
        if (avaliableMaterials >= 1)
        {
            //avaliableMaterials = avaliableMaterials - 1000;
            _gameController.updateBuildingMaterials(avaliableMaterials);
            _gameController.addTurret("Turret 1");
            workshopText.text = "Basic turret built!";
            workshopText.color = Color.green;
            updateDropdownTurretList();
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

        Vector3 turret1Vector = new Vector3(-260, 80, -400);
        Vector3 turret2Vector = new Vector3(-260, -180, -400);
        Vector3 turret3Vector = new Vector3(-260, -90, -400);
        Vector3 turret6Vector = new Vector3(-260, 0, -400);

        var panel = GameObject.Find("WorkshopPanel");
        if (panel != null) 
        {
            GameObject a = Instantiate(turret1Ref, turret1Vector, Quaternion.identity);
            a.transform.SetParent(panel.transform, false);
            a.gameObject.transform.position.Set(-260, 80, a.gameObject.transform.position.z);

            GameObject b = Instantiate(turret2Ref, turret2Vector, Quaternion.identity);
            b.transform.SetParent(panel.transform, false);
            b.gameObject.transform.position.Set(-260, -180, b.gameObject.transform.position.z);

            GameObject c = Instantiate(turret3Ref, turret3Vector, Quaternion.identity);
            c.transform.SetParent(panel.transform, false);
            c.gameObject.transform.position.Set(-260, -90, c.gameObject.transform.position.z);

            GameObject d = Instantiate(turret6Ref, turret6Vector, Quaternion.identity);
            d.transform.SetParent(panel.transform, false);
            d.gameObject.transform.position.Set(-260, 0, d.gameObject.transform.position.z);

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

    public void addWeaponToAvaliableWeapons(string weapon)
    {
        _avaliableWeapons.Add(weapon);
        _gameController.updateAllAvaliableWeapons(_avaliableWeapons);
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

}

   
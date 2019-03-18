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

    private string _primaryWeapon;
    private string _secondaryWeapon;

    private int avaliableMaterials;

    public Dropdown turret1Dropdown;
    public Dropdown turret2Dropdown;
    public Dropdown turret3Dropdown;

    public Canvas workshopCanvas;
    public GameObject workshopObject;

    public Canvas summaryCanvas;

    public Text avaliableMaterialsText;

    private void Start()
    {
        _gameControllerObject = GameObject.FindWithTag("GameController");
        _workshopBackButtonObject = GameObject.FindWithTag("WorkshopBackButton");

        _workshopBackButton = _workshopBackButtonObject.GetComponent<Button>();
        _workshopBackButton.onClick.AddListener(WorkshopBackButtonClicked);

        if (_gameControllerObject != null)
        {
             _gameController = _gameControllerObject.GetComponent<GameController>();
            _avaliableWeapons = _gameController.getAllAvaliableWeapons();
        }

        workshopObject = GameObject.FindWithTag("WorkshopCanvas");
        workshopObject.SetActive(true);
        workshopCanvas = workshopObject.GetComponent<Canvas>();
    }

    public void InstantiateTurrets()
    {
        turret1Ref = turret1;
        turret2Ref = turret2;
        turret3Ref = turret3;
        turret6Ref = turret6;

        Vector3 turret1Vector = new Vector3(-260, 80, -4647);
        Vector3 turret2Vector = new Vector3(-260, -180, -4647);
        Vector3 turret3Vector = new Vector3(-260, -90, -4647);
        Vector3 turret6Vector = new Vector3(-260, 0, -4647);

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



    //public void updateDropdownWeaponList()
    //{
    //    _avaliableWeapons = _gameController.getAllAvaliableWeapons();
    //    List<string> results = _avaliableWeapons.Cast<string>().Distinct().ToList();
    //    primaryWeaponDropdown.ClearOptions();
    //    secondaryWeaponDropdown.ClearOptions();
    //    primaryWeaponDropdown.AddOptions(results);
    //    secondaryWeaponDropdown.AddOptions(results);
    //}

}

   
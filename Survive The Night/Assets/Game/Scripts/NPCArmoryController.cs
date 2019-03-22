using Assets.Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NPCArmoryController : MonoBehaviour
{
    public GameObject _gameControllerObject;

    public GameObject _armoryBackButtonObject;
    public Button _armoryBackButton;

    private GameController _gameController;

    public GameObject S1USP;
    public GameObject S1MP5;
    public GameObject S1Shotgun;
    public GameObject S1Scout;

    public GameObject S2USP;
    public GameObject S2MP5;
    public GameObject S2Shotgun;
    public GameObject S2Scout;

    public GameObject S3USP;
    public GameObject S3MP5;
    public GameObject S3Shotgun;
    public GameObject S3Scout;

    private Dictionary<string, int> _avaliableWeapons = new Dictionary<string, int>();

    private string _primaryWeapon;
    private string _secondaryWeapon;

    public Dropdown NPC1Dropdown;
    public Dropdown NPC2Dropdown;
    public Dropdown NPC3Dropdown;

    private int turret1Count = 0;
    private int turret2Count = 0;
    private int turret3Count = 0;
    private int turret6Count = 0;


    public Canvas armoryCanvas;
    public GameObject armoryObject;

    public Canvas summaryCanvas;

    public Text armoryHelperText;
    private object turret1Value;
    private object turret2Value;
    private object turret3Value;
    private object npc1Value;
    private object npc2Value;
    private object npc3Value;

    private void Start()
    {
        _gameControllerObject = GameObject.FindWithTag("GameController");

        _armoryBackButtonObject = GameObject.FindWithTag("ArmoryBackButton");
        _armoryBackButton = _armoryBackButtonObject.GetComponent<Button>();
        _armoryBackButton.onClick.AddListener(ArmoryBackButtonClicked);

        

        if (_gameControllerObject != null)
        {
             _gameController = _gameControllerObject.GetComponent<GameController>();
            _avaliableWeapons = _gameController.getAllAvaliableWeapons();
        }

        _avaliableWeapons = _gameController.getAllAvaliableWeapons();

        List<string> results = _avaliableWeapons.Keys.Cast<string>().Distinct().ToList();
        NPC1Dropdown.ClearOptions();
        NPC2Dropdown.ClearOptions();
        NPC3Dropdown.ClearOptions();
        NPC1Dropdown.AddOptions(results);
        NPC2Dropdown.AddOptions(results);
        NPC3Dropdown.AddOptions(results);

        NPC1Dropdown.onValueChanged.AddListener(delegate {
            npc1DropdownValueChanged(npc1Value);
        });

        NPC2Dropdown.onValueChanged.AddListener(delegate {
            npc2DropdownValueChanged(npc2Value);
        });

        NPC3Dropdown.onValueChanged.AddListener(delegate {
            npc3DropdownValueChanged(npc3Value);
        });
    }

    private void npc1DropdownValueChanged(object npc1Value)
    {
        int turret1temp = 0;
        int turret2temp = 0;
        int turret3temp = 0;
        int turret6temp = 0;

        var tempList = _gameController.getAllAvaliableWeapons();
        var selectedPrimary = _gameController.getPrimary();
        var selectedSecondary = _gameController.getSecondary();

        if (tempList.ContainsKey(selectedPrimary))
        {
            int old = tempList[selectedPrimary];
            tempList[selectedPrimary] = old - 1;
        }

        if (tempList.ContainsKey(selectedSecondary))
        {
            int old = tempList[selectedPrimary];
            tempList[selectedPrimary] = old - 1;
        }
    }

    private void npc2DropdownValueChanged(object npc2Value)
    {
        throw new NotImplementedException();
    }

    private void npc3DropdownValueChanged(object npc3Value)
    {
        throw new NotImplementedException();
    }

    private void ArmoryBackButtonClicked()
    {
        armoryCanvas.enabled = false;
        summaryCanvas.enabled = true;     
    }

    public void InstantiateTurrets()
    {
       // turret1Ref = turret1;
       // turret2Ref = turret2;
       // turret3Ref = turret3;
       //// turret6Ref = turret6;

       // Vector3 turret1Vector = new Vector3(-260, 90, -400);
       // Vector3 turret2Vector = new Vector3(-260, 0, -400);
       // Vector3 turret3Vector = new Vector3(-260, -90, -400);
       // Vector3 turret6Vector = new Vector3(-260, -180, -400);

       // var panel = GameObject.Find("WorkshopPanel");
       // if (panel != null) 
       // {
       //     GameObject a = Instantiate(turret1Ref, turret1Vector, Quaternion.identity);
       //     a.transform.SetParent(panel.transform, false);

       //     GameObject b = Instantiate(turret2Ref, turret2Vector, Quaternion.identity);
       //     b.transform.SetParent(panel.transform, false);

       //     GameObject c = Instantiate(turret3Ref, turret3Vector, Quaternion.identity);
       //     c.transform.SetParent(panel.transform, false);

       //    // GameObject d = Instantiate(turret6Ref, turret6Vector, Quaternion.identity);
       //     //d.transform.SetParent(panel.transform, false);

       //     turret1Ref.GetComponent<AnimatedExampleWeapon>().SetState(ExampleWeapon.State.Waiting);
       //     turret2Ref.GetComponent<AnimatedExampleWeapon>().SetState(ExampleWeapon.State.Waiting);
       //     turret3Ref.GetComponent<AnimatedExampleWeapon>().SetState(ExampleWeapon.State.Waiting);
       //     //turret6Ref.GetComponent<AnimatedExampleWeapon>().SetState(ExampleWeapon.State.Waiting);

       // }

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
        ////workshopCanvas.enabled = false;
        //summaryCanvas.enabled = true;        
    }


    public void updateAvaliableMaterialsText()
    {
       // avaliableMaterialsText.text = "Avaliable materials: "+avaliableMaterials;
    }


    public void updateWeaponsDropdown()
    {
        List<string> results = _avaliableWeapons.Keys.Cast<string>().Distinct().ToList();
        NPC1Dropdown.ClearOptions();
        NPC2Dropdown.ClearOptions();
        NPC3Dropdown.ClearOptions();
        NPC1Dropdown.AddOptions(results);
        NPC2Dropdown.AddOptions(results);
        NPC3Dropdown.AddOptions(results);
    }

    public void updateLayout()
    {
        var noOfNpcs = _gameController.getNoOfNPCS();
        if(noOfNpcs==1)
        {
            NPC2Dropdown.Hide();
            NPC3Dropdown.Hide();
            NPC2Dropdown.enabled = false;
            NPC3Dropdown.enabled = false;
        }
        if (noOfNpcs == 2)
        {
            NPC2Dropdown.Show();
            NPC3Dropdown.Hide();
            NPC2Dropdown.enabled = true;
            NPC3Dropdown.enabled = false;
        }
        if(noOfNpcs>=3)
        {
            NPC3Dropdown.Show();
            NPC1Dropdown.enabled = true;
            NPC2Dropdown.enabled = true;
            NPC3Dropdown.enabled = true;
        }
    }

}

   
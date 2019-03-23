using Assets.Game.Scripts;
using Assets.HeroEditor.Common.CharacterScripts;
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

    public Canvas armoryCanvas;
    public GameObject armoryObject;

    public Canvas summaryCanvas;

    public Text armoryHelperText;
 
    public GameObject S1Cross;
    public GameObject S2Cross;
    public GameObject S3Cross;

    private object npc1Value;
    private object npc2Value;
    private object npc3Value;

    private void Start()
    {
        _gameControllerObject = GameObject.FindWithTag("GameController");

        _armoryBackButtonObject = GameObject.FindWithTag("ArmoryBackButton");
        _armoryBackButton = _armoryBackButtonObject.GetComponent<Button>();
        _armoryBackButton.onClick.AddListener(ArmoryBackButtonClicked);

        S1Cross = GameObject.FindWithTag("S1Cross");
        S2Cross = GameObject.FindWithTag("S2Cross");
        S3Cross = GameObject.FindWithTag("S3Cross");




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

        if (_gameController.getNoOfNPCS() == 1)
        {
            _gameController.selectNPCs("S1" + NPC1Dropdown.options[NPC1Dropdown.value].text,null,null);
        }
        else if (_gameController.getNoOfNPCS() == 2)
        {
            _gameController.selectNPCs("S1" + NPC1Dropdown.options[NPC1Dropdown.value].text, "S2" + NPC2Dropdown.options[NPC2Dropdown.value].text,null);
        }
        else if (_gameController.getNoOfNPCS() == 3)
        {
            _gameController.selectNPCs("S1" + NPC1Dropdown.options[NPC1Dropdown.value].text, "S2" + NPC2Dropdown.options[NPC2Dropdown.value].text, "S3" + NPC3Dropdown.options[NPC3Dropdown.value].text);
        }
        else
        {
      
        }

        DestroyNPCS();
        S1Cross.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        S2Cross.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        S3Cross.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        summaryCanvas.enabled = true;     
    }

    public void InstantiateNPCS()
    {
        Vector3 pos1 = new Vector3(-4f, 0, 0);
        Vector3 pos2 = new Vector3(0, 0, 0);
        Vector3 pos3 = new Vector3(4, 0, 0);


        var panel = GameObject.Find("NPCPanel");
        if (panel != null)
        {
            if (_gameController.getNoOfNPCS() == 1)
            {
                GameObject a = Instantiate(S1USP, pos1, Quaternion.identity);
                a.GetComponent<WeaponControls>().isNPC();
                a.GetComponent<WeaponControls>().setLocation("Armory");
                a.transform.SetParent(panel.transform, true);
            }

            else if(_gameController.getNoOfNPCS() == 2)
            {
                GameObject a = Instantiate(S1USP, pos1, Quaternion.identity);
                a.GetComponent<WeaponControls>().isNPC();
                a.GetComponent<WeaponControls>().setLocation("Armory");
                a.transform.SetParent(panel.transform, true);

                GameObject b = Instantiate(S2USP, pos2, Quaternion.identity);
                b.GetComponent<WeaponControls>().isNPC();
                b.GetComponent<WeaponControls>().setLocation("Armory");
                b.transform.SetParent(panel.transform, true);
            }

            else if (_gameController.getNoOfNPCS() == 3)
            {
                GameObject a = Instantiate(S1USP, pos1, Quaternion.identity);
                a.GetComponent<WeaponControls>().isNPC();
                a.GetComponent<WeaponControls>().setLocation("Armory");
                a.transform.SetParent(panel.transform, true);

                GameObject b = Instantiate(S2USP, pos2, Quaternion.identity);
                b.GetComponent<WeaponControls>().isNPC();
                b.GetComponent<WeaponControls>().setLocation("Armory");
                b.transform.SetParent(panel.transform, true);

                GameObject c = Instantiate(S3USP, pos3, Quaternion.identity);
                c.GetComponent<WeaponControls>().isNPC();
                c.GetComponent<WeaponControls>().setLocation("Armory");
                c.transform.SetParent(panel.transform, true);
            }

        }

    }

    public void DestroyNPCS()
    {
        var allNPCS = GameObject.FindGameObjectsWithTag("NPC");

        for (var i = 0; i < allNPCS.Length; i++)
        {
            Destroy(allNPCS[i]);
        }
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
        if(noOfNpcs==0)
        {
            S1Cross.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            S2Cross.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            S3Cross.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if(noOfNpcs==1)
        {
            Debug.Log("hit here");
            S1Cross.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            S2Cross.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            S3Cross.gameObject.GetComponent<SpriteRenderer>().enabled = true;

            NPC2Dropdown.Hide();
            NPC3Dropdown.Hide();
            NPC2Dropdown.enabled = false;
            NPC3Dropdown.enabled = false;
        }
        if (noOfNpcs == 2)
        {
            S2Cross.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            S3Cross.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            NPC2Dropdown.Show();
            NPC3Dropdown.Hide();
            NPC2Dropdown.enabled = true;
            NPC3Dropdown.enabled = false;
        }
        if(noOfNpcs>=3)
        {
            S2Cross.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            NPC3Dropdown.Show();
            NPC1Dropdown.enabled = true;
            NPC2Dropdown.enabled = true;
            NPC3Dropdown.enabled = true;
        }
    }

}

   
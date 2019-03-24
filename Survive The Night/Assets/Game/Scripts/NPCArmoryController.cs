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

    private Vector3 pos1 = new Vector3(-4f, 0, 0);
    private Vector3 pos2 = new Vector3(0, 0, 0);
    private Vector3 pos3 = new Vector3(4, 0, 0);

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

        destroyCharcter("S1");
        updateNPC("S1", NPC1Dropdown.options[NPC1Dropdown.value].text, pos1);

        SetWeapons();

       

        //if (tempList.ContainsKey(selectedPrimary))
        //{
        //    int old = tempList[selectedPrimary];
        //    tempList[selectedPrimary] = old - 1;
        //}

        //if (tempList.ContainsKey(selectedSecondary))
        //{
        //    int old = tempList[selectedPrimary];
        //    tempList[selectedPrimary] = old - 1;
        //}

        //if(tempList[NPC1Dropdown.options[NPC1Dropdown.value].text] <0)
        //{
        //    armoryHelperText.text = "You do not have enough " + NPC1Dropdown.options[NPC1Dropdown.value].text + "'s";
        //    Debug.Log(NPC1Dropdown.value);
        //}
    }

    private void SetWeapons()
    {
        if (_gameController.getNoOfNPCS() == 1)
        {
            _gameController.selectNPCs("S1" + NPC1Dropdown.options[NPC1Dropdown.value].text, null, null);
        }
        else if (_gameController.getNoOfNPCS() == 2)
        {
            _gameController.selectNPCs("S1" + NPC1Dropdown.options[NPC1Dropdown.value].text, "S2" + NPC2Dropdown.options[NPC2Dropdown.value].text, null);
        }
        else if (_gameController.getNoOfNPCS() >= 3)
        {
            _gameController.selectNPCs("S1" + NPC1Dropdown.options[NPC1Dropdown.value].text, "S2" + NPC2Dropdown.options[NPC2Dropdown.value].text, "S3" + NPC3Dropdown.options[NPC3Dropdown.value].text);
        }
    }

    private void npc2DropdownValueChanged(object npc2Value)
    {
        var tempList = _gameController.getAllAvaliableWeapons();
        var selectedPrimary = _gameController.getPrimary();
        var selectedSecondary = _gameController.getSecondary();

        destroyCharcter("S2");
        updateNPC("S2",NPC2Dropdown.options[NPC2Dropdown.value].text, pos2);

        SetWeapons();

        //if (tempList.ContainsKey(selectedPrimary))
        //{
        //    int old = tempList[selectedPrimary];
        //    tempList[selectedPrimary] = old - 1;
        //}

        //if (tempList.ContainsKey(selectedSecondary))
        //{
        //    int old = tempList[selectedPrimary];
        //    tempList[selectedPrimary] = old - 1;
        //}

        //if (tempList[NPC2Dropdown.options[NPC2Dropdown.value].text] < 0)
        //{
        //    armoryHelperText.text = "You do not have enough " + NPC2Dropdown.options[NPC2Dropdown.value].text + "'s";
        //    Debug.Log(NPC2Dropdown.value);
        //}
    }

    private void npc3DropdownValueChanged(object npc3Value)
    {
        var tempList = _gameController.getAllAvaliableWeapons();
        var selectedPrimary = _gameController.getPrimary();
        var selectedSecondary = _gameController.getSecondary();

        destroyCharcter("S3");
        updateNPC("S3",NPC3Dropdown.options[NPC3Dropdown.value].text, pos3);

        SetWeapons();

        //if (tempList.ContainsKey(selectedPrimary))
        //{
        //    int old = tempList[selectedPrimary];
        //    tempList[selectedPrimary] = old - 1;
        //}

        //if (tempList.ContainsKey(selectedSecondary))
        //{
        //    int old = tempList[selectedPrimary];
        //    tempList[selectedPrimary] = old - 1;
        //}

        //if (tempList[NPC3Dropdown.options[NPC3Dropdown.value].text] < 0)
        //{
        //    armoryHelperText.text = "You do not have enough " + NPC3Dropdown.options[NPC3Dropdown.value].text + "'s";
        //    Debug.Log(NPC3Dropdown.value);
        //}
    }

    private void ArmoryBackButtonClicked()
    {
        armoryCanvas.enabled = false;

        SetWeapons();

        DestroyNPCS();
        S1Cross.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        S2Cross.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        S3Cross.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        summaryCanvas.enabled = true;     
    }

    private void updateNPC(string charac, string weapon, Vector3 pos)
    {
        var panel = GameObject.Find("NPCPanel");
        if (panel != null)
        {
            GameObject a = Instantiate(npcToPrefab(charac,weapon), pos, Quaternion.identity);
            a.GetComponent<WeaponControls>().isNPC();
            a.GetComponent<WeaponControls>().setLocation("Armory");
            a.transform.SetParent(panel.transform, true);
        }
    }

    public void InstantiateNPCS()
    {
        var panel = GameObject.Find("NPCPanel");
        if (panel != null)
        {
            if(_gameController.getNoOfNPCS() == 0)
            {

            }
            else if (_gameController.getNoOfNPCS() == 1)
            {
                GameObject a = Instantiate(npcToPrefab("S1", NPC1Dropdown.options[NPC1Dropdown.value].text), pos1, Quaternion.identity);
                a.GetComponent<WeaponControls>().isNPC();
                a.GetComponent<WeaponControls>().setLocation("Armory");
                a.transform.SetParent(panel.transform, true);
            }

            else if(_gameController.getNoOfNPCS() == 2)
            {
                GameObject a = Instantiate(npcToPrefab("S1", NPC1Dropdown.options[NPC1Dropdown.value].text), pos1, Quaternion.identity);
                a.GetComponent<WeaponControls>().isNPC();
                a.GetComponent<WeaponControls>().setLocation("Armory");
                a.transform.SetParent(panel.transform, true);

                GameObject b = Instantiate(npcToPrefab("S2", NPC2Dropdown.options[NPC2Dropdown.value].text), pos2, Quaternion.identity);
                b.GetComponent<WeaponControls>().isNPC();
                b.GetComponent<WeaponControls>().setLocation("Armory");
                b.transform.SetParent(panel.transform, true);
            }

            else if (_gameController.getNoOfNPCS() >= 3)
            {
                GameObject a = Instantiate(npcToPrefab("S1", NPC1Dropdown.options[NPC1Dropdown.value].text), pos1, Quaternion.identity);
                a.GetComponent<WeaponControls>().isNPC();
                a.GetComponent<WeaponControls>().setLocation("Armory");
                a.transform.SetParent(panel.transform, true);

                GameObject b = Instantiate(npcToPrefab("S2", NPC2Dropdown.options[NPC2Dropdown.value].text), pos2, Quaternion.identity);
                b.GetComponent<WeaponControls>().isNPC();
                b.GetComponent<WeaponControls>().setLocation("Armory");
                b.transform.SetParent(panel.transform, true);

                GameObject c = Instantiate(npcToPrefab("S3", NPC2Dropdown.options[NPC3Dropdown.value].text), pos3, Quaternion.identity);
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

    private void destroyCharcter(string survivorNumber)
    {
        var allNPCS = GameObject.FindGameObjectsWithTag("NPC");
        
        foreach(var selectedcharacter in allNPCS)
        {
            if(survivorNumber.Equals("S1"))
            {
                if(selectedcharacter.gameObject.name.Equals("S1USP(Clone)") || selectedcharacter.gameObject.name.Equals("S1MP5(Clone)") || selectedcharacter.gameObject.name.Equals("S1Scout(Clone)") || selectedcharacter.gameObject.name.Equals("S1Shotgun(Clone)"))
                {
                    Destroy(selectedcharacter);
                }

            }
            else if (survivorNumber.Equals("S2"))
            {
                if (selectedcharacter.gameObject.name.Equals("S2USP(Clone)") || selectedcharacter.gameObject.name.Equals("S2MP5(Clone)") || selectedcharacter.gameObject.name.Equals("S2Scout(Clone)") || selectedcharacter.gameObject.name.Equals("S2Shotgun(Clone)"))
                {
                    Destroy(selectedcharacter);
                }

            }
            else if (survivorNumber.Equals("S3"))
            {
                if (selectedcharacter.gameObject.name.Equals("S3USP(Clone)") || selectedcharacter.gameObject.name.Equals("S3MP5(Clone)") || selectedcharacter.gameObject.name.Equals("S3Scout(Clone)") || selectedcharacter.gameObject.name.Equals("S3Shotgun(Clone)"))
                {
                    Destroy(selectedcharacter);
                }

            }
        }

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
            armoryHelperText.text = "Arm your survivors...";
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
            armoryHelperText.text = "Arm your survivors...";
            S2Cross.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            S3Cross.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            NPC2Dropdown.Show();
            NPC3Dropdown.Hide();
            NPC2Dropdown.enabled = true;
            NPC3Dropdown.enabled = false;
        }
        if(noOfNpcs>=3)
        {
            armoryHelperText.text = "Arm your survivors...";
            S2Cross.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            NPC3Dropdown.Show();
            NPC1Dropdown.enabled = true;
            NPC2Dropdown.enabled = true;
            NPC3Dropdown.enabled = true;
        }
    }

    public GameObject npcToPrefab(string charac, string npc)
    {
        if (npc == null)
        {
            return null;
        }

        if(charac.Equals("S1"))
        {
            if (npc.Equals("USP"))
            {
                return S1USP;
            }
            if (npc.Equals("MP-5"))
            {
                return S1MP5;
            }
            if (npc.Equals("Scout"))
            {
                return S1Scout;
            }
            if (npc.Equals("Shotgun"))
            {
                return S1Shotgun;
            }
        }

        else if (charac.Equals("S2"))
        {
            if (npc.Equals("USP"))
            {
                return S2USP;
            }
            if (npc.Equals("MP-5"))
            {
                return S2MP5;
            }
            if (npc.Equals("Scout"))
            {
                return S2Scout;
            }
            if (npc.Equals("Shotgun"))
            {
                return S2Shotgun;
            }
        }

        else if (charac.Equals("S3"))
        {
            if (npc.Equals("USP"))
            {
                return S3USP;
            }
            if (npc.Equals("MP-5"))
            {
                return S3MP5;
            }
            if (npc.Equals("Scout"))
            {
                return S3Scout;
            }
            if (npc.Equals("Shotgun"))
            {
                return S3Shotgun;
            }
        }

        return null;
    }

}

   
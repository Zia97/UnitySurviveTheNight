﻿//Author : Qasim Ziauddin

//Controller handling the NPC Armoury. Gives a visual display of suvivors that have been found. Allows user to switch their weapons.
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
    public GameObject S1Revolver;

    public GameObject S2USP;
    public GameObject S2MP5;
    public GameObject S2Shotgun;
    public GameObject S2Scout;
    public GameObject S2Revolver;

    public GameObject S3USP;
    public GameObject S3MP5;
    public GameObject S3Shotgun;
    public GameObject S3Scout;
    public GameObject S3Revolver;

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

    public GameObject npcInfoButtonObject;
    private Button _infoButton;

    public GameObject _resumeButtonObject;
    private Button _resumeButton;

    public GameObject BlurCanvasObject;
    Canvas BlurCanvas;

    public GameObject npcCanvasObject;
    Canvas PausedCanvas;

    private static List<string> allowedWeapons = new List<string>();


    private void Start()
    {
        _gameControllerObject = GameObject.FindWithTag("GameController");

        _armoryBackButtonObject = GameObject.FindWithTag("ArmoryBackButton");
        _armoryBackButton = _armoryBackButtonObject.GetComponent<Button>();
        _armoryBackButton.onClick.AddListener(ArmoryBackButtonClicked);

        BlurCanvasObject = GameObject.Find("BlurCanvas");
        BlurCanvas = BlurCanvasObject.GetComponent<Canvas>();
        BlurCanvas.enabled = false;

        npcCanvasObject = GameObject.Find("npcInfo");
        PausedCanvas = npcCanvasObject.GetComponent<Canvas>();
        PausedCanvas.enabled = false;

        _resumeButton = _resumeButtonObject.GetComponent<Button>();
        _resumeButton.onClick.AddListener(ResumeButtonClicked);

        npcInfoButtonObject = GameObject.Find("npcInfoButton");
        _infoButton = npcInfoButtonObject.GetComponent<Button>();
        _infoButton.onClick.AddListener(infoButtonClicked);

        allowedWeapons.Add("USP");
        allowedWeapons.Add("Revolver");
        allowedWeapons.Add("Scout");
        allowedWeapons.Add("MP-5");
        allowedWeapons.Add("Shotgun");

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

        List<string> updatedWeaponsList = new List<string>();

        foreach (String weapon in results)
        {
            if (allowedWeapons.Contains(weapon))
            {
                updatedWeaponsList.Add(weapon);
            }
        }

        NPC1Dropdown.ClearOptions();
        NPC2Dropdown.ClearOptions();
        NPC3Dropdown.ClearOptions();
        NPC1Dropdown.AddOptions(updatedWeaponsList);
        NPC2Dropdown.AddOptions(updatedWeaponsList);
        NPC3Dropdown.AddOptions(updatedWeaponsList);

        NPC1Dropdown.onValueChanged.AddListener(delegate
        {
            npc1DropdownValueChanged(npc1Value);
        });

        NPC2Dropdown.onValueChanged.AddListener(delegate
        {
            npc2DropdownValueChanged(npc2Value);
        });

        NPC3Dropdown.onValueChanged.AddListener(delegate
        {
            npc3DropdownValueChanged(npc3Value);
        });
    }

    private void npc1DropdownValueChanged(object npc1Value)
    {
        var tempList = _gameController.getAllAvaliableWeapons();
        var selectedPrimary = _gameController.getPrimary();
        var selectedSecondary = _gameController.getSecondary();

        if (_gameController.getNoOfNPCS() == 1)
        {
            destroyCharcter("S1");
            updateNPC("S1", NPC1Dropdown.options[NPC1Dropdown.value].text, pos1);
        }

        SetWeapons();
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

        if (_gameController.getNoOfNPCS() == 2)
        {
            destroyCharcter("S2");
            updateNPC("S2", NPC2Dropdown.options[NPC2Dropdown.value].text, pos2);
        }

        SetWeapons();
    }

    private void npc3DropdownValueChanged(object npc3Value)
    {
        var tempList = _gameController.getAllAvaliableWeapons();
        var selectedPrimary = _gameController.getPrimary();
        var selectedSecondary = _gameController.getSecondary();

        if (_gameController.getNoOfNPCS() == 3)
        {
            destroyCharcter("S3");
            updateNPC("S3", NPC3Dropdown.options[NPC3Dropdown.value].text, pos3);
        }

        SetWeapons();
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
            GameObject a = Instantiate(npcToPrefab(charac, weapon), pos, Quaternion.identity);
            a.GetComponent<WeaponControls>().isNPC();
            a.GetComponent<WeaponControls>().setLocation("Armory");
            a.transform.SetParent(panel.transform, true);
            a.transform.localScale = new Vector3(28, 28, 28);
        }
    }

    public void InstantiateNPCS()
    {
        var panel = GameObject.Find("NPCPanel");
        if (panel != null)
        {
            if (_gameController.getNoOfNPCS() == 0)
            {

            }
            else if (_gameController.getNoOfNPCS() == 1)
            {

                GameObject a = Instantiate(npcToPrefab("S1", "USP"), pos1, Quaternion.identity);
                a.GetComponent<WeaponControls>().isNPC();
                a.GetComponent<WeaponControls>().setLocation("Armory");
                a.transform.SetParent(panel.transform, true);
                a.transform.localScale = new Vector3(28, 28, 28);
            }

            else if (_gameController.getNoOfNPCS() == 2)
            {
                GameObject a = Instantiate(npcToPrefab("S1", "USP"), pos1, Quaternion.identity);
                a.GetComponent<WeaponControls>().isNPC();
                a.GetComponent<WeaponControls>().setLocation("Armory");
                a.transform.SetParent(panel.transform, true);
                a.transform.localScale = new Vector3(28, 28, 28);

                GameObject b = Instantiate(npcToPrefab("S2", "USP"), pos2, Quaternion.identity);
                b.GetComponent<WeaponControls>().isNPC();
                b.GetComponent<WeaponControls>().setLocation("Armory");
                b.transform.SetParent(panel.transform, true);
                b.transform.localScale = new Vector3(28, 28, 28);
            }

            else if (_gameController.getNoOfNPCS() >= 3)
            {
                GameObject a = Instantiate(npcToPrefab("S1", "USP"), pos1, Quaternion.identity);
                a.GetComponent<WeaponControls>().isNPC();
                a.GetComponent<WeaponControls>().setLocation("Armory");
                a.transform.SetParent(panel.transform, true);
                a.transform.localScale = new Vector3(28, 28, 28);

                GameObject b = Instantiate(npcToPrefab("S2", "USP"), pos2, Quaternion.identity);
                b.GetComponent<WeaponControls>().isNPC();
                b.GetComponent<WeaponControls>().setLocation("Armory");
                b.transform.SetParent(panel.transform, true);
                b.transform.localScale = new Vector3(28, 28, 28);

                GameObject c = Instantiate(npcToPrefab("S3", "USP"), pos3, Quaternion.identity);
                c.GetComponent<WeaponControls>().isNPC();
                c.GetComponent<WeaponControls>().setLocation("Armory");
                c.transform.SetParent(panel.transform, true);
                c.transform.localScale = new Vector3(28, 28, 28);

                //GameObject c = Instantiate(npcToPrefab("S3", NPC3Dropdown.options[NPC3Dropdown.value].text), pos3, Quaternion.identity);
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
        //avaliableMaterialsText.text = "Avaliable materials: "+avaliableMaterials;
    }

    private void destroyCharcter(string survivorNumber)
    {
        var allNPCS = GameObject.FindGameObjectsWithTag("NPC");

        foreach (var selectedcharacter in allNPCS)
        {
            if (survivorNumber.Equals("S1"))
            {
                if (selectedcharacter.gameObject.name.Equals("S1USP(Clone)") || selectedcharacter.gameObject.name.Equals("S1MP5(Clone)") || selectedcharacter.gameObject.name.Equals("S1Scout(Clone)") || selectedcharacter.gameObject.name.Equals("S1Shotgun(Clone)") || selectedcharacter.gameObject.name.Equals("S1Revolver(Clone)"))
                {
                    Destroy(selectedcharacter);
                }

            }
            else if (survivorNumber.Equals("S2"))
            {
                if (selectedcharacter.gameObject.name.Equals("S2USP(Clone)") || selectedcharacter.gameObject.name.Equals("S2MP5(Clone)") || selectedcharacter.gameObject.name.Equals("S2Scout(Clone)") || selectedcharacter.gameObject.name.Equals("S2Shotgun(Clone)") || selectedcharacter.gameObject.name.Equals("S2Revolver(Clone)"))
                {
                    Destroy(selectedcharacter);
                }

            }
            else if (survivorNumber.Equals("S3"))
            {
                if (selectedcharacter.gameObject.name.Equals("S3USP(Clone)") || selectedcharacter.gameObject.name.Equals("S3MP5(Clone)") || selectedcharacter.gameObject.name.Equals("S3Scout(Clone)") || selectedcharacter.gameObject.name.Equals("S3Shotgun(Clone)") || selectedcharacter.gameObject.name.Equals("S3Revolver(Clone)"))
                {
                    Destroy(selectedcharacter);
                }

            }
        }

    }


    public void updateWeaponsDropdown()
    {
        List<string> results = _avaliableWeapons.Keys.Cast<string>().Distinct().ToList();
        List<string> updatedWeaponsList = new List<string>();

        foreach (String weapon in results)
        {
            if (allowedWeapons.Contains(weapon))
            {
                updatedWeaponsList.Add(weapon);
            }
        }

        NPC1Dropdown.ClearOptions();
        NPC2Dropdown.ClearOptions();
        NPC3Dropdown.ClearOptions();
        NPC1Dropdown.AddOptions(updatedWeaponsList);
        NPC2Dropdown.AddOptions(updatedWeaponsList);
        NPC3Dropdown.AddOptions(updatedWeaponsList);
    }

    public void updateLayout()
    {
        var noOfNpcs = _gameController.getNoOfNPCS();
        if (noOfNpcs == 0)
        {
            S1Cross.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            S2Cross.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            S3Cross.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (noOfNpcs == 1)
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
        if (noOfNpcs >= 3)
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

        if (charac.Equals("S1"))
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
            if (npc.Equals("Revolver"))
            {
                return S1Revolver;
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
            if (npc.Equals("Revolver"))
            {
                return S2Revolver;
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
            if (npc.Equals("Revolver"))
            {
                return S3Revolver;
            }
        }

        return null;
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

   
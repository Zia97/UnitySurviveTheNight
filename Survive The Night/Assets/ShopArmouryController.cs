using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopArmouryController : MonoBehaviour
{
    private List<string> _avaliableWeapons = new List<string>();
    public Dropdown primaryWeaponDropdown;
    public Dropdown secondaryWeaponDropdown;

    private object primaryWeaponDropdownValue;
    private object secondaryWeaponDropdownValue;
    private string _primaryWeapon;
    private string _secondaryWeapon;

    // Start is called before the first frame update
    void Start()
    {
        checkOwnedWeapons();

        primaryWeaponDropdown.onValueChanged.AddListener(delegate {
            primaryWeaponDropdownChanged(primaryWeaponDropdownValue);
        });

        secondaryWeaponDropdown.onValueChanged.AddListener(delegate {
            secondaryWeaponDropdownChanged(secondaryWeaponDropdownValue);
        });

        primaryWeaponDropdown.ClearOptions();
        secondaryWeaponDropdown.ClearOptions();
        primaryWeaponDropdown.AddOptions(_avaliableWeapons);
        secondaryWeaponDropdown.AddOptions(_avaliableWeapons);

    }

    private void secondaryWeaponDropdownChanged(object secondaryWeaponDropdownValue)
    {
        _primaryWeapon = primaryWeaponDropdown.options[primaryWeaponDropdown.value].text;
        _secondaryWeapon = secondaryWeaponDropdown.options[secondaryWeaponDropdown.value].text;
        UserProfile.setPrimaryWeapon(_primaryWeapon);
        UserProfile.setSecondaryWeapon(_secondaryWeapon);
    }

    private void primaryWeaponDropdownChanged(object primaryWeaponDropdownValue)
    {
        _primaryWeapon = primaryWeaponDropdown.options[primaryWeaponDropdown.value].text;
        _secondaryWeapon = secondaryWeaponDropdown.options[secondaryWeaponDropdown.value].text;
        UserProfile.setPrimaryWeapon(_primaryWeapon);
        UserProfile.setSecondaryWeapon(_secondaryWeapon);
    }

    void checkOwnedWeapons()
    {
        if (PlayerPrefs.HasKey("MP5"))
        {
            _avaliableWeapons.Add("MP-5");
        }
        if (PlayerPrefs.HasKey("Scout"))
        {
            _avaliableWeapons.Add("Scout");
        }
        if (PlayerPrefs.HasKey("Shotgun"))
        {
            _avaliableWeapons.Add("Shotgun");
        }
        if (PlayerPrefs.HasKey("GoldenAK"))
        {
            _avaliableWeapons.Add("AK-47 [Golden]");
        }
        if (PlayerPrefs.HasKey("M249"))
        {
            _avaliableWeapons.Add("M-249");
        }
        if (PlayerPrefs.HasKey("M4Laser"))
        {
            _avaliableWeapons.Add("M-4Laser");
        }
        if (PlayerPrefs.HasKey("RocketLauncher"))
        {
            _avaliableWeapons.Add("RocketLauncher");
        }
        if (PlayerPrefs.HasKey("RPG"))
        {
            _avaliableWeapons.Add("RPG");
        }
        if (PlayerPrefs.HasKey("SPAS12"))
        {
            _avaliableWeapons.Add("SPAS-12");
        }
        if (PlayerPrefs.HasKey("SRL"))
        {
            _avaliableWeapons.Add("SRL");
        }
        if (PlayerPrefs.HasKey("Revolver"))
        {
            _avaliableWeapons.Add("Revolver");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

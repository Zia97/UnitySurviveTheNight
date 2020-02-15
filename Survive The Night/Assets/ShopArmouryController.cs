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
    public Button ShopArmouryBackButton;

    private object primaryWeaponDropdownValue;
    private object secondaryWeaponDropdownValue;
    private string _primaryWeapon;
    private string _secondaryWeapon;

    public GameObject BasicPistolPlayer;
    public GameObject BasicMP5Player;
    public GameObject ShotgunPlayer;
    public GameObject SniperPlayer;
    public GameObject GoldenAKPlayer;
    public GameObject M249Player;
    public GameObject RocketLauncherPlayer;
    public GameObject RPGPlayer;
    public GameObject SPAS12Player;
    public GameObject SRLPlayer;
    public GameObject RevolverPlayer;
    public GameObject M4LaserPlayer;

    private Vector3 defaultPos;

    // Start is called before the first frame update
    void Start()
    {

        defaultPos.x = 0;
        defaultPos.y = -1;
        defaultPos.z = 0;

        checkOwnedWeapons();

        primaryWeaponDropdown.onValueChanged.AddListener(delegate {
            primaryWeaponDropdownChanged(primaryWeaponDropdownValue);
        });

        secondaryWeaponDropdown.onValueChanged.AddListener(delegate {
            secondaryWeaponDropdownChanged(secondaryWeaponDropdownValue);
        });

        ShopArmouryBackButton.onClick.AddListener(delegate {
            ShopBackButtonClicked();
        });


        primaryWeaponDropdown.ClearOptions();
        secondaryWeaponDropdown.ClearOptions();
        primaryWeaponDropdown.AddOptions(_avaliableWeapons);
        secondaryWeaponDropdown.AddOptions(_avaliableWeapons);

        instantiatePlayer(UserProfile.getPrimaryWeapon());

        Debug.Log("started");

    }

    private void ShopBackButtonClicked()
    {
        UserProfile.setPrimaryWeapon(primaryWeaponDropdown.options[primaryWeaponDropdown.value].text);
        UserProfile.setSecondaryWeapon(secondaryWeaponDropdown.options[secondaryWeaponDropdown.value].text);
    }

    void instantiatePlayer(string selectedWeapon="")
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        GameObject a = Instantiate(weaponNameToPrefab(selectedWeapon), defaultPos, Quaternion.identity);
        a.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        a.GetComponent<PlayerControls>().uiButtonClicked(true);


    }

    private void secondaryWeaponDropdownChanged(object secondaryWeaponDropdownValue)
    {
        _primaryWeapon = primaryWeaponDropdown.options[primaryWeaponDropdown.value].text;
        _secondaryWeapon = secondaryWeaponDropdown.options[secondaryWeaponDropdown.value].text;
        UserProfile.setPrimaryWeapon(_primaryWeapon);
        UserProfile.setSecondaryWeapon(_secondaryWeapon);
        instantiatePlayer(_secondaryWeapon);
    }

    private void primaryWeaponDropdownChanged(object primaryWeaponDropdownValue)
    {
        _primaryWeapon = primaryWeaponDropdown.options[primaryWeaponDropdown.value].text;
        _secondaryWeapon = secondaryWeaponDropdown.options[secondaryWeaponDropdown.value].text;
        UserProfile.setPrimaryWeapon(_primaryWeapon);
        UserProfile.setSecondaryWeapon(_secondaryWeapon);
        instantiatePlayer(_primaryWeapon);
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

    public GameObject weaponNameToPrefab(string weaponName)
    {
        if(weaponName == null)
        {
            return ShotgunPlayer;
        }

        if (weaponName.Equals("USP") || weaponName.Equals(""))
        {
            return BasicPistolPlayer;
        }
        if (weaponName.Equals("MP-5"))
        {
            return BasicMP5Player;
        }
        if (weaponName.Equals("Shotgun"))
        {
            return ShotgunPlayer;
        }
        if (weaponName.Equals("Scout"))
        {
            return SniperPlayer;
        }
        if (weaponName.Equals("AK-47 [Golden]"))
        {
            return GoldenAKPlayer;
        }

        if (weaponName.Equals("Revolver"))
        {
            return RevolverPlayer;
        }

        if (weaponName.Equals("M-4Laser"))
        {
            return M4LaserPlayer;
        }

        if (weaponName.Equals("SPAS-12"))
        {
            return SPAS12Player;
        }

        if (weaponName.Equals("M-249"))
        {
            return M249Player;
        }

        if (weaponName.Equals("SRL"))
        {
            return SRLPlayer;
        }

        if (weaponName.Equals("RPG"))
        {
            return RPGPlayer;
        }

        if (weaponName.Equals("RocketLauncher"))
        {
            return RocketLauncherPlayer;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

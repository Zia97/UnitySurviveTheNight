using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    GameObject _moneyButtonObject;
    Button _moneyButton;

    GameObject _purchaseMP5ButtonObject;
    Button _purchaseMP5Button;

    GameObject _purchaseShotgunButtonObject;
    Button _purchaseShotgunButton;

    GameObject _purchaseScoutButtonObject;
    Button _purchaseScoutButton;

    public Button _page1Button;
    public Button _page2Button;

    GameObject _purchaseGoldenAKButtonObject;
    Button _purchaseGoldenAKButton;

    GameObject _purchaseRevolverButtonObject;
    Button _purchaseRevolverButton;

    GameObject _purchaseM4LaserButtonObject;
    Button _purchaseM4LaserButton;

    GameObject _purchaseSPAS12ButtonObject;
    Button _purchaseSPAS12Button;

    GameObject _purchaseM249ButtonObject;
    Button _purchaseM249Button;

    GameObject _purchaseSRLButtonObject;
    Button _purchaseSRLButton;

    GameObject _purchaseRPGButtonObject;
    Button _purchaseRPGButton;

    GameObject _purchaseRocketLauncherButtonObject;
    Button _purchaseRocketLauncherButton;

    public GameObject BlurCanvasObject;
    Canvas BlurCanvas;

    public GameObject NotEnoughCoinsObject;
    Canvas NotEnoughCoinsCanvas;

    public GameObject PurchasedCanvasObject;
    Canvas PurchasedCanvas;
    public GameObject ConfirmedPurchase_Text;

    public GameObject ConfirmationCanvasObject;
    Canvas ConfirmationCanvas;
    public GameObject Purchase_Text;

    public GameObject ImageMP5;
    public GameObject ImageShotgun;
    public GameObject ImageScout;
    public GameObject ImageGoldenAK;
    public GameObject ImageRevolver;
    public GameObject ImageM4Laser;
    public GameObject ImageSPAS12;
    public GameObject ImageM249;
    public GameObject ImageSRL;
    public GameObject ImageRPG;
    public GameObject ImageRocketLauncher;

    public GameObject ConfirmedImageMP5;
    public GameObject ConfirmedImageShotgun;
    public GameObject ConfirmedImageScout;
    public GameObject ConfirmedImageGoldenAK;
    public GameObject ConfirmedImageRevolver;
    public GameObject ConfirmedImageM4Laser;
    public GameObject ConfirmedImageSPAS12;
    public GameObject ConfirmedImageM249;
    public GameObject ConfirmedImageSRL;
    public GameObject ConfirmedImageRPG;
    public GameObject ConfirmedImageRocketLauncher;

    public GameObject MP5OwnedDisplay;
    public GameObject ShotgunOwnedDisplay;
    public GameObject ScoutOwnedDisplay;
    public GameObject GoldenAKOwnedDisplay;
    public GameObject RevolverOwnedDisplay;
    public GameObject M4LaserOwnedDisplay;
    public GameObject SPAS12OwnedDisplay;
    public GameObject M249OwnedDisplay;
    public GameObject SRLOwnedDisplay;
    public GameObject RPGOwnedDisplay;
    public GameObject RocketLauncherOwnedDisplay;


    private string _selectedStoreWeapon;


    // Start is called before the first frame update
    void Start()
    {
        #region Canvas'

        PlayerPrefs.DeleteAll();

        BlurCanvasObject = GameObject.Find("BlurCanvas");
        BlurCanvas = BlurCanvasObject.GetComponent<Canvas>();
        BlurCanvas.enabled = false;

        PurchasedCanvasObject = GameObject.Find("PurchasedCanvas");
        PurchasedCanvas = PurchasedCanvasObject.GetComponent<Canvas>();
        PurchasedCanvas.enabled = false;

        NotEnoughCoinsObject = GameObject.Find("NotEnoughCoinsCanvas");
        NotEnoughCoinsCanvas = NotEnoughCoinsObject.GetComponent<Canvas>();
        NotEnoughCoinsCanvas.enabled = false;

        ConfirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        ConfirmationCanvas = ConfirmationCanvasObject.GetComponent<Canvas>();
        ConfirmationCanvas.enabled = false;

        #endregion

        #region Images GameObjects

        disableAllImages();
    
        setOwnedTofalse();


        #endregion

        #region MoneyButton

        _moneyButtonObject = GameObject.Find("Btn_Money");
        _moneyButton = _moneyButtonObject.GetComponent<Button>();

        #endregion

        #region Gun Buttons
        activatePage1Weapons();

        #endregion

        updateShopButtonsPage1();
        updateUserMoney();
    }


    private void updateShopButtonsPage1()
    {
        if (PlayerPrefs.HasKey("MP5"))
        {
            _purchaseMP5Button.enabled = false;
            MP5OwnedDisplay.SetActive(true);
        }
        else
        {
            _purchaseMP5Button.enabled = true;
            MP5OwnedDisplay.SetActive(false);
        }

        if (PlayerPrefs.HasKey("Shotgun"))
        {
            _purchaseShotgunButton.enabled = false;
            ShotgunOwnedDisplay.SetActive(true);
        }
        else
        {
            _purchaseShotgunButton.enabled = true;
            ShotgunOwnedDisplay.SetActive(false);
        }

        if (PlayerPrefs.HasKey("GoldenAK"))
        {
            _purchaseGoldenAKButton.enabled = false;
            GoldenAKOwnedDisplay.SetActive(true);

        }
        else
        {
            _purchaseGoldenAKButton.enabled = true;
            GoldenAKOwnedDisplay.SetActive(false);
        }

        if (PlayerPrefs.HasKey("Scout"))
        {
            _purchaseScoutButton.enabled = false;
            ScoutOwnedDisplay.SetActive(true);
        }
        else
        {
            _purchaseScoutButton.enabled = true;
            ScoutOwnedDisplay.SetActive(false);
        }

        if (PlayerPrefs.HasKey("M4Laser"))
        {
            _purchaseM4LaserButton.enabled = false;
            M4LaserOwnedDisplay.SetActive(true);
        }
        else
        {
            _purchaseM4LaserButton.enabled = true;
            M4LaserOwnedDisplay.SetActive(false);
        }

        if (PlayerPrefs.HasKey("Revolver"))
        {
            _purchaseRevolverButton.enabled = false;
            RevolverOwnedDisplay.SetActive(true);
        }
        else
        {
            _purchaseRevolverButton.enabled = true;
            RevolverOwnedDisplay.SetActive(false);
        }

        if (PlayerPrefs.HasKey("SPAS12"))
        {
            _purchaseSPAS12Button.enabled = false;
            SPAS12OwnedDisplay.SetActive(true);
        }
        else
        {
            _purchaseSPAS12Button.enabled = true;
            SPAS12OwnedDisplay.SetActive(false);
        }

        if (PlayerPrefs.HasKey("M249"))
        {
            _purchaseM249Button.enabled = false;
            M249OwnedDisplay.SetActive(true);
        }
        else
        {
            _purchaseM249Button.enabled = true;
            M249OwnedDisplay.SetActive(false);
        }
    }

    public void updateShopButtonsPage2()
    {
        if (PlayerPrefs.HasKey("SRL"))
        {
            _purchaseSRLButton.enabled = false;
            SRLOwnedDisplay.SetActive(true);
        }
        else
        {
            _purchaseSRLButton.enabled = true;
            SRLOwnedDisplay.SetActive(false);
        }

        if (PlayerPrefs.HasKey("RPG"))
        {
            _purchaseRPGButton.enabled = false;
            RPGOwnedDisplay.SetActive(true);
        }
        else
        {
            _purchaseRPGButton.enabled = true;
            RPGOwnedDisplay.SetActive(false);
        }

        if (PlayerPrefs.HasKey("RocketLauncher"))
        {
            _purchaseRocketLauncherButton.enabled = false;
            RocketLauncherOwnedDisplay.SetActive(true);
        }
        else
        {
            _purchaseRocketLauncherButton.enabled = true;
            RocketLauncherOwnedDisplay.SetActive(false);
        }
    }

    private void activatePage1Weapons()
    {
        //MP5
        _purchaseMP5ButtonObject = GameObject.Find("PurchaseMP5Button");
        _purchaseMP5Button = _purchaseMP5ButtonObject.GetComponent<Button>();
        _purchaseMP5Button.onClick.AddListener(_purchaseMP5ButtonClicked);

        //Shotgun
        _purchaseShotgunButtonObject = GameObject.Find("PurchaseShotgunButton");
        _purchaseShotgunButton = _purchaseShotgunButtonObject.GetComponent<Button>();
        _purchaseShotgunButton.onClick.AddListener(_purchaseShotgunButtonClicked);

        //GoldenAK
        _purchaseGoldenAKButtonObject = GameObject.Find("PurchaseGoldenAKButton");
        _purchaseGoldenAKButton = _purchaseGoldenAKButtonObject.GetComponent<Button>();
        _purchaseGoldenAKButton.onClick.AddListener(_purchaseGoldenAKButtonClicked);

        //Scout
        _purchaseScoutButtonObject = GameObject.Find("PurchaseScoutButton");
        _purchaseScoutButton = _purchaseScoutButtonObject.GetComponent<Button>();
        _purchaseScoutButton.onClick.AddListener(_purchaseScoutButtonClicked);

        //Revolver
        _purchaseRevolverButtonObject = GameObject.Find("PurchaseRevolverButton");
        _purchaseRevolverButton = _purchaseRevolverButtonObject.GetComponent<Button>();
        _purchaseRevolverButton.onClick.AddListener(_purchaseRevolverButtonClicked);

        //M4Lazer
        _purchaseM4LaserButtonObject = GameObject.Find("PurchaseM4LaserButton");
        _purchaseM4LaserButton = _purchaseM4LaserButtonObject.GetComponent<Button>();
        _purchaseM4LaserButton.onClick.AddListener(_purchaseM4LaserButtonClicked);

        //SPAS12
        _purchaseSPAS12ButtonObject = GameObject.Find("PurchaseSPAS12Button");
        _purchaseSPAS12Button = _purchaseSPAS12ButtonObject.GetComponent<Button>();
        _purchaseSPAS12Button.onClick.AddListener(_purchaseSPAS12ButtonClicked);

        //M429
        _purchaseM249ButtonObject = GameObject.Find("PurchaseM249Button");
        _purchaseM249Button = _purchaseM249ButtonObject.GetComponent<Button>();
        _purchaseM249Button.onClick.AddListener(_purchasedM249ButtonClicked);

    }

    public void activatePage2Weapons()
    {
        //SRL
        _purchaseSRLButtonObject = GameObject.Find("PurchaseSRLButton");
        _purchaseSRLButton = _purchaseSRLButtonObject.GetComponent<Button>();
        _purchaseSRLButton.onClick.AddListener(_purchasedSRLButtonClicked);

        //RPG
        _purchaseRPGButtonObject = GameObject.Find("PurchaseRPGButton");
        _purchaseRPGButton = _purchaseRPGButtonObject.GetComponent<Button>();
        _purchaseRPGButton.onClick.AddListener(_purchaseRPGButtonClicked);

        //RocketLauncher
        _purchaseRocketLauncherButtonObject = GameObject.Find("PurchaseRocketLauncherButton");
        _purchaseRocketLauncherButton = _purchaseRocketLauncherButtonObject.GetComponent<Button>();
        _purchaseRocketLauncherButton.onClick.AddListener(_purchaseRocketLauncherButtonClicked);
    }

    #region Purchase buttons 

    private void _purchaseGoldenAKButtonClicked()
    {
        InitiateScreenFocus();
        CheckForEnoughCoins("GoldenAK");
        var check = CheckForEnoughCoins("GoldenAK");
        displayConfirmation(check, "GoldenAK");
        _selectedStoreWeapon = "GoldenAK";
    }

    private void _purchaseScoutButtonClicked()
    {
        InitiateScreenFocus();
        CheckForEnoughCoins("Scout");
        var check = CheckForEnoughCoins("Scout");
        displayConfirmation(check, "Scout");
        _selectedStoreWeapon = "Scout";
    }

    private void _purchaseShotgunButtonClicked()
    {
        InitiateScreenFocus();
        CheckForEnoughCoins("Shotgun");
        var check = CheckForEnoughCoins("Shotgun");
        displayConfirmation(check, "Shotgun");
        _selectedStoreWeapon = "Shotgun";
    }

    private void _purchaseMP5ButtonClicked()
    {
        InitiateScreenFocus();
        var check = CheckForEnoughCoins("MP5");
        displayConfirmation(check, "MP5");
        _selectedStoreWeapon = "MP5";
    }

    private void _purchaseRevolverButtonClicked()
    {
        InitiateScreenFocus();
        var check = CheckForEnoughCoins("Revolver");
        displayConfirmation(check, "Revolver");
        _selectedStoreWeapon = "Revolver";
    }

    private void _purchaseRocketLauncherButtonClicked()
    {
        InitiateScreenFocus();
        var check = CheckForEnoughCoins("RocketLauncher");
        displayConfirmation(check, "RocketLauncher");
        _selectedStoreWeapon = "RocketLauncher";
    }

    private void _purchaseRPGButtonClicked()
    {
        InitiateScreenFocus();
        var check = CheckForEnoughCoins("RPG");
        displayConfirmation(check, "RPG");
        _selectedStoreWeapon = "RPG";
    }

    private void _purchasedSRLButtonClicked()
    {
        InitiateScreenFocus();
        var check = CheckForEnoughCoins("SRL");
        displayConfirmation(check, "SRL");
        _selectedStoreWeapon = "SRL";
    }

    private void _purchasedM249ButtonClicked()
    {
        InitiateScreenFocus();
        var check = CheckForEnoughCoins("M249");
        displayConfirmation(check, "M249");
        _selectedStoreWeapon = "M249";
    }

    private void _purchaseSPAS12ButtonClicked()
    {
        InitiateScreenFocus();
        var check = CheckForEnoughCoins("SPAS12");
        displayConfirmation(check, "SPAS12");
        _selectedStoreWeapon = "SPAS12";
    }

    private void _purchaseM4LaserButtonClicked()
    {
        InitiateScreenFocus();
        var check = CheckForEnoughCoins("M4Laser");
        displayConfirmation(check, "M4Laser");
        _selectedStoreWeapon = "M4Laser";
    }

    #endregion

    public void _purchaseConfirmButtonClicked()
    {
        ConfirmationCanvas.enabled = false;
        PlayerPrefs.SetString(_selectedStoreWeapon, _selectedStoreWeapon);
        ConfirmedPurchase_Text.GetComponentInChildren<Text>().text = _selectedStoreWeapon + " purchased!";
        if (_selectedStoreWeapon.Equals("MP5"))
        {
            ConfirmedImageMP5.SetActive(true);
            UserProfile.decreaseCoins(250);
        }
        else if (_selectedStoreWeapon.Equals("Scout"))
        {
            ConfirmedImageScout.SetActive(true);
            UserProfile.decreaseCoins(650);
        }
        else if (_selectedStoreWeapon.Equals("Shotgun"))
        {
            ConfirmedImageShotgun.SetActive(true);
            UserProfile.decreaseCoins(500);
        }
        else if (_selectedStoreWeapon.Equals("GoldenAK"))
        {
            ConfirmedImageGoldenAK.SetActive(true);
            UserProfile.decreaseCoins(1000);
        }
        else if (_selectedStoreWeapon.Equals("Revolver"))
        {
            ConfirmedImageRevolver.SetActive(true);
            UserProfile.decreaseCoins(150);
        }
        else if (_selectedStoreWeapon.Equals("M4Laser"))
        {
            ConfirmedImageM4Laser.SetActive(true);
            UserProfile.decreaseCoins(150);
        }
        else if (_selectedStoreWeapon.Equals("SPAS12"))
        {
            ConfirmedImageSPAS12.SetActive(true);
            UserProfile.decreaseCoins(150);
        }
        else if (_selectedStoreWeapon.Equals("M249"))
        {
            ConfirmedImageM249.SetActive(true);
            UserProfile.decreaseCoins(150);
        }
        else if (_selectedStoreWeapon.Equals("SRL"))
        {
            ConfirmedImageSRL.SetActive(true);
            UserProfile.decreaseCoins(150);
        }
        else if (_selectedStoreWeapon.Equals("RPG"))
        {
            ConfirmedImageRPG.SetActive(true);
            UserProfile.decreaseCoins(150);
        }
        else if (_selectedStoreWeapon.Equals("RocketLauncher"))
        {
            ConfirmedImageRocketLauncher.SetActive(true);
            UserProfile.decreaseCoins(150);
        }
        PurchasedCanvas.enabled = true;
        updateUserMoney();
        _selectedStoreWeapon = "";
    }


    #region Cancel buttons

    public void _purchaseCancelButtonClicked()
    {
        ConfirmationCanvas.enabled = false;
        BlurCanvas.enabled = false;
        disableAllImages();
        _selectedStoreWeapon = "";
    }

    public void _weaponPurchasedCloseButtonClicked()
    {
        PurchasedCanvas.enabled = false;
        BlurCanvas.enabled = false;
        disableAllImages();
        updateShopButtonsPage1();
        updateShopButtonsPage2();
        _selectedStoreWeapon = "";
    }

    public void _notEnoughCoinsCancelButtonClicked()
    {
        NotEnoughCoinsCanvas.enabled = false;
        _selectedStoreWeapon = "";
        BlurCanvas.enabled = false;
    }

    public void disableAllImages()
    {
        ImageMP5.SetActive(false);
        ImageShotgun.SetActive(false);
        ImageScout.SetActive(false);
        ImageGoldenAK.SetActive(false);
        ImageRevolver.SetActive(false);
        ImageM4Laser.SetActive(false);
        ImageSPAS12.SetActive(false);
        ImageM249.SetActive(false);
        ImageSRL.SetActive(false);
        ImageRPG.SetActive(false);
        ImageRocketLauncher.SetActive(false);

        ConfirmedImageMP5.SetActive(false);
        ConfirmedImageShotgun.SetActive(false);
        ConfirmedImageScout.SetActive(false);
        ConfirmedImageGoldenAK.SetActive(false);
        ConfirmedImageRevolver.SetActive(false);
        ConfirmedImageM4Laser.SetActive(false);
        ConfirmedImageSPAS12.SetActive(false);
        ConfirmedImageM249.SetActive(false);
        ConfirmedImageSRL.SetActive(false);
        ConfirmedImageRPG.SetActive(false);
        ConfirmedImageRocketLauncher.SetActive(false);
    }

    void setOwnedTofalse()
    {
        MP5OwnedDisplay.SetActive(false);
        ShotgunOwnedDisplay.SetActive(false);
        ScoutOwnedDisplay.SetActive(false);
        GoldenAKOwnedDisplay.SetActive(false);
        RevolverOwnedDisplay.SetActive(false);
        M4LaserOwnedDisplay.SetActive(false);
        SPAS12OwnedDisplay.SetActive(false);
        M249OwnedDisplay.SetActive(false);
        SRLOwnedDisplay.SetActive(false);
        RPGOwnedDisplay.SetActive(false);
        RocketLauncherOwnedDisplay.SetActive(false);
    }

    #endregion

    //Modify confirmation display
    private void displayConfirmation(bool enoughMoneyForPurchase, string weapon)
    {
        if (enoughMoneyForPurchase)
        {
            ConfirmationCanvas.enabled = true;

            if (weapon.Equals("MP5"))
            {
                ImageMP5.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 250"+ System.Environment.NewLine+ "New Balance: " +(UserProfile.getCoins()-250);
            }
            else if (weapon.Equals("Scout"))
            {
                ImageScout.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 650" + System.Environment.NewLine + "New Balance: " + (UserProfile.getCoins() - 650);
            }
            else if (weapon.Equals("Shotgun"))
            {
                ImageShotgun.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 500" + System.Environment.NewLine + "New Balance: " + (UserProfile.getCoins() - 500);
            }
            else if (weapon.Equals("GoldenAK"))
            {
                ImageGoldenAK.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 1000" + System.Environment.NewLine + "New Balance: " + (UserProfile.getCoins() - 1000);
            }
            else if (weapon.Equals("Revolver"))
            {
                ImageRevolver.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 150" + System.Environment.NewLine + "New Balance: " + (UserProfile.getCoins() - 150);
            }
            else if (weapon.Equals("M4Laser"))
            {
                ImageM4Laser.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 150" + System.Environment.NewLine + "New Balance: " + (UserProfile.getCoins() - 150);
            }
            else if (weapon.Equals("SPAS12"))
            {
                ImageSPAS12.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 150" + System.Environment.NewLine + "New Balance: " + (UserProfile.getCoins() - 150);
            }
            else if (weapon.Equals("M249"))
            {
                ImageM249.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 150" + System.Environment.NewLine + "New Balance: " + (UserProfile.getCoins() - 150);
            }
            else if (weapon.Equals("SRL"))
            {
                ImageSRL.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 150" + System.Environment.NewLine + "New Balance: " + (UserProfile.getCoins() - 150);
            }
            else if (weapon.Equals("RPG"))
            {
                ImageRPG.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 150" + System.Environment.NewLine + "New Balance: " + (UserProfile.getCoins() - 150);
            }
            else if (weapon.Equals("RocketLauncher"))
            {
                ImageRocketLauncher.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 150" + System.Environment.NewLine + "New Balance: " + (UserProfile.getCoins() - 150);
            }

        }
        else
        {
            NotEnoughCoinsCanvas.enabled = true;
        }
    }

    //Prices for guns hard-coded
    private bool CheckForEnoughCoins(string _selectedGun)
    {
        if (_selectedGun.Equals("MP5"))
        {
            if (300>= 250)
            {
                return true;
            }
        }
        else if (_selectedGun.Equals("Scout"))
        {
            if (700 >= 650)
            {
                return true;
            }
        }
        else if (_selectedGun.Equals("Shotgun"))
        {
            if (UserProfile.getCoins() >= 500)
            {
                return true;
            }
        }
        else if (_selectedGun.Equals("GoldenAK"))
        {
            if (2000 >= 1000)
            {
                return true;
            }
        }
        else if (_selectedGun.Equals("Revolver"))
        {
            if (2000 >= 300)
            {
                return true;
            }
        }
        else if (_selectedGun.Equals("M4Laser"))
        {
            if (2000 >= 300)
            {
                return true;
            }
        }
        else if (_selectedGun.Equals("SPAS12"))
        {
            if (2000 >= 300)
            {
                return true;
            }
        }
        else if (_selectedGun.Equals("M249"))
        {
            if (2000 >= 300)
            {
                return true;
            }
        }
        else if (_selectedGun.Equals("SRL"))
        {
            if (2000 >= 300)
            {
                return true;
            }
        }
        else if (_selectedGun.Equals("RPG"))
        {
            if (2000 >= 300)
            {
                return true;
            }
        }
        else if (_selectedGun.Equals("RocketLauncher"))
        {
            if (2000 >= 300)
            {
                return true;
            }
        }
        return false;
    }

    //Blurs screen background
    private void InitiateScreenFocus()
    {
        BlurCanvas.enabled = true;
    }

    private void updateUserMoney()
    {
        _moneyButton.GetComponentInChildren<Text>().text = UserProfile.getCoins().ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

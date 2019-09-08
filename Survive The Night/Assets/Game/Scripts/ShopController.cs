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

    GameObject _purchaseGoldenAKButtonObject;
    Button _purchaseGoldenAKButton;

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

    public GameObject ConfirmedImageMP5;
    public GameObject ConfirmedImageShotgun;
    public GameObject ConfirmedImageScout;
    public GameObject ConfirmedImageGoldenAK;

    public GameObject MP5OwnedDisplay;
    public GameObject ShotgunOwnedDisplay;
    public GameObject ScoutOwnedDisplay;
    public GameObject GoldenAKOwnedDisplay;


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

        ImageMP5.SetActive(false);
        ImageShotgun.SetActive(false);
        ImageScout.SetActive(false);
        ImageGoldenAK.SetActive(false);

        ConfirmedImageMP5.SetActive(false);
        ConfirmedImageShotgun.SetActive(false);
        ConfirmedImageScout.SetActive(false);
        ConfirmedImageGoldenAK.SetActive(false);

        MP5OwnedDisplay.SetActive(false);
        ShotgunOwnedDisplay.SetActive(false);
        ScoutOwnedDisplay.SetActive(false);
        GoldenAKOwnedDisplay.SetActive(false);

        #endregion

        #region MoneyButton

        _moneyButtonObject = GameObject.Find("Btn_Money");
        _moneyButton = _moneyButtonObject.GetComponent<Button>();

        #endregion

        #region Gun Buttons
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

        #endregion

        updateShopButtons();
        updateUserMoney();
    }


    private void updateShopButtons()
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
        updateShopButtons();
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
        ConfirmedImageMP5.SetActive(false);
        ConfirmedImageGoldenAK.SetActive(false);
        ConfirmedImageScout.SetActive(false);
        ConfirmedImageShotgun.SetActive(false);

        ImageGoldenAK.SetActive(false);
        ImageScout.SetActive(false);
        ImageMP5.SetActive(false);
        ImageShotgun.SetActive(false);
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
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 250" + System.Environment.NewLine + "New Balance: " + (UserProfile.getCoins() - 650);
            }
            else if (weapon.Equals("Shotgun"))
            {
                ImageShotgun.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 250" + System.Environment.NewLine + "New Balance: " + (UserProfile.getCoins() - 500);
            }
            else if (weapon.Equals("GoldenAK"))
            {
                ImageGoldenAK.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 250" + System.Environment.NewLine + "New Balance: " + (UserProfile.getCoins() - 1000);
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

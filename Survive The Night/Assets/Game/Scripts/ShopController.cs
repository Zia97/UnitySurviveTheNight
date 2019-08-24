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

    public GameObject ConfirmationCanvasObject;
    Canvas ConfirmationCanvas;
    public GameObject Purchase_Text;

    public GameObject ImageMP5;
    public GameObject ImageShotgun;
    public GameObject ImageScout;
    public GameObject ImageGoldenAK;


    // Start is called before the first frame update
    void Start()
    {
        BlurCanvasObject = GameObject.Find("BlurCanvas");
        BlurCanvas = BlurCanvasObject.GetComponent<Canvas>();
        BlurCanvas.enabled = false;

        NotEnoughCoinsObject = GameObject.Find("NotEnoughCoinsCanvas");
        NotEnoughCoinsCanvas = NotEnoughCoinsObject.GetComponent<Canvas>();
        NotEnoughCoinsCanvas.enabled = false;

        ConfirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        ConfirmationCanvas = ConfirmationCanvasObject.GetComponent<Canvas>();
        ConfirmationCanvas.enabled = false;

        ImageMP5.SetActive(false);
        ImageShotgun.SetActive(false);
        ImageScout.SetActive(false);
        ImageGoldenAK.SetActive(false);

        _moneyButtonObject = GameObject.Find("Btn_Money");
        _moneyButton = _moneyButtonObject.GetComponent<Button>();
        _moneyButton.GetComponentInChildren<Text>().text = UserProfile.getCoins().ToString();

        //MP5
        _purchaseMP5ButtonObject = GameObject.Find("PurchaseMP5Button");
        _purchaseMP5Button = _purchaseMP5ButtonObject.GetComponent<Button>();
        _purchaseMP5Button.onClick.AddListener(_purchaseMP5ButtonClicked);

        //Shotgun
        _purchaseShotgunButtonObject = GameObject.Find("PurchaseShotgunButton");
        _purchaseShotgunButton = _purchaseShotgunButtonObject.GetComponent<Button>();
        _purchaseShotgunButton.onClick.AddListener(_purchaseShotgunButtonClicked);

        //Scout
        _purchaseScoutButtonObject = GameObject.Find("PurchaseScoutButton");
        _purchaseScoutButton = _purchaseScoutButtonObject.GetComponent<Button>();
        _purchaseScoutButton.onClick.AddListener(_purchaseScoutButtonClicked);

        //GoldenAK
        _purchaseGoldenAKButtonObject = GameObject.Find("PurchaseGoldenAKButton");
        _purchaseGoldenAKButton = _purchaseGoldenAKButtonObject.GetComponent<Button>();
        _purchaseGoldenAKButton.onClick.AddListener(_purchaseGoldenAKButtonClicked);


        //Blur Canvas
        BlurCanvasObject = GameObject.Find("BlurCanvas");
    }

    private void _purchaseGoldenAKButtonClicked()
    {
        InitiateScreenFocus();
        CheckForEnoughCoins("GoldenAK");
        var check = CheckForEnoughCoins("GoldenAK");
        displayConfirmation(check, "GoldenAK");
    }


    private void _purchaseScoutButtonClicked()
    {
        InitiateScreenFocus();
        CheckForEnoughCoins("Scout");
        var check = CheckForEnoughCoins("Scout");
        displayConfirmation(check, "Scout");
    }

    private void _purchaseShotgunButtonClicked()
    {
        InitiateScreenFocus();
        CheckForEnoughCoins("Shotgun");
        var check = CheckForEnoughCoins("Shotgun");
        displayConfirmation(check, "Shotgun");
    }


    private void _purchaseMP5ButtonClicked()
    {
        InitiateScreenFocus();
        var check = CheckForEnoughCoins("MP5");
        displayConfirmation(check, "MP5");
    }

    private void displayConfirmation(bool enoughMoneyForPurchase, string weapon)
    {
        if (enoughMoneyForPurchase)
        {
            Debug.Log("ccanvas enabled");
            ConfirmationCanvas.enabled = true;

            if (weapon.Equals("MP5"))
            {
                ImageMP5.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 250"+ System.Environment.NewLine+ "New Balance: " +(UserProfile.getCoins()-250);
                UserProfile.decreaseCoins(250);
            }
            else if (weapon.Equals("Scout"))
            {
                ImageScout.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 250" + System.Environment.NewLine + "New Balance: " + (UserProfile.getCoins() - 650);
                UserProfile.decreaseCoins(650);
            }
            else if (weapon.Equals("Shotgun"))
            {
                ImageShotgun.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 250" + System.Environment.NewLine + "New Balance: " + (UserProfile.getCoins() - 500);
                UserProfile.decreaseCoins(500);
            }
            else if (weapon.Equals("GoldenAK"))
            {
                ImageGoldenAK.SetActive(true);
                Purchase_Text.GetComponentInChildren<Text>().text = "Current balance: " + UserProfile.getCoins() + System.Environment.NewLine + "Cost: 250" + System.Environment.NewLine + "New Balance: " + (UserProfile.getCoins() - 1000);
                UserProfile.decreaseCoins(1000);
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
            if (650 >= 650)
            {
                return true;
            }
        }
        else if (_selectedGun.Equals("Shotgun"))
        {
            if (500 >= 500)
            {
                return true;
            }
        }
        else if (_selectedGun.Equals("GoldenAK"))
        {
            if (1000>= 1000)
            {
                return true;
            }
        }
        return false;
    }


    private void InitiateScreenFocus()
    {
        BlurCanvas.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {

    }
}

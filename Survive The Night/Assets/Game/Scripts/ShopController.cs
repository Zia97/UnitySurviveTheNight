using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    GameObject _moneyButtonObject;
    Button _moneyButton;

   
    // Start is called before the first frame update
    void Start()
    {
      _moneyButtonObject = GameObject.Find("Btn_Money");
      _moneyButton =  _moneyButtonObject.GetComponent<Button>();
        _moneyButton.GetComponentInChildren<Text>().text = UserProfile.getCoins().ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

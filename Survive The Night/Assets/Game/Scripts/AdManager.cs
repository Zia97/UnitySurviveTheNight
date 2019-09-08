using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{

    GameObject PurchaseCoinsMoneyObject;

    
    // Start is called before the first frame update
    void Start()
    {
        PurchaseCoinsMoneyObject = GameObject.Find("PurchaseCoinsMoney");
        PurchaseCoinsMoneyObject.GetComponentInChildren<Text>().text = UserProfile.getCoins().ToString();
    }

    public void showAD()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult};
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                UserProfile.addCoins(25);
                updateUserMoney();
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }

    private void updateUserMoney()
    {
        PurchaseCoinsMoneyObject.GetComponentInChildren<Text>().text = UserProfile.getCoins().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

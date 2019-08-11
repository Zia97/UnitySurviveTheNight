//Author : Qasim Ziauddin

//Loads store
using UnityEngine;
using UnityEngine.SceneManagement;

public class PurchaseCoinsOnClick : MonoBehaviour
{
    public void LoadPurchaseCoins(string sceneName)
    {
        Debug.Log("loading " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void add2Coins()
    {
        UserProfile.addCoins(2);
    }
}

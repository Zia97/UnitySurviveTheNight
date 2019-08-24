//Author : Qasim Ziauddin

//Loads store
using UnityEngine;
using UnityEngine.SceneManagement;

public class PurchaseCoinsOnClick : MonoBehaviour
{
    public void LoadPurchaseCoins(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void add2Coins()
    {
        UserProfile.addCoins(2);
    }
}

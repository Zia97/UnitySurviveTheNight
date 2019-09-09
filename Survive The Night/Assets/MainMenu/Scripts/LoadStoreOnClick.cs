//Author : Qasim Ziauddin

//Loads store
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStoreOnClick : MonoBehaviour
{
    public void LoadStore(string sceneName)
    {
        Debug.Log("loading " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void deletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}

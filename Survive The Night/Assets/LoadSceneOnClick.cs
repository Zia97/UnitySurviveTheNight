//Author : Qasim Ziauddin

//This class controls the difficulty selection. Set the difficulty value in hte difficulty selector depending on which difficult is selected
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public void LoadSceneEasy(string sceneName)
    {
        PlayerPrefs.SetInt("Coins", 0);
        DifficultySelector.setDifficulty(0.75);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneMedium(string sceneName)
    {
        DifficultySelector.setDifficulty(1);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneHard(string sceneName)
    {
        DifficultySelector.setDifficulty(1.2);
        SceneManager.LoadScene(sceneName);
    }

    public void loadPreGrame()
    {
        string temp = PlayerPrefs.GetString("IsFirstTime");
        if (temp == null)
        {
            Debug.Log("First time loaded");
            PlayerPrefs.SetString("IsFirstTime", "NotFirst");
        }
        else
        {

        }
    }

}

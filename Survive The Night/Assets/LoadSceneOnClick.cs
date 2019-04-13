using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public void LoadSceneEasy(string sceneName)
    {
        DifficultySelector.setDifficulty(1);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneMedium(string sceneName)
    {
        DifficultySelector.setDifficulty(1.2);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneHard(string sceneName)
    {
        DifficultySelector.setDifficulty(1.5);
        SceneManager.LoadScene(sceneName);
    }

}

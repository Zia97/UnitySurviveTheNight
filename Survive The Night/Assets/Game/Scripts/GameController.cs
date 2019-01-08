using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    

    public Text scoretext;
    private int score = 0;

    public Text healthText;
    private int wallHeath = 100;

    public Text gameOverText;

    private bool _gameOver;

    private void Start()
    {
        scoretext.text = "Score : " + score;
        healthText.text = "Health: " + wallHeath + "/100";
        gameOverText.text = "";
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (!_gameOver)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

            hazardCount = hazardCount + 5;

            yield return new WaitForSeconds(waveWait);

            if (_gameOver)
            {
                break;
            }
        }
      
    }

    public void damageWall(int damage)
    {
        if (!_gameOver)
        { 
            wallHeath = wallHeath - damage;
            healthText.text = "Health: " + wallHeath + "/100";
        }

        if(wallHeath<=0)
        {
            healthText.text = "Health: 0/100";
            gameOver();
        }
    }

    public void updateScore(int scoreValue)
    {
        score = score + scoreValue;
        scoretext.text = "Score : " + score;
    }

    public void gameOver()
    {
        _gameOver = true;
        gameOverText.text = "Game Over";
    }
 
}

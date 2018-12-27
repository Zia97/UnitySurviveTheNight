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

    private void Start()
    {
        scoretext.text = "Score : " + score;
        healthText.text = "Health: " + wallHeath + "/100";
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
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
        }
      
    }

    public void damageWall(int damage)
    {
        wallHeath = wallHeath - damage;
        healthText.text = "Health: "+wallHeath + "/100";
    }

    public void updateScore()
    {
        score++;
        scoretext.text = "Score : " + score;
    }
 
}

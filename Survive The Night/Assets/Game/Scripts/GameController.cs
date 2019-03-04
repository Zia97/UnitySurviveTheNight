using Assets.Game.Scripts;
using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject basicEnemy;
    public GameObject mediumEnemy;
    public GameObject officerEnemy;
    public GameObject dogEnemy;
    public GameObject runnerEnemy;
    public Vector3 spawnValues;

    public BasicEnemy basicEnemyObject;
    public MediumEnemy mediumEnemyObject;

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private int roundWaveDifficulty = 10;

    public Text scoretext;
    private int score = 0;
    private int currentWaveScore = 0;

    public Text healthText;
    private int wallHeath = 100;

    public Text gameOverText;
    public Text waveText;

    public Text ammoText;

    private bool _gameOver;
    private bool _roundOver;
    private bool _beginNextRound;

    public Canvas roundOverCanvas;
    public GameObject roundOverGameObject;

    public GameObject _playerGameObject;
    public PlayerControls _player;

    private Character myCharacter;

    private int _waveCount = 1;


    private System.Random rnd = new System.Random();

    private void Start()
    {
        _playerGameObject = GameObject.FindWithTag("Player");
    
        _player= _playerGameObject.GetComponent<PlayerControls>();
        myCharacter = _playerGameObject.GetComponent<Character>();
        roundOverGameObject = GameObject.FindWithTag("RoundEndCanvas");
        roundOverGameObject.SetActive(true);
        roundOverCanvas = roundOverGameObject.GetComponent<Canvas>();
        
        roundOverCanvas.enabled = false;

        scoretext.text = "Score : " + score;
        healthText.text = "Health: " + wallHeath + "/100";
        gameOverText.text = "";
        waveText.text = "Wave "+_waveCount.ToString();
        ammoText.text = (myCharacter.Firearm.Params.MagazineCapacity - myCharacter.Firearm.AmmoShooted) + "/" + myCharacter.Firearm.Params.MagazineCapacity;
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        ammoText.text = (myCharacter.Firearm.Params.MagazineCapacity - myCharacter.Firearm.AmmoShooted) + "/" + myCharacter.Firearm.Params.MagazineCapacity;
    }

    IEnumerator SpawnWaves()
    {
        healthText.text = "Health: " + wallHeath + "/100";
        _player.AreControlsEnabled(true);

        while (!_gameOver && !_roundOver)
        {
            roundOverCanvas.enabled = false;
            waveText.text = "Wave " + _waveCount.ToString();
            roundWaveDifficulty = roundWaveDifficulty + 4;
            int currentWaveDifficultyValue = 0;
            currentWaveScore = 0;
            int randomValue = 0;

            yield return new WaitForSecondsRealtime(4);
            waveText.text = "";

            while (currentWaveDifficultyValue < roundWaveDifficulty)
            {             

                if(roundWaveDifficulty-currentWaveDifficultyValue>=4)
                {
                   randomValue = rnd.Next(3);
                }
                else
                {
                    randomValue = rnd.Next(2);
                }
                

                Vector3 spawnPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                if (randomValue == 0)
                {
                    int easyRandom = rnd.Next(3);
                    if (easyRandom <= 1)
                    {
                        Instantiate(basicEnemy, spawnPosition, spawnRotation);
                    }
                    else
                    {
                        Instantiate(dogEnemy, spawnPosition, spawnRotation);
                    }
                    currentWaveDifficultyValue = currentWaveDifficultyValue + 1;

                }
                else if(randomValue ==1)
                {
                    int medRandom = rnd.Next(3);
                    if (currentWaveDifficultyValue + 1 == roundWaveDifficulty)
                    {
                        Instantiate(basicEnemy, spawnPosition, spawnRotation);
                        currentWaveDifficultyValue = currentWaveDifficultyValue + 1;
                    }
                    else
                    {
                        if (medRandom <= 1)
                        {
                            Instantiate(mediumEnemy, spawnPosition, spawnRotation);
                        }
                        else
                        {
                            Instantiate(runnerEnemy, spawnPosition, spawnRotation);
                        }         
                        currentWaveDifficultyValue = currentWaveDifficultyValue + 2;
                    }
                }
                else if(randomValue==2)
                {
                    Instantiate(officerEnemy, spawnPosition, spawnRotation);
                    currentWaveDifficultyValue = currentWaveDifficultyValue + 4;
                }

                yield return new WaitForSeconds(spawnWait);
            }


            while (currentWaveScore != roundWaveDifficulty)
            {
                    yield return new WaitForSeconds(waveWait);
            }


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

        if (wallHeath <= 0)
        {
            healthText.text = "Health: 0/100";
            gameOver();
        }
    }

    public void increaseCurrentWaveScore(int value)
    {
        currentWaveScore = currentWaveScore + value;

        if (currentWaveScore == roundWaveDifficulty)
        {
            if (roundOverCanvas == null)
            {
                Debug.Log("canvas is null");
            }
            else
            {
                _roundOver = true;
                _player.AreControlsEnabled(false);
                StartCoroutine(endOfRoundDelay());

            };
        }
    }

    IEnumerator endOfRoundDelay()
    {
        yield return new WaitForSecondsRealtime(4);
        InitiateEndOfRound();

    }

    private void InitiateEndOfRound()
    {
        roundOverCanvas.enabled = true;
    }

    public void updateScore(int scoreValue)
    {
        score = score + scoreValue;
        scoretext.text = "Score : " + score;
    }

    public void gameOver()
    {
        _player.AreControlsEnabled(false);
        _gameOver = true;
        gameOverText.text = "Game Over";
    }

    public void StartNextWave()
    {
        _waveCount = _waveCount + 1;
        _roundOver = false;
        spawnWait = spawnWait - 0.05f;
        if(spawnWait<=0.3)
        {
            spawnWait = 0.3f;
        }
        StartCoroutine(SpawnWaves());
    }

    public void RepairBase(int value)
    {
        wallHeath = wallHeath + value;
        if(wallHeath>100)
        {
            wallHeath = 100;
        }
    }

}
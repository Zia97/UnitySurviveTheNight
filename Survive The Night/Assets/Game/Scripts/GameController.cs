using Assets.Game.Scripts;
using Assets.HeroEditor.Common.CharacterScripts;
using System;
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

    public GameObject BasicPistolPlayer;
    public GameObject BasicMP5Player;
    public GameObject ShotgunPlayer;

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

    public Canvas summaryCanvas;
    public GameObject summaryObject;

    public GameObject dropdownObject;
    public Canvas dropdownCanvas;

    public GameObject _playerGameObject;
    public PlayerControls _player;

    public GameObject _switchWeaponObject;
    private Button _switchWeaponButton;

    public GameObject _ReloadObject;
    private Button _ReloadWeaponButton;

    private ArrayList _avaliableWeapons = new ArrayList();

    private string _primaryWeapon;
    private string _secondaryWeapon;


    private Character myCharacter;

    private int _waveCount = 1;

    private bool _UIButtonClicked;

    private Vector3 _playerPos;


    private System.Random rnd = new System.Random();

    private void Start()
    {
        _avaliableWeapons.Add("USP");
        _playerGameObject = GameObject.FindWithTag("Player");
        _player = _playerGameObject.GetComponent<PlayerControls>();
        myCharacter = _playerGameObject.GetComponent<Character>();

        _switchWeaponObject = GameObject.FindWithTag("SwitchWeapon");
        _switchWeaponButton = _switchWeaponObject.GetComponent<Button>();
        _switchWeaponButton.onClick.AddListener(SwitchWeaponClicked);

        _ReloadObject = GameObject.FindWithTag("ReloadButton");
        _ReloadWeaponButton = _ReloadObject.GetComponent<Button>();
        _ReloadWeaponButton.onClick.AddListener(ReloadWeaponClicked);

        roundOverGameObject = GameObject.FindWithTag("RoundEndCanvas");
        roundOverGameObject.SetActive(true);
        roundOverCanvas = roundOverGameObject.GetComponent<Canvas>(); 
        roundOverCanvas.enabled = false;

        summaryObject = GameObject.FindWithTag("SummaryCanvas");
        summaryObject.SetActive(true);
        summaryCanvas = summaryObject.GetComponent<Canvas>();
        summaryCanvas.enabled = false;

        dropdownObject = GameObject.FindWithTag("SummaryCanvas");
        dropdownObject.SetActive(true);
        dropdownCanvas = dropdownObject.GetComponent<Canvas>();
        dropdownCanvas.enabled = false;

        scoretext.text = "Score : " + score;
        healthText.text = "Health: " + wallHeath + "/100";
        gameOverText.text = "";
        waveText.text = "Wave "+_waveCount.ToString();

        _primaryWeapon = "USP";
        _secondaryWeapon = null;

        StartCoroutine(SpawnWaves());
    }

    public int getWallHealth()
    {
        return wallHeath;
    }

    private void ReloadWeaponClicked()
    {
        StartCoroutine(GameObject.FindWithTag("Player").GetComponent<Character>().Firearm.Reload.Reload());
    }

    private void SwitchWeaponClicked()
    {
          var temp = GameObject.FindWithTag("Player");
         _playerPos = temp.gameObject.transform.position;

        Destroy(GameObject.FindWithTag("Player"));

        if (myCharacter.Firearm.Params.Name.Equals(_primaryWeapon))
        {
            if (_secondaryWeapon != null)
            {
                Instantiate(weaponNameToPrefab(_secondaryWeapon), _playerPos, Quaternion.identity);
                _playerGameObject = GameObject.FindWithTag("Player");
                _player = weaponNameToPrefab(_secondaryWeapon).GetComponent<PlayerControls>();
                myCharacter = weaponNameToPrefab(_secondaryWeapon).gameObject.GetComponent<Character>();
            }
            else
            {
                Instantiate(weaponNameToPrefab(_primaryWeapon), _playerPos, Quaternion.identity);
                _playerGameObject = GameObject.FindWithTag("Player");
                _player = weaponNameToPrefab(_primaryWeapon).GetComponent<PlayerControls>();
                myCharacter = weaponNameToPrefab(_primaryWeapon).gameObject.GetComponent<Character>();
            }


        }
        else if (myCharacter.Firearm.Params.Name.Equals(_secondaryWeapon))
        {
                 Instantiate(weaponNameToPrefab(_primaryWeapon), _playerPos, Quaternion.identity);
                _playerGameObject = GameObject.FindWithTag("Player");
                _player = weaponNameToPrefab(_primaryWeapon).GetComponent<PlayerControls>();
                myCharacter = weaponNameToPrefab(_primaryWeapon).gameObject.GetComponent<Character>();       
        }

    }

    internal void disableEndOfRoundlayer()
    {
        roundOverCanvas.enabled = false;
    }

    void Update()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            ammoText.text = (GameObject.FindWithTag("Player").GetComponent<Character>().Firearm.Params.MagazineCapacity - GameObject.FindWithTag("Player").GetComponent<Character>().Firearm.AmmoShooted) + "/" + GameObject.FindWithTag("Player").GetComponent<Character>().Firearm.Params.MagazineCapacity;
        }
    }

    IEnumerator SpawnWaves()
    {
        healthText.text = "Health: " + wallHeath + "/100";

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
                

                Vector3 spawnPosition = new Vector3(spawnValues.x, UnityEngine.Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
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
        _gameOver = true;
        gameOverText.text = "Game Over";
    }

    public void StartNextWave()
    {
        roundOverCanvas.enabled = false;
        summaryCanvas.enabled = false;
        dropdownCanvas.enabled = false;
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

    public void updateSelectedWeapons(string weapon1, string weapon2)
    {
        _primaryWeapon = weapon1;
        _secondaryWeapon = weapon2;
    }

    public void updateAllAvaliableWeapons(ArrayList allWeapons)
    {
        _avaliableWeapons = allWeapons;
    }

    public GameObject weaponNameToPrefab(string weaponName)
    {
        if(weaponName.Equals("USP"))
        {
            return BasicPistolPlayer;
        }
        if (weaponName.Equals("MP-5"))
        {
            return BasicMP5Player;
        }
        if (weaponName.Equals("Shotgun"))
        {
            return ShotgunPlayer;
        }
        return null;
    }

    public ArrayList getAllAvaliableWeapons()
    {
        return _avaliableWeapons;
    }

}
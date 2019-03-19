using Assets.Game.Scripts;
using Assets.HeroEditor.Common.CharacterScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject basicEnemy;
    public GameObject mediumEnemy;
    public GameObject officerEnemy;
    public GameObject dogEnemy;
    public GameObject runnerEnemy;
    public GameObject crawlerEnemy;

    public GameObject BasicPistolPlayer;
    public GameObject BasicMP5Player;
    public GameObject ShotgunPlayer;
    public GameObject SniperPlayer;

    public GameObject turret1;
    public GameObject turret2;
    public GameObject turret3;
    public GameObject turret6;

    private GameObject turret1Ref;
    private GameObject turret2Ref;
    private GameObject turret3Ref;
    private GameObject turret6Ref;

    private string selectedTurret1;
    private string selectedTurret2;
    private string selectedTurret3;

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

    private int _buildingMaterials = 0;

    public Text healthText;
    private int wallHeath = 100;

    public Text gameOverText;
    public Text waveText;

    public Text ammoText;

    private bool _gameOver;
    private bool _roundOver;
    private bool _beginNextRound;
    private bool wallDestoryed = false;

    public Canvas roundOverCanvas;
    public GameObject roundOverGameObject;

    public Canvas summaryCanvas;
    public GameObject summaryObject;

    public Canvas gameOverCanvas;
    public GameObject gameOverObject;

    public Canvas workshopCanvas;
    public GameObject workshopObject;
    public GameObject workshopController;

    public GameObject dropdownObject;
    public Canvas dropdownCanvas;

    public GameObject BaseWall;

    public GameObject _playerGameObject;
    public PlayerControls _player;

    public GameObject _switchWeaponObject;
    private Button _switchWeaponButton;

    public GameObject _ReloadObject;
    private Button _ReloadWeaponButton;

    private ArrayList _avaliableWeapons = new ArrayList();

    private string _primaryWeapon;
    private string _secondaryWeapon;
    private Dictionary<string, int> avaliableTurrets = new Dictionary<string, int>();

    private Character myCharacter;

    private int _waveCount = 1;

    private bool _UIButtonClicked;

    private Vector3 _playerPos;

    private bool _isPlayerDead = false;

    private System.Random rnd = new System.Random();

    private Vector3 defaultPos;


    private void Start()
    {
        defaultPos.x = -8;
        defaultPos.y = -0;
        defaultPos.z = 1;
        _avaliableWeapons.Add("USP");
        _playerGameObject = GameObject.FindWithTag("Player");
        _player = _playerGameObject.GetComponent<PlayerControls>();
        myCharacter = _playerGameObject.GetComponent<Character>();

        BaseWall = GameObject.FindWithTag("BaseWall");

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

        workshopObject = GameObject.FindWithTag("WorkshopCanvas");
        workshopObject.SetActive(true);
        workshopCanvas = workshopObject.GetComponent<Canvas>();
        workshopCanvas.enabled = false;
        workshopController = GameObject.FindWithTag("WorkshopController");
        workshopController.SetActive(true);

        summaryObject = GameObject.FindWithTag("SummaryCanvas");
        summaryObject.SetActive(true);
        summaryCanvas = summaryObject.GetComponent<Canvas>();
        summaryCanvas.enabled = false;

        dropdownObject = GameObject.FindWithTag("SummaryCanvas");
        dropdownObject.SetActive(true);
        dropdownCanvas = dropdownObject.GetComponent<Canvas>();
        dropdownCanvas.enabled = false;

        gameOverObject = GameObject.FindWithTag("GameOverController");
        gameOverObject.SetActive(true);
        gameOverCanvas.enabled = false;

        scoretext.text = "Score : " + score;
        healthText.text = "Health: " + wallHeath + "/100";
        gameOverText.text = "";
        waveText.text = "Night "+_waveCount.ToString();

        avaliableTurrets.Add("Turret 1", 0);
        avaliableTurrets.Add("Turret 2", 0);
        avaliableTurrets.Add("Turret 3", 0);
        avaliableTurrets.Add("Turret 6", 0);

        _primaryWeapon = "USP";
        _secondaryWeapon = "MP-5";
        InstantiateTurrets();
        StartCoroutine(SpawnWaves());
        var temp = GameObject.FindWithTag("Player");
        
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

        InstantiatePlayer();

    }

    private void InstantiatePlayer()
    {
        if(_playerPos==null)
        {
            _playerPos = defaultPos;
        }
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
            waveText.text = "Night " + _waveCount.ToString();
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
                    int easyRandom = rnd.Next(6);
                    if (easyRandom <= 1)
                    {
                        Instantiate(basicEnemy, spawnPosition, spawnRotation);
                    }
                    else if(easyRandom >= 2 && easyRandom <=4 )
                    {
                        Instantiate(dogEnemy, spawnPosition, spawnRotation);
                    }
                    else
                    {
                        Instantiate(crawlerEnemy, spawnPosition, spawnRotation);
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

    public void PlayerDead()
    {
        _isPlayerDead = true;
    }

    public bool isPlayerDead()
    {
        return _isPlayerDead;
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
            wallDestoryed = true;
            Destroy(BaseWall);
            gameOver();
        }
    }

    public bool isWallDestroyed()
    {
        return wallDestoryed;
    }

    public Vector3 getPlayerPosition()
    {
        return GameObject.FindWithTag("Player").gameObject.transform.position;
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

    IEnumerator endOfGameDelay()
    {
        yield return new WaitForSecondsRealtime(4);
        gameOverCanvas.enabled = true;
    }

    IEnumerator endOfRoundDelay()
    {
        yield return new WaitForSecondsRealtime(4);
        InitiateEndOfRound();
    }

    private void InitiateEndOfRound()
    {
        Destroy(GameObject.FindWithTag("Player"));
        DestroyTurrets();
        roundOverCanvas.enabled = true;
    }

    public void updateScore(int scoreValue)
    {
        score = score + scoreValue;
        scoretext.text = "Score : " + score;
    }

    public void addBuildingMaterials(int noOfMaterials)
    {
        _buildingMaterials = _buildingMaterials + noOfMaterials;
        workshopController.GetComponent<WorkshopController>().setNumberOfAvaliableMaterials(_buildingMaterials);
    }

    public void updateBuildingMaterials(int noOfMaterials)
    {
        _buildingMaterials = noOfMaterials;
    }

    public void gameOver()
    {
        _gameOver = true;
        gameOverText.text = "Game Over";
        gameOverObject.GetComponent<GameOverController>().updateWaveReachedAndScore(_waveCount-1, score);
        StartCoroutine(endOfGameDelay());
    }

    public void StartNextWave()
    {
        roundOverCanvas.enabled = false;
        summaryCanvas.enabled = false;
        dropdownCanvas.enabled = false;

        InstantiateTurrets();

        Instantiate(weaponNameToPrefab(_primaryWeapon), defaultPos, Quaternion.identity);
        _playerGameObject = GameObject.FindWithTag("Player");
        _player = weaponNameToPrefab(_primaryWeapon).GetComponent<PlayerControls>();
        myCharacter = weaponNameToPrefab(_primaryWeapon).gameObject.GetComponent<Character>();

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
        if (weaponName.Equals("Scout"))
        {
            return SniperPlayer;
        }
        return null;
    }


    public GameObject turretToPrefab(string weaponName)
    {
        if (weaponName.Equals("Turret 1"))
        {
            return BasicPistolPlayer;
        }
        if (weaponName.Equals("Turret 2"))
        {
            return BasicMP5Player;
        }
        if (weaponName.Equals("Turret 3"))
        {
            return ShotgunPlayer;
        }
        if (weaponName.Equals("Turret 6"))
        {
            return SniperPlayer;
        }
        return null;
    }

    public Dictionary<string,int> getTurrets()
    {
        return avaliableTurrets;
    }

    public void addTurret(string turret)
    {
        if(avaliableTurrets.ContainsKey(turret))
        {
            int oldValue = avaliableTurrets[turret];
            avaliableTurrets[turret] = oldValue + 1;
            Debug.Log(turret + " : " + avaliableTurrets[turret]);
        }
    }

    public ArrayList getAllAvaliableWeapons()
    {
        return _avaliableWeapons;
    }

    public void selectTurrets(string t1, string t2, string t3)
    {
        selectedTurret1 = t1;
        selectedTurret2 = t2;
        selectedTurret3 = t3;
    }

    public void InstantiateTurrets()
    {
        turret1Ref = turret1;
        turret2Ref = turret2;
        turret3Ref = turret3;
        turret6Ref = turret6;

        Vector3 topTurret = new Vector3(-355, 140, -40);
        Vector3 centerTurret = new Vector3(-355, 0, -40);
        Vector3 bottomTurret = new Vector3(-355, -140, -40);

        var panel = GameObject.Find("MainGamePanel");
        if (panel != null) 
        {
            GameObject a = Instantiate(turret1Ref, topTurret, Quaternion.identity);
            a.transform.SetParent(panel.transform, false);

            GameObject b = Instantiate(turret2Ref, centerTurret, Quaternion.identity);
            b.transform.SetParent(panel.transform, false);

            GameObject c = Instantiate(turret3Ref, bottomTurret, Quaternion.identity);
            c.transform.SetParent(panel.transform, false);
        }

    }

    public void DestroyTurrets()
    {
        Destroy(GameObject.FindWithTag("Turret1"));
        Destroy(GameObject.FindWithTag("Turret2"));
        Destroy(GameObject.FindWithTag("Turret3"));
        Destroy(GameObject.FindWithTag("Turret6"));
    }

}
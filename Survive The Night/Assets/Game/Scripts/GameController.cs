//Author : Qasim Ziauddin

//The main game controller that is responsible for co-ordinating each waves. Loads and deletes assets such as NPC's and turrets.
//Handles enemy generation. Also handles UI updates.
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
    public GameObject GoldenAKPlayer;
    public GameObject M249Player;
    public GameObject RocketLauncherPlayer;
    public GameObject RPGPlayer;
    public GameObject SPAS12Player;
    public GameObject SRLPlayer;
    public GameObject RevolverPlayer;
    public GameObject M4LaserPlayer;


    public GameObject S1USP;
    public GameObject S1MP5;
    public GameObject S1Shotgun;
    public GameObject S1Scout;

    public GameObject S2USP;
    public GameObject S2MP5;
    public GameObject S2Shotgun;
    public GameObject S2Scout;

    public GameObject S3USP;
    public GameObject S3MP5;
    public GameObject S3Shotgun;
    public GameObject S3Scout;

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

    private string selectedNPC1;
    private string selectedNPC2;
    private string selectedNPC3;

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
    private double wallHeath = 100;

    public Text gameOverText;
    public Text waveText;

    public Text ammoText;

    public Text roundText;

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

    public Canvas armoryCanvas;
    public GameObject armoryObject;
    public GameObject armoryController;

    public GameObject dropdownObject;
    public Canvas dropdownCanvas;

    public GameObject PausedCanvasObject;
    Canvas PausedCanvas;

    public GameObject BaseWall;

    public GameObject _playerGameObject;
    public PlayerControls _player;

    public GameObject _switchWeaponObject;
    private Button _switchWeaponButton;

    public GameObject _pauseButtonObject;
    private Button _pauseButton;

    public GameObject _resumeButtonObject;
    private Button _resumeButton;

    public GameObject _ReloadObject;
    private Button _ReloadWeaponButton;

    public GameObject BlurCanvasObject;
    Canvas BlurCanvas;

    private Dictionary<string, int> _avaliableWeapons = new Dictionary<string, int>();
    private ArrayList _turretList = new ArrayList();

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

    private int noOfNPCS = 0;

    public AudioSource mainGameMusic;
    public AudioSource breakMusic;


    private void Start()
    {
        breakMusic.Stop();
        _turretList.Add(selectedTurret1);
        _turretList.Add(selectedTurret2);
        _turretList.Add(selectedTurret3);

        defaultPos.x = -8;
        defaultPos.y = -0;
        defaultPos.z = 1;

        AddOwnedWeapons();
        _avaliableWeapons.Add("USP", 5);
        _primaryWeapon = "USP";
        _secondaryWeapon = "USP";

        if (!UserProfile.getPrimaryWeapon().Equals(""))
        {
            _primaryWeapon = UserProfile.getPrimaryWeapon();
        }

        if (!UserProfile.getSecondaryWeapon().Equals(""))
        {
            _secondaryWeapon = UserProfile.getSecondaryWeapon();
        }

        #region Instantiate Player
        Instantiate(weaponNameToPrefab(_primaryWeapon), defaultPos, Quaternion.identity);
        _playerGameObject = GameObject.FindWithTag("Player");
        _player = weaponNameToPrefab(_primaryWeapon).GetComponent<PlayerControls>();
        myCharacter = weaponNameToPrefab(_primaryWeapon).gameObject.GetComponent<Character>();
        #endregion

        BaseWall = GameObject.FindWithTag("BaseWall");

        #region Find Objects

        BlurCanvasObject = GameObject.Find("BlurCanvas");
        BlurCanvas = BlurCanvasObject.GetComponent<Canvas>();
        BlurCanvas.enabled = false;

        PausedCanvasObject = GameObject.Find("PausedCanvas");
        PausedCanvas = PausedCanvasObject.GetComponent<Canvas>();
        PausedCanvas.enabled = false;

        _switchWeaponObject = GameObject.FindWithTag("SwitchWeapon");
        _switchWeaponButton = _switchWeaponObject.GetComponent<Button>();
        _switchWeaponButton.onClick.AddListener(SwitchWeaponClicked);

       // _resumeButtonObject = GameObject.FindWithTag("GameResumeButton");
        _resumeButton = _resumeButtonObject.GetComponent<Button>();
        _resumeButton.onClick.AddListener(ResumeButtonClicked);

        _pauseButtonObject = GameObject.FindWithTag("PauseButton");
        _pauseButton = _pauseButtonObject.GetComponent<Button>();
        _pauseButton.onClick.AddListener(PauseButtonClicked);

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

        armoryObject = GameObject.FindWithTag("NPCCanvas");
        armoryObject.SetActive(true);
        armoryCanvas = armoryObject.GetComponent<Canvas>();
        armoryCanvas.enabled = false;
        armoryController = GameObject.FindWithTag("ArmoryController");
        armoryController.SetActive(true);

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

        #endregion

        scoretext.text = "Score : " + score;
        healthText.text = "Health: " + wallHeath + "/100";
        gameOverText.text = "";
        waveText.text = "Night " + _waveCount.ToString();

        avaliableTurrets.Add("Turret 1", 0);
        avaliableTurrets.Add("Turret 2", 0);
        avaliableTurrets.Add("Turret 3", 0);
        avaliableTurrets.Add("Turret 6", 0);

        var S1Cross = GameObject.FindWithTag("S1Cross");
        var S2Cross = GameObject.FindWithTag("S2Cross");
        var S3Cross = GameObject.FindWithTag("S3Cross");

        S1Cross.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        S2Cross.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        S3Cross.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        InstantiateTurrets();


        StartCoroutine(SpawnWaves());
        updateRoundText();

        var temp = GameObject.FindWithTag("Player");

    }

  

    //Check for all purchasable weapons 
    private void AddOwnedWeapons()
    {
        if (PlayerPrefs.HasKey("MP5"))
        {
            _avaliableWeapons.Add("MP-5", 1);
        }
        if (PlayerPrefs.HasKey("Scout"))
        {
            _avaliableWeapons.Add("Scout", 1);
        }
        if (PlayerPrefs.HasKey("Shotgun"))
        {
            _avaliableWeapons.Add("Shotgun", 1);
        }
        if (PlayerPrefs.HasKey("GoldenAK"))
        {
            _avaliableWeapons.Add("AK-47 [Golden]", 1);
        }
        if (PlayerPrefs.HasKey("M249"))
        {
            _avaliableWeapons.Add("M-249", 1);
        }
        if (PlayerPrefs.HasKey("M4Laser"))
        {
            _avaliableWeapons.Add("M-4Laser", 1);
        }
        if (PlayerPrefs.HasKey("RocketLauncher"))
        {
            _avaliableWeapons.Add("RocketLauncher", 1);
        }
        if (PlayerPrefs.HasKey("RPG"))
        {
            _avaliableWeapons.Add("RPG", 1);
        }
        if (PlayerPrefs.HasKey("SPAS12"))
        {
            _avaliableWeapons.Add("SPAS-12", 1);
        }
        if (PlayerPrefs.HasKey("SRL"))
        {
            _avaliableWeapons.Add("SRL", 1);
        }
        if (PlayerPrefs.HasKey("Revolver"))
        {
            _avaliableWeapons.Add("Revolver", 1);
        }
    }

    public double getWallHealth()
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

    private void ResumeButtonClicked()
    {
        if (Pause.isPaused)
        {
            Time.timeScale = 1;
            Pause.isPaused = false;
            BlurCanvas.enabled = false;
            PausedCanvas.enabled = false;
        }
    }

    private void PauseButtonClicked()
    {
        if (Pause.isPaused)
        {
            Time.timeScale = 1;
            Pause.isPaused = false;
            BlurCanvas.enabled = false;
            PausedCanvas.enabled = false;
        }
        else if(!Pause.isPaused)
        {
            Time.timeScale = 0;
            Pause.isPaused = true;
            BlurCanvas.enabled = true;
            PausedCanvas.enabled = true;
        }
        
    }

    private void InstantiatePlayer()
    {
        if (_playerPos == null)
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
        else
        {
            Debug.Log("EOERWERWERWETJGKIJWE");
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
        updateRoundText();
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

                if (roundWaveDifficulty - currentWaveDifficultyValue >= 4)
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
                    else if (easyRandom >= 2 && easyRandom <= 4)
                    {
                        Instantiate(dogEnemy, spawnPosition, spawnRotation);
                    }
                    else
                    {
                        Instantiate(crawlerEnemy, spawnPosition, spawnRotation);
                    }
                    currentWaveDifficultyValue = currentWaveDifficultyValue + 1;

                }
                else if (randomValue == 1)
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
                else if (randomValue == 2)
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

    public void damageWall(double damage)
    {
        if (!_gameOver)
        {
            wallHeath = wallHeath - damage;
            healthText.text = "Health: " + wallHeath + "/100";
        }

        if (wallHeath <= 100 && wallHeath >= 65)
        {
            healthText.color = Color.green;
        }
        if (wallHeath <= 64 && wallHeath >= 35)
        {
            healthText.color = Color.yellow;
        }
        if (wallHeath <= 34)
        {
            healthText.color = Color.red;
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
        waveText.text = "Wave Cleared";
        yield return new WaitForSecondsRealtime(4);
        InitiateEndOfRound();
    }

    private void InitiateEndOfRound()
    {
        Destroy(GameObject.FindWithTag("Player"));
        DestroyTurrets();
        DestroyNPCS();
        DestroyDeadEnemies();
        roundOverCanvas.enabled = true;
        mainGameMusic.Stop();
        breakMusic.PlayDelayed(2);
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
        gameOverObject.GetComponent<GameOverController>().updateWaveReachedAndScore(_waveCount - 1, score);
        StartCoroutine(endOfGameDelay());
    }

    public void StartNextWave()
    {
        breakMusic.Stop();
        mainGameMusic.Play();
        roundOverCanvas.enabled = false;
        summaryCanvas.enabled = false;
        dropdownCanvas.enabled = false;

        InstantiateTurrets();
        InstantiateNPCS();

        Instantiate(weaponNameToPrefab(_primaryWeapon), defaultPos, Quaternion.identity);
        _playerGameObject = GameObject.FindWithTag("Player");
        _player = weaponNameToPrefab(_primaryWeapon).GetComponent<PlayerControls>();
        myCharacter = weaponNameToPrefab(_primaryWeapon).gameObject.GetComponent<Character>();

        _waveCount = _waveCount + 1;
        _roundOver = false;
        spawnWait = spawnWait - 0.05f;
        if (spawnWait <= 0)
        {
            spawnWait = 0f;
        }
        StartCoroutine(SpawnWaves());
    }

    public void RepairBase(int value)
    {
        wallHeath = wallHeath + value;
        if (wallHeath > 100)
        {
            wallHeath = 100;
        }
    }

    private void updateRoundText()
    {
        roundText.text = "Night " + _waveCount.ToString();
    }

    public void updateSelectedWeapons(string weapon1, string weapon2)
    {
        _primaryWeapon = weapon1;
        _secondaryWeapon = weapon2;
    }

    public void updateAllAvaliableWeapons(Dictionary<string, int> allWeapons)
    {
        _avaliableWeapons = allWeapons;
    }

    public GameObject weaponNameToPrefab(string weaponName)
    {
        if (weaponName == null)
        {
            return null;
        }

        if (weaponName.Equals("USP"))
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
        if (weaponName.Equals("AK-47 [Golden]"))
        {
            return GoldenAKPlayer;
        }

        if (weaponName.Equals("Revolver"))
        {
            return RevolverPlayer;
        }

        if (weaponName.Equals("M-4Laser"))
        {
            return M4LaserPlayer;
        }

        if (weaponName.Equals("SPAS-12"))
        {
            return SPAS12Player;
        }

        if (weaponName.Equals("M-249"))
        {
            return M249Player;
        }

        if (weaponName.Equals("SRL"))
        {
            return SRLPlayer;
        }

        if (weaponName.Equals("RPG"))
        {
            return RPGPlayer;
        }

        if (weaponName.Equals("RocketLauncher"))
        {
            return RocketLauncherPlayer;
        }
        return null;
    }


    public GameObject turretToPrefab(string selectedTurret)
    {
        if (selectedTurret == null)
        {
            return null;
        }
        if (selectedTurret.Equals("Basic Turret"))
        {
            return turret1;
        }
        if (selectedTurret.Equals("Medium Turret"))
        {
            return turret2;
        }
        if (selectedTurret.Equals("Heavy Turret"))
        {
            return turret3;
        }
        if (selectedTurret.Equals("Super Turret"))
        {
            return turret6;
        }
        return null;
    }

    public Dictionary<string, int> getTurrets()
    {
        return avaliableTurrets;
    }

    public void addTurret(string turret)
    {
        if (avaliableTurrets.ContainsKey(turret))
        {
            int oldValue = avaliableTurrets[turret];
            avaliableTurrets[turret] = oldValue + 1;
        }
    }

    public Dictionary<string, int> getAllAvaliableWeapons()
    {
        return _avaliableWeapons;
    }

    public void InstantiateNPCS()
    {
        Vector3 pos1 = new Vector3(-6.4f, 2.4f, 0);
        Vector3 pos2 = new Vector3(-6.4f, 0.5f, 0);
        Vector3 pos3 = new Vector3(-6.4f, -3f, 0);

        var topNPC = npcToPrefab(selectedNPC1);
        var midNPC = npcToPrefab(selectedNPC2);
        var botNPC = npcToPrefab(selectedNPC3);

        if (topNPC != null)
        {
            topNPC.GetComponent<WeaponControls>().isNPC();
            topNPC.GetComponent<WeaponControls>().setLocation("Top");
            Instantiate(topNPC, pos1, Quaternion.identity);


        }

        if (midNPC != null)
        {
            midNPC.GetComponent<WeaponControls>().isNPC();
            midNPC.GetComponent<WeaponControls>().setLocation("Mid");
            Instantiate(midNPC, pos2, Quaternion.identity);

        }

        if (botNPC != null)
        {
            botNPC.GetComponent<WeaponControls>().isNPC();
            botNPC.GetComponent<WeaponControls>().setLocation("Bot");
            Instantiate(botNPC, pos3, Quaternion.identity);

        }


    }

    public void selectTurrets(string t1, string t2, string t3)
    {
        selectedTurret1 = t1;
        selectedTurret2 = t2;
        selectedTurret3 = t3;
    }

    public void InstantiateTurrets()
    {
        var topTurret = turretToPrefab(selectedTurret1);
        var centreTurret = turretToPrefab(selectedTurret2);
        var bottomTurret = turretToPrefab(selectedTurret3);

        Vector3 topTurretLoc = new Vector3(-355, 140, -40);
        Vector3 centerTurretLoc = new Vector3(-355, 0, -40);
        Vector3 bottomTurretLoc = new Vector3(-355, -140, -40);

        var panel = GameObject.Find("MainGamePanel");
        if (panel != null)
        {
            if (topTurret != null)
            {
                GameObject a = Instantiate(topTurret, topTurretLoc, Quaternion.identity);
                a.transform.SetParent(panel.transform, false);
                a.transform.localScale = new Vector3(17, 17, 17);
                topTurret.GetComponent<AnimatedExampleWeapon>().SetState(ExampleWeapon.State.Waiting);
            }

            if (centreTurret != null)
            {
                GameObject b = Instantiate(centreTurret, centerTurretLoc, Quaternion.identity);
                b.transform.SetParent(panel.transform, false);
                b.transform.localScale = new Vector3(17, 17, 17);
                centreTurret.GetComponent<AnimatedExampleWeapon>().SetState(ExampleWeapon.State.Waiting);
            }

            if (bottomTurret != null)
            {
                GameObject c = Instantiate(bottomTurret, bottomTurretLoc, Quaternion.identity);
                c.transform.SetParent(panel.transform, false);
                c.transform.localScale = new Vector3(17, 17, 17);
                bottomTurret.GetComponent<AnimatedExampleWeapon>().SetState(ExampleWeapon.State.Waiting);
            }

        }

    }

    public void DestroyTurrets()
    {
        var allTurret1 = GameObject.FindGameObjectsWithTag("Turret1");
        var allTurret2 = GameObject.FindGameObjectsWithTag("Turret2");
        var allTurret3 = GameObject.FindGameObjectsWithTag("Turret3");
        var allTurret4 = GameObject.FindGameObjectsWithTag("Turret6");

        for (var i = 0; i < allTurret1.Length; i++)
        {
            Destroy(allTurret1[i]);
        }

        for (var i = 0; i < allTurret2.Length; i++)
        {
            Destroy(allTurret2[i]);
        }

        for (var i = 0; i < allTurret3.Length; i++)
        {
            Destroy(allTurret3[i]);
        }

        for (var i = 0; i < allTurret4.Length; i++)
        {
            Destroy(allTurret4[i]);
        }
    }

    public void DestroyNPCS()
    {
        var allNPCS = GameObject.FindGameObjectsWithTag("NPC");

        for (var i = 0; i < allNPCS.Length; i++)
        {
            Destroy(allNPCS[i]);
        }
    }

    public void DestroyDeadEnemies()
    {
        var allenemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (var i = 0; i < allenemies.Length; i++)
        {
            Destroy(allenemies[i]);
        }
    }

    public void selectNPCs(string t1, string t2, string t3)
    {
        selectedNPC1 = t1;
        selectedNPC2 = t2;
        selectedNPC3 = t3;
    }

    public string getPrimary()
    {
        return _primaryWeapon;
    }

    public string getSecondary()
    {
        return _secondaryWeapon;
    }

    public GameObject npcToPrefab(string npc)
    {
        if (npc == null)
        {
            return null;
        }

        if (npc.Equals("S1USP"))
        {
            return S1USP;
        }
        if (npc.Equals("S2USP"))
        {
            return S2USP;
        }
        if (npc.Equals("S3USP"))
        {
            return S3USP;
        }

        if (npc.Equals("S1MP-5"))
        {
            return S1MP5;
        }
        if (npc.Equals("S2MP-5"))
        {
            return S2MP5;
        }
        if (npc.Equals("S3MP-5"))
        {
            return S3MP5;
        }

        if (npc.Equals("S1Shotgun"))
        {
            return S1Shotgun;
        }
        if (npc.Equals("S2Shotgun"))
        {
            return S2Shotgun;
        }
        if (npc.Equals("S3Shotgun"))
        {
            return S3Shotgun;
        }

        if (npc.Equals("S1Scout"))
        {
            return S1Scout;
        }
        if (npc.Equals("S2Scout"))
        {
            return S2Scout;
        }
        if (npc.Equals("S3Scout"))
        {
            return S3Scout;
        }

        return null;
    }

    public void setNoOfNPCS(int value)
    {
        noOfNPCS = value;
    }

    public int getNoOfNPCS()
    {
        return noOfNPCS;
    }

}
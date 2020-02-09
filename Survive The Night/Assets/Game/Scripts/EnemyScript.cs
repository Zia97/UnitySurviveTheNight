//Author : Qasim Ziauddin

//Superclass enemy script which is intended to be extended by futher subclassess. Provides the basic framework for each enemy,
//providing movement, bullet collision, animations, death etc
using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private float _speed = 3.0f;
    private bool isMovingTowardsBase = true;
    private bool isMovingTowardsPlayer = false;
    private double _health = 100;
    protected GameController gameController;
    private double _wallDamagetick = 1;
    private float _wallDamageFrequency = 1f;
    private bool randomDrop;
    private int _dropProbability = 2;
    private int _scoreValue = 1;
    private bool isDead = false;
    private bool reachedPlayer = false;
    private bool collidedWithPlayer = false;
    private int minMaterials = 0;
    private int maxMaterials = 0;
    private int noOfMaterials = 0;
    private double difficultyMultiplier = 1;
    Rigidbody2D rb;

    public AudioSource audioSource;

    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;
    public AudioClip music4;
    public AudioClip music5;
    public AudioClip music6;
    public AudioClip music7;
    public AudioClip music8;
    public AudioClip music9;
    public AudioClip music10;

    private int randomMusic = 0;
    private float randomTime = 2f;
    private float timeCounter = 0f;

    private GameObject _gameController;




    private System.Random rnd = new System.Random();

    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        GetComponent<Collider2D>().isTrigger = true;
        _gameController = GameObject.FindWithTag("GameController");
        if (_gameController != null)
        {
            gameController = _gameController.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (gameController == null)
        {
            this.gameObject.GetComponent<Animator>().Play("walkSide");
            transform.position += -transform.right * _speed * Time.deltaTime;
            Destroy(gameObject, 25);
        }
        else if (gameController.isPlayerDead())
        {
            if (collidedWithPlayer)
            {
                gameObject.GetComponent<Animator>().Play("strike");
            }
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        else if (!isDead)
        {
            if (isMovingTowardsBase)
            {
                this.gameObject.GetComponent<Animator>().Play("walkSide");
                transform.position += -transform.right * _speed * Time.deltaTime;
            }
            else if (isMovingTowardsPlayer)
            {
                this.gameObject.GetComponent<Animator>().Play("walkSide");
                transform.position += gameController.getPlayerPosition() * _speed / 10 * Time.deltaTime;
            }
            else if (reachedPlayer)
            {
                gameObject.GetComponent<Animator>().Play("strike");
            }
            else
            {
                gameObject.GetComponent<Animator>().Play("strike");
                if (gameController.isWallDestroyed())
                {
                    isMovingTowardsPlayer = true;
                }
            }
        }
        else if (isDead)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }


        if (timeCounter > randomTime)
        {
            randomTime = Random.Range(0, 5);
            timeCounter = 0f;
            ChooseMusic();
            GetComponent<AudioSource>().Play();
        }

        timeCounter += Time.deltaTime;
    }

    protected void createDropProbability()
    {
        if (rnd.Next(11) <= _dropProbability)
        {
            randomDrop = true;
            noOfMaterials = rnd.Next(minMaterials, maxMaterials);
        }
    }

    protected void setHealth(int health)
    {
        _health = DifficultySelector.getDifficulty() * health;
    }

    protected void setSpeed(float speed)
    {
        _speed = (float)DifficultySelector.getDifficulty() * speed;
    }

    protected void setDamage(int damage)
    {
        _wallDamagetick = DifficultySelector.getDifficulty() * damage;
    }

    protected void setDamageFrequency(float frequency)
    {
        _wallDamageFrequency = frequency;
    }

    protected void setDropProbability(int probability)
    {
        _dropProbability = probability;
    }

    protected void setScoreValue(int _score)
    {
        _scoreValue = _score;
    }

    IEnumerator deathDelay()
    {
        yield return new WaitForSecondsRealtime(4);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
    }

    IEnumerator flashRed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 255f);
        yield return new WaitForSecondsRealtime(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.name == "USPBullet" || collision.transform.gameObject.name == "USPBullet(Clone)")
        {
            if (collision.gameObject.GetComponent<Projectile>().getHealth() > 0)
            {
                bulletCollisionCommon(20);
            }
            collision.gameObject.GetComponent<Projectile>().Bang(gameObject, collision.gameObject);
            StartCoroutine(flashRed());
        }

        else if (collision.transform.gameObject.name == "MP-5Bullet" || collision.transform.gameObject.name == "MP-5Bullet(Clone)")
        {
            if (collision.gameObject.GetComponent<Projectile>().getHealth() > 0)
            {
                bulletCollisionCommon(15);
            }
            collision.gameObject.GetComponent<Projectile>().Bang(gameObject, collision.gameObject);
            StartCoroutine(flashRed());
        }

        else if (collision.transform.gameObject.name == "ShotgunBullet" || collision.transform.gameObject.name == "ShotgunBullet(Clone)")
        {
            if (collision.gameObject.GetComponent<Projectile>().getHealth() > 0)
            {
                bulletCollisionCommon(15);
            }
            collision.gameObject.GetComponent<Projectile>().Bang(gameObject, collision.gameObject);
            StartCoroutine(flashRed());
        }

        else if (collision.transform.gameObject.name == "AK-47Bullet" || collision.transform.gameObject.name == "AK-47Bullet(Clone)")
        {
            if (collision.gameObject.GetComponent<Projectile>().getHealth() > 0)
            {
                bulletCollisionCommon(35);
            }
            collision.gameObject.GetComponent<Projectile>().Bang(gameObject, collision.gameObject);
            StartCoroutine(flashRed());
        }

        else if (collision.transform.gameObject.name == "RevolverBullet" || collision.transform.gameObject.name == "RevolverBullet(Clone)")
        {
            if (collision.gameObject.GetComponent<Projectile>().getHealth() > 0)
            {
                bulletCollisionCommon(35);
            }
            collision.gameObject.GetComponent<Projectile>().Bang(gameObject, collision.gameObject);
            StartCoroutine(flashRed());
        }

        else if (collision.transform.gameObject.name == "M-4LaserBullet" || collision.transform.gameObject.name == "M-4LaserBullet(Clone)")
        {
            if (collision.gameObject.GetComponent<Projectile>().getHealth() > 0)
            {
                bulletCollisionCommon(33);
            }
            collision.gameObject.GetComponent<Projectile>().Bang(gameObject, collision.gameObject);
            StartCoroutine(flashRed());
        }

        else if (collision.transform.gameObject.name == "SPAS-12Bullet" || collision.transform.gameObject.name == "SPAS-12Bullet(Clone)")
        {
            if (collision.gameObject.GetComponent<Projectile>().getHealth() > 0)
            {
                bulletCollisionCommon(25);
            }
            collision.gameObject.GetComponent<Projectile>().Bang(gameObject, collision.gameObject);
            StartCoroutine(flashRed());
        }

        else if (collision.transform.gameObject.name == "M-249Bullet" || collision.transform.gameObject.name == "M-249Bullet(Clone)")
        {
            if (collision.gameObject.GetComponent<Projectile>().getHealth() > 0)
            {
                bulletCollisionCommon(25);
            }
            collision.gameObject.GetComponent<Projectile>().Bang(gameObject, collision.gameObject);
            StartCoroutine(flashRed());
        }

        else if (collision.transform.gameObject.name == "TurretBullet" || collision.transform.gameObject.name == "TurretBullet(Clone)")
        {
            bulletCollisionCommon(15);
            StartCoroutine(flashRed());
        }

        else if (collision.transform.gameObject.name == "SniperBullet" || collision.transform.gameObject.name == "SniperBullet(Clone)")
        {
            bulletCollisionCommon(150);
            StartCoroutine(flashRed());
        }

        else if (collision.transform.gameObject.name == "SRLBullet" || collision.transform.gameObject.name == "SRLBullet(Clone)")
        {
            bulletCollisionCommon(70);
            collision.gameObject.GetComponent<Projectile>().Bang(gameObject, collision.gameObject);
            StartCoroutine(flashRed());
        }

        else if (collision.transform.gameObject.name == "RPGBullet" || collision.transform.gameObject.name == "RPGBullet(Clone)")
        {
            bulletCollisionCommon(200);
            collision.gameObject.GetComponent<Projectile>().Bang(gameObject, collision.gameObject);
            StartCoroutine(flashRed());
        }

        else if (collision.transform.gameObject.name == "RocketLauncherBullet" || collision.transform.gameObject.name == "RocketLauncherBullet(Clone)")
        {
            bulletCollisionCommon(140);
            collision.gameObject.GetComponent<Projectile>().Bang(gameObject, collision.gameObject);
            StartCoroutine(flashRed());
        }
    }

    void bulletCollisionCommon(int bulletDamage)
    {
        if (!isDead)
        {
            _health = _health - bulletDamage;

            if (_health <= 0)
            {
                isDead = true;
                gameController.updateScore(_scoreValue);
                gameController.increaseCurrentWaveScore(_scoreValue);

                gameObject.GetComponent<Animator>().Play("die");
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 255f);

                Destroy(gameObject, 3);

                if (randomDrop)
                {
                    gameController.addBuildingMaterials(noOfMaterials);
                }
            }
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.name == "wall")
        {
            isMovingTowardsBase = false;
            StartCoroutine("beginWallDamage");
            beginWallDamage();
        }

        else if (collision.gameObject.name == "BasicPlayer" || collision.gameObject.name == "MP5Player" || collision.gameObject.name == "ShotgunPlayer" || collision.gameObject.name == "SniperPlayer")
        {
            gameObject.GetComponent<Animator>().Play("strike");
            collidedWithPlayer = true;
            isMovingTowardsPlayer = false;
            isMovingTowardsBase = false;
            reachedPlayer = true;
            gameController.PlayerDead();
        }
    }

    protected void updateMaterialRange(int min, int max)
    {
        minMaterials = min;
        maxMaterials = max;
    }

    private IEnumerator beginWallDamage()
    {
        while (true)
        {
            if (!isDead)
            {
                if (!gameController.isWallDestroyed())
                {
                    gameController.damageWall(_wallDamagetick);
                }
            }
            yield return new WaitForSeconds(_wallDamageFrequency);
        }
    }

    private void ChooseMusic()
    {
        randomMusic = Random.Range(1, 11);

        switch (randomMusic)
        {
            case 1:
                GetComponent<AudioSource>().clip = music1;
                break;
            case 2:
                GetComponent<AudioSource>().clip = music2;
                break;
            case 3:
                GetComponent<AudioSource>().clip = music3;
                break;
            case 4:
                GetComponent<AudioSource>().clip = music4;
                break;
            case 5:
                GetComponent<AudioSource>().clip = music5;
                break;
            case 6:
                GetComponent<AudioSource>().clip = music6;
                break;
            case 7:
                GetComponent<AudioSource>().clip = music7;
                break;
            case 8:
                GetComponent<AudioSource>().clip = music8;
                break;
            case 9:
                GetComponent<AudioSource>().clip = music9;
                break;
            case 10:
                GetComponent<AudioSource>().clip = music10;
                break;
        }
    }

}

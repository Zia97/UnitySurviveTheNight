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

    private GameObject _gameController;
    
    private System.Random rnd = new System.Random();

    // Use this for initialization
    void Start()
    {
        difficultyMultiplier = DifficultySelector.getDifficulty();
        if(difficultyMultiplier<1)
        {
            difficultyMultiplier = 1;
        }
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
        if(gameController==null)
        {
            this.gameObject.GetComponent<Animator>().Play("walkSide");
            transform.position += -transform.right * _speed * Time.deltaTime;
            Destroy(gameObject, 25);
        }
        else if(gameController.isPlayerDead())
        {
            if(collidedWithPlayer)
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
            else if(isMovingTowardsPlayer)
            {
                this.gameObject.GetComponent<Animator>().Play("walkSide");
                transform.position += gameController.getPlayerPosition() * _speed/10 * Time.deltaTime;
            }
            else if(reachedPlayer)
            {
                gameObject.GetComponent<Animator>().Play("strike");
            }
            else
            {
                gameObject.GetComponent<Animator>().Play("strike");
                if(gameController.isWallDestroyed())
                {
                    isMovingTowardsPlayer = true;
                }
            }
        }
        else if(isDead)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    protected void createDropProbability()
    {
        if (rnd.Next(11) <= _dropProbability)
        {
            randomDrop = true;
            noOfMaterials = rnd.Next(minMaterials,maxMaterials);
        }
    }

    protected void setHealth(int health)
    {
        
        _health = difficultyMultiplier * health;
    }

    protected void setSpeed(float speed)
    {
        _speed = (float)difficultyMultiplier * speed;
    }

    protected void setDamage(int damage)
    {
        _wallDamagetick = difficultyMultiplier * damage;
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

        Debug.Log("color scheme set");
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.name == "Bullet" || collision.transform.gameObject.name == "Bullet(Clone)")
        { 
            if (collision.gameObject.GetComponent<Projectile>().getHealth() > 0)
            {
                if (!isDead)
                {
                    _health = _health - 30;

                    if (_health <= 0)
                    {
                        isDead = true;
                        gameController.updateScore(_scoreValue);
                        gameController.increaseCurrentWaveScore(_scoreValue);

                        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 0f ,0f, 255f);
                        gameObject.GetComponent<Animator>().Play("die");

                        Destroy(gameObject, 3);

                        if (randomDrop)
                        {
                            gameController.addBuildingMaterials(noOfMaterials);
                        }
                    }
                }
            }
            collision.gameObject.GetComponent<Projectile>().Bang(gameObject, collision.gameObject);
        }
        else if (collision.transform.gameObject.name == "SniperBullet" || collision.transform.gameObject.name == "SniperBullet(Clone)")
        {
            if (!isDead)
            {
                _health = _health - 150;

                if (_health <= 0)
                {
                    isDead = true;
                    gameController.updateScore(_scoreValue);
                    gameController.increaseCurrentWaveScore(_scoreValue);

                    gameObject.GetComponent<Animator>().Play("die");
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 255f);

                    if (randomDrop)
                    {
                        gameController.addBuildingMaterials(noOfMaterials);
                    }
                }
            }
        }
        else if (collision.transform.gameObject.name == "TurretBullet" || collision.transform.gameObject.name == "TurretBullet(Clone)")
        {
            if (!isDead)
            {
                _health = _health - 20;

                if (_health <= 0)
                {
                    isDead = true;
                    gameController.updateScore(_scoreValue);
                    gameController.increaseCurrentWaveScore(_scoreValue);

                    gameObject.GetComponent<Animator>().Play("die");
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 255f);

                    if (randomDrop)
                    {
                        gameController.addBuildingMaterials(noOfMaterials);
                    }
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
                gameController.damageWall(_wallDamagetick);
            }
            yield return new WaitForSeconds(_wallDamageFrequency);
        }
    }

}

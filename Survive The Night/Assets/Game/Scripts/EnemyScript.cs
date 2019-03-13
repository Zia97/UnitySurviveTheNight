using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private float _speed = 3.0f;
    private bool isMovingTowardsBase = true;
    private bool isMovingTowardsPlayer = false;
    private double _health = 100;
    protected GameController gameController;
    private int _wallDamagetick = 1;
    private float _wallDamageFrequency = 1f;
    private bool randomDrop;
    private int _dropProbability = 3;
    private int _scoreValue = 1;
    private bool isDead = false;
    Rigidbody2D rb;

    private GameObject _gameController;
    
    private System.Random rnd = new System.Random();

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (!isDead)
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
                //Vector3 targetPosition = Vector3.MoveTowards(transform.position, gameController.getPlayerPosition(), _speed * Time.deltaTime);
                //rb.MovePosition(targetPosition);
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
        }
    }

    protected void setHealth(int health)
    {
        _health = health;
    }

    protected void setSpeed(float speed)
    {
        _speed = speed;
    }

    protected void setDamage(int damage)
    {
        _wallDamagetick = damage;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.gameObject.name == "wall")
        {
            isMovingTowardsBase = false;
            StartCoroutine("beginWallDamage");
            beginWallDamage();
        }

        else if (collision.transform.gameObject.name == "Bullet" || collision.transform.gameObject.name == "Bullet(Clone)")
        {
            if (!isDead)
            {
                _health = _health - 30;

                if (_health <= 0)
                {
                    isDead = true;
                    gameController.updateScore(_scoreValue);
                    gameController.increaseCurrentWaveScore(_scoreValue);

                    gameObject.GetComponent<Animator>().Play("die");

                    Destroy(gameObject, 3);

                    if (randomDrop)
                    {
                        Debug.Log("Random drop");
                    }
                }
            }
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

                    Destroy(gameObject, 3);

                    if (randomDrop)
                    {
                        Debug.Log("Random drop");
                    }
                }
            }
        }

        else
        {
            Debug.Log("Unidentified enemy collision");
        }
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

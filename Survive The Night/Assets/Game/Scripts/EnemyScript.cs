using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private float _speed = 3.0f;
    private bool isMoving = true;
    private double _health = 100;
    protected GameController gameController;
    private int _wallDamagetick = 1;
    private float _wallDamageFrequency = 1f;
    private bool randomDrop;
    private int _dropProbability = 3;
    private int _scoreValue = 1;

    private System.Random rnd = new System.Random();

    // Use this for initialization
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position += -transform.right * _speed * Time.deltaTime;
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
            isMoving = false;
            StartCoroutine("beginWallDamage");
            beginWallDamage();
        }

        else if (collision.transform.gameObject.name == "Bullet" || collision.transform.gameObject.name == "Bullet(Clone)")
        {
            _health = _health - 30;

            if (_health <= 0)
            {
                gameController.updateScore(_scoreValue);
                gameController.increaseCurrentWaveScore(_scoreValue);

                if(randomDrop)
                {
                    Debug.Log("Random drop");
                }

                Destroy(gameObject);
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
            gameController.damageWall(_wallDamagetick);
            yield return new WaitForSeconds(_wallDamageFrequency); 
        }
    }

}

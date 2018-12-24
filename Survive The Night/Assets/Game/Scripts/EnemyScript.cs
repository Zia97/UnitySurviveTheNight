using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private float speed = 3.0f;
    private bool isMoving = true;
    private double health = 100;
    private GameController gameController;
    private System.Timers.Timer aTimer = new System.Timers.Timer();

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
            transform.position += -transform.right * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.gameObject.name == "wall")
        {
            isMoving = false;
            beginWallDamage();
        }

        else if (collision.transform.gameObject.name == "Bullet" || collision.transform.gameObject.name == "Bullet(Clone)")
        {
            health = health - 30;

            if (health <= 0)
            {
                killTimer();
                Destroy(gameObject);
            }
        }
        
        else
        {
            Debug.Log("Unidentified enemy collision");
        }
    }

    void beginWallDamage()
    {
        aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        aTimer.Interval = 2000;
        aTimer.Enabled = true;
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        gameController.damageWall(1);
    }

    private void killTimer()
    {
        aTimer.Stop();
        aTimer.Dispose();
    }
}

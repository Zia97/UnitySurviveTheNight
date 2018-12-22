using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private float speed = 3.0f;
    private bool isMoving = true;
    private double health = 100;

    // Use this for initialization
    void Start()
    {

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
        Debug.Log("Hit:" + collision.collider.name);

        
        if (collision.transform.gameObject.name == "wall")
        {
            isMoving = false;
        }

        else if (collision.transform.gameObject.name == "Bullet" || collision.transform.gameObject.name == "Bullet(Clone)")
        {
            health = health - 30;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        else
        {
            Debug.Log("Unidentified enemy collision");
        }
    }
}

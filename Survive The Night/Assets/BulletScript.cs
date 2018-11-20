using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public float velX =50f;
    float velY = 6f;
    Rigidbody2D rb;
    private Vector3 target;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody2D>();
        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;
        rb.velocity = new Vector2(shootDirection.x*velX, shootDirection.y*velX);
        Destroy(gameObject, 3f);

    }
	
	// Update is called once per frame
	void Update ()
    {


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.name == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    private Vector3 target;
    public float playerSpeed = 7f;
    public GameObject bullet;
    Rigidbody2D rb;
    Vector2 bulletPos;

    // Use this for initialization
    void Start ()
    {
        target = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            if (target.x > -6)
            {
                fireBullet(target);
            };
        }

        if (target.x < -6)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, playerSpeed * Time.deltaTime);
        };
    }


    void fireBullet(Vector3 target)
    {
        bulletPos = transform.position;
        bulletPos += new Vector2(1f, 0f);
        Instantiate(bullet,bulletPos,Quaternion.identity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        target = transform.position;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    private float speed = 3.0f;
    private bool isMoving = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isMoving)
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.name != "wall")
        {
            Destroy(this.gameObject);
        }
        else
        {
            isMoving = false;
        
        }
    }
}

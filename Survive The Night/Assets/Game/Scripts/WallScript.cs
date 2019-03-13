using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

    int health = 100;


    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void decreaseHealth(int damage)
    {
        health = health - damage;
        if(health==0)
        {
            Debug.Log("Wall destroyed");
        }
    }
}

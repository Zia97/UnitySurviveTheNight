using Assets.Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    private Vector3 target;
    public float playerSpeed = 7f;
    public GameObject bullet;
    Rigidbody2D rb;
    Vector2 bulletPos;
    private bool controlsEnabled = true;
    public GameObject _weaponControllerObject;
    private WeaponController weaponController;

    // Use this for initialization
    void Start ()
    {
        _weaponControllerObject = GameObject.FindWithTag("WeaponController");
        weaponController = _weaponControllerObject.GetComponent<WeaponController>();
        target = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (controlsEnabled)
        {
            MoveCharacter();
        }
    }

    void MoveCharacter()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            if (target.x >= -5.5)
            {
                fireBullet();
            };
        }

        if (target.x < -5.5)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, playerSpeed * Time.deltaTime);
        };
    }


    void fireBullet()
    {
        
        bulletPos = transform.position;
        bulletPos += new Vector2(1f, -0.4f);
        weaponController.Fire(bulletPos);
        //Instantiate(bullet,bulletPos,Quaternion.identity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        target = transform.position;
    }

    public void AreControlsEnabled(bool set)
    {
        controlsEnabled = set;
    }

}

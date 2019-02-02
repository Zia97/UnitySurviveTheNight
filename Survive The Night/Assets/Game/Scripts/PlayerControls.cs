using Assets.Game.Scripts;
using Assets.HeroEditor.Common.CharacterScripts;
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
    private WeaponControls _weaponControls;
   // private GameObject _weaponNew;

    // Use this for initialization
    void Start ()
    {
        _weaponControllerObject = GameObject.FindWithTag("WeaponController");
        _weaponControls = gameObject.GetComponent<WeaponControls>();

        weaponController = _weaponControllerObject.GetComponent<WeaponController>();
        target = transform.position;
        rb = GetComponent<Rigidbody2D>();
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
        }

        if (target.x < -5.5)
        {
            Vector3 targetPosition = Vector3.MoveTowards(transform.position, target, playerSpeed * Time.deltaTime);
            rb.MovePosition(targetPosition);
        };
    }


    void fireBullet()
    {
        bulletPos = transform.position;
        bulletPos += new Vector2(1f, -0.4f);
        weaponController.Fire(bulletPos);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        target = transform.position;
    }

    public void AreControlsEnabled(bool set)
    {
        controlsEnabled = set;
        if (_weaponControls != null)
        {
            _weaponControls.areControlsEnabled(set);
        }
        else
        {
            _weaponControls = gameObject.GetComponent<WeaponControls>();
        }
    }

}

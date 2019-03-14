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
    private GameObject _gameControllerObject;
    private GameController _gameController;
    private bool isAlive = true;

    private Vector3 targetPos;
    private Character myCharacter;

    private bool _uiButtonClicked;

    // Use this for initialization
    void Start ()
    {
        myCharacter = this.GetComponent<Character>();
        _weaponControllerObject = GameObject.FindWithTag("WeaponController");
        _weaponControls = gameObject.GetComponent<WeaponControls>();

        weaponController = _weaponControllerObject.GetComponent<WeaponController>();
        target = transform.position;
        rb = GetComponent<Rigidbody2D>();

        _gameControllerObject = GameObject.FindWithTag("GameController");
        _gameController = _gameControllerObject.GetComponent<GameController>();

    }

    public void getCurrentWeapon()
    {
       Debug.Log(myCharacter.Firearm.Params.Name + "      349tuq349t");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!_uiButtonClicked)
        {
            MoveCharacter();
            if (transform.position != targetPos)
            {
                myCharacter.Animator.SetBool("Run", true);
            }
            else
            {
                myCharacter.Animator.SetBool("Run", false);
            }
        
        }
    }

    void MoveCharacter()
    {
        if(isAlive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = transform.position.z;
                
            }

            if (target.x < -5.5)
            {
               targetPos = Vector3.MoveTowards(transform.position, target, playerSpeed * Time.deltaTime);
                rb.MovePosition(targetPos);
            }
        }
        
    }


    void fireBullet()
    {
        bulletPos = transform.position;
        bulletPos += new Vector2(1f, -0.4f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        target = transform.position;
        Debug.Log(collision.gameObject.name);
        if (!collision.gameObject.name.Equals("wall") && !collision.gameObject.name.Equals("borderwall"))
        {
            isAlive = false;
            myCharacter.Animator.SetBool("DieBack", true);
            _gameController.PlayerDead();
        }

    }



    public void uiButtonClicked(bool set)
    {
        _uiButtonClicked = set;
    }

}

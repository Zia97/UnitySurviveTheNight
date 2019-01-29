using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class WeaponController : MonoBehaviour
    {

        public GameObject bullet;
        //public Transform shotSpawn;
        public float fireRate;
        public float delay;

       // private AudioSource audioSource;

        public void Start()
        {
            //audioSource = GetComponent<AudioSource>();
            //InvokeRepeating("Fire", delay, fireRate);
        }

        public void Fire(Vector2 direction)
        {
            Debug.Log("Fired from weapon controller");
            Instantiate(bullet, direction, Quaternion.identity);
            //audioSource.Play();
        }

    }
}

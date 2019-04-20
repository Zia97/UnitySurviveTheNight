using Assets.Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject basicEnemy;
    public GameObject mediumEnemy;
    public GameObject officerEnemy;
    public GameObject dogEnemy;
    public GameObject runnerEnemy;
    public GameObject crawlerEnemy;

    private Quaternion spawnRotation = Quaternion.identity;
    private Vector3 spawnPosition = new Vector3(12, -4.5f, 0);

    private bool spawn = true;

    private void Start()
    {
        System.Random rnd = new System.Random();
        InvokeRepeating("SpawnEnemies", 1.6F, rnd.Next(0, 7));

    }

    private void SpawnEnemies()
    {
        System.Random rnd = new System.Random();
        var val = rnd.Next(0, 6);

        if(val==0)
        {
            Instantiate(dogEnemy, spawnPosition, spawnRotation);
        }
        else if (val == 1)
        {
            Instantiate(basicEnemy, spawnPosition, spawnRotation);
        }
        else if (val == 2)
        {
            Instantiate(officerEnemy, spawnPosition, spawnRotation);
        }
        else if (val == 3)
        {
            Instantiate(runnerEnemy, spawnPosition, spawnRotation);
        }
        else if (val == 4)
        {
            Instantiate(crawlerEnemy, spawnPosition, spawnRotation);
        }
        else if (val == 5)
        {
            Instantiate(mediumEnemy, spawnPosition, spawnRotation);
        }
    }

}

   
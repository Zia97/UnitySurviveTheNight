using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Scripts
{
    class MediumEnemy : EnemyScript
    {

        private int damage = 3;
        private int health = 150;
        private float frequency = 1f;
        private int dropProb = 6;

        void Start()
        {
            GameObject gameControllerObject = GameObject.FindWithTag("GameController");

            if (gameControllerObject != null)
            {
                gameController = gameControllerObject.GetComponent<GameController>();
            }
            if (gameController == null)
            {
                Debug.Log("Cannot find 'GameController' script");
            }

            setScoreValue(3);
            setDamage(damage);
            setDropProbability(dropProb);
            createDropProbability();
            setHealth(health);
        }
    }
}

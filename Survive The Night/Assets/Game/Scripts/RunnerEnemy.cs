using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class RunnerEnemy : EnemyScript
    {

        private int damage = 1;
        private int health = 110;
        private float frequency = 1f;
        private int dropProb = 6;
        private float speed = 2.4f;
        private int _difficulty = 2;

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

            updateMaterialRange(7, 13);
            setScoreValue(2);
            setDamage(damage);
            setDropProbability(dropProb);
            createDropProbability();
            setHealth(health);
            setSpeed(speed);
        }

        public int getDifficulty()
        {
            return _difficulty;
        }
    }
}

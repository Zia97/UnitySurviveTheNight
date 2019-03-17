using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class CrawlerEnemy : EnemyScript
    {

        private int damage = 1;
        private int health = 90;
        private float frequency = 1f;
        private int dropProb = 6;
        private float speed = 2.5f;
        private int _difficulty = 1;

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

            updateMaterialRange(3, 7);
            setScoreValue(1);
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

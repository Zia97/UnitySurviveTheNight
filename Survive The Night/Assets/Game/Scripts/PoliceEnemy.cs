using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class PoliceEnemy : EnemyScript
    {

        private int damage = 5;
        private int health = 300;
        private float frequency = 1f;
        private int dropProb = 6;
        private float speed = 1.3f;
        private int _difficulty = 4;

        void Start()
        {
            GameObject gameControllerObject = GameObject.FindWithTag("GameController");

            if (gameControllerObject != null)
            {
                gameController = gameControllerObject.GetComponent<GameController>();
            }
            if (gameController == null)
            {
            }

            updateMaterialRange(15, 25);
            setScoreValue(4);
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

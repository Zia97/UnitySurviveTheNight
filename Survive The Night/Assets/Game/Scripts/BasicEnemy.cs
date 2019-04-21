//Author : Qasim Ziauddin

//Subclass of the EnemyScript.cs superclass. Sets the specific properties for this BasicEnemy.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class BasicEnemy : EnemyScript
    {

        private int damage = 1;
        private int health = 120;
        private float frequency = 1f;
        private int dropProb = 3;
        private int _difficulty = 1;
        private float speed = 1.7f;

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

            updateMaterialRange(3, 7);
            setScoreValue(1);
            setDamage(damage);
            setDropProbability(4);
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

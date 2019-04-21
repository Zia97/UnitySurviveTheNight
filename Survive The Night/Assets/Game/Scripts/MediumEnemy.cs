//Author : Qasim Ziauddin

//Subclass of the EnemyScript.cs superclass. Sets the specific properties for this MediumEnemy.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class MediumEnemy : EnemyScript
    {

        private int damage = 3;
        private int health = 150;
        private float frequency = 1f;
        private int dropProb = 6;
        private float speed = 1.5f;
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
            }

            updateMaterialRange(8, 15);
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

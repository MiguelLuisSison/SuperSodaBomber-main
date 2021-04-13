using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Enemy_ScriptObject
    A scriptable object that holds the initial data for the enemies:
        - Shooter
        - Roller
*/
namespace SuperSodaBomber.Enemies{
    [CreateAssetMenu(fileName = "New Enemy ScriptableObject", menuName = "ScriptableObjects/Enemy")]
    public class Enemy_ScriptObject : ScriptableObject
    {
        public float health = 100;
        public float movementSpeed;
        public float attackSpeed, attackRadius, attackRate;
        public float spotRadius;
        public GameObject projectilePrefab;
        public EnemyType enemyType;
        public EnemyPhase enemyPhase;
        public bool isMoving = false;
        public bool facingRight = true;

    }
}

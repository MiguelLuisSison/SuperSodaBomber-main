using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperSodaBomber.Events;

/*
Enemy
    Responsible for handling the enemy properties
    and its behaviour.

    Things are needed to improve:
        Make sure that the script is flexible for all types of
        milkings. Except for Milcher. He'll have his own script

    Milkings:
        Shooter Milking
        Roller Milkling
*/
namespace SuperSodaBomber.Enemies{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Enemy_ScriptObject scriptObject;   //holds saved data for the enemy
        [SerializeField] private VoidEvent enemyDeathEvent;         //contains the events when the enemy dies
        private float health; //movementSpeed, attackSpeed;
        // private EnemyState state;
        // private float attackRange, spotRange;
        // private GameObject projectilePrefab;
        // private EnemyType type;
        // private EnemyPhase phase;   //(Phase 1, Phase 2)
        // private bool isMoving;

        private BaseEnemy chosenScript;

        void Awake()
        {
            health = scriptObject.health;
            // movementSpeed = scriptObject.movementSpeed;
            // attackSpeed = scriptObject.attackSpeed;
            // attackRange = scriptObject.attackRadius;
            // spotRange = scriptObject.spotRadius;
            // projectilePrefab = scriptObject.projectilePrefab;
            // type = scriptObject.enemyType;
            // phase = scriptObject.enemyPhase;
            // isMoving = scriptObject.isMoving;
            chosenScript = gameObject.AddComponent(EnemyProcessor.Fetch(scriptObject, gameObject)) as BaseEnemy;
            chosenScript.Init(scriptObject);
        }

        void FixedUpdate()
        {
            chosenScript.InvokeState();
            // state = chosenScript.GetState();
            // Debug.Log($"state {state}");
        }

        public void Damage(float hp){
            health -= hp;

            if (health <=0){
                Die();
            }
        }

        //when the enemy rans out of health
        void Die(){
            enemyDeathEvent?.Raise();
            Destroy(gameObject);
        }
    }
}
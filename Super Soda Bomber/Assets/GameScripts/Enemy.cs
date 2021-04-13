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
    public abstract class BaseEnemy: MonoBehaviour, IEnemyOuter{
        protected Dictionary<EnemyPhase, IEnemyInner> phaseDict = new Dictionary<EnemyPhase, IEnemyInner>();
        protected IEnemyInner chosenPhase;

        protected EnemyState currentState = EnemyState.Wander;
        protected bool facingRight;
        protected Vector3 playerPos;
        
        //data needed from scriptable object
        protected float spotRadius, attackRadius, attackRate;

        //configures data
        public virtual void Init(Enemy_ScriptObject scriptObject){
            spotRadius = scriptObject.spotRadius;
            attackRadius = scriptObject.attackRadius;
            attackRate = scriptObject.attackRate;
            facingRight = scriptObject.facingRight;
        }

        //changes the state of the enemy
        public EnemyState GetState(){
            return currentState;
        }

        //flips the character if it's within the firing radius
        public void Flip(){
            if(facingRight && (playerPos.x < transform.position.x) ||
            !facingRight && playerPos.x > transform.position.x){

                facingRight = !facingRight;
                //Unlike the player, the enemy has a different shape of collider.
                transform.localScale = new Vector3(transform.localScale.x*-1, 1f, 1f);
            }
        }

        //returns true if the enemy is within the radius
        protected bool FindTarget(float radius){
            playerPos = PlayerMovement.playerPosition;
            return (Vector3.Distance(transform.position, playerPos)  < radius);
        }

        public abstract void InvokeState();

        //nested class for phases, since it will have different behavior
        public abstract class BaseInnerEnemy:IEnemyInner{
            //this is where the behavior takes place
            public abstract void CallState();
        }
    }

    public class Shooter: BaseEnemy{
        public override void Init(Enemy_ScriptObject scriptObject)
        {
            base.Init(scriptObject);
            phaseDict.Add(EnemyPhase.Phase1, new Phase1(this));
            // phaseDict.Add(EnemyPhase.Phase2, new Phase2(this));

            chosenPhase = phaseDict[scriptObject.enemyPhase];     
        }

        public override void InvokeState()
        {
            chosenPhase.CallState();
        }

        public class Phase1: BaseInnerEnemy{
            private Shooter outer;
            public Phase1(Shooter o){
                outer = o;
            }

            public override void CallState(){
                switch (outer.currentState){
                    case EnemyState.Wander:
                        //find if the player is within the range
                        if (outer.FindTarget(outer.spotRadius))
                            outer.currentState = EnemyState.Attack;                
                        break;
                    case EnemyState.Attack:
                        if (outer.FindTarget(outer.attackRadius)){
                            //attack
                        }
                        else if (!outer.FindTarget(outer.spotRadius))
                            outer.currentState = EnemyState.Wander;

                        outer.Flip();
                        break;
                }
            }
        }
        public class Phase2: BaseInnerEnemy{
            private Shooter outer;
            public Phase2(Shooter o){
                outer = o;
            }

            public override void CallState(){
            
            }
        }
    }
    
}
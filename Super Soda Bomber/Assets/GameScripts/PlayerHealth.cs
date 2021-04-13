using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperSodaBomber.Events;

/*
Player Health
    Manages the health behavior of the player.
    It also triggers the damage and death of the player.
*/

public class PlayerHealth : MonoBehaviour
{
    [SerializeField][Range(0, 10)] private int health = 3;  //health of the player
    [SerializeField] private VoidEvent onPlayerDamageEvent; //events to trigger when player takes damage (e.g. sound)
    [SerializeField] private VoidEvent onPlayerDeathEvent;  //events to trigger when player dies

    private GameObject player;          //player gameobject
    private Coroutine coroutine;        //asynchronous work
    private bool isTempImmune;          //player status if it's temporarily immuned
    private SpriteRenderer p_Renderer;  //player sprite renderer

    void Awake(){
        player = gameObject;
        p_Renderer = player.GetComponent<SpriteRenderer>();
    }

    void Damage(){
        --health;
        onPlayerDamageEvent?.Raise();
        GameplayScript.SetHpUI(health<0 ? 0 : health);
        if (health <= 0)
            OnPlayerDeath();
        else{
            coroutine = StartCoroutine(TemporaryImmunity());
        }

    }

    void OnPlayerDeath(){
        if (coroutine != null)
            StopCoroutine(coroutine);
        onPlayerDeathEvent?.Raise();
        GameplayScript.current.GameOver();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.transform.position.y < 0){
            OnPlayerDeath();
        }
    }

    //triggers when player touches an enemy
	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.layer == 11 && !isTempImmune){
			Damage();
		}
	}

    private IEnumerator TemporaryImmunity(){
        //contains number of seconds to blink 8 times
        float[] durations = {2f, 1f};
        float blinkCycle = 8f;
        isTempImmune = true;

        Color oldColor = p_Renderer.color;  //opaque
        Color blinkColor = new Color(oldColor.r, oldColor.g, oldColor.b, .5f);  //semi-transparent

        //loops through the durations array
        for (int i = 0; i < durations.Length; i++){
            for (int j = 0; j < blinkCycle; j++){
                
                //changes the player's renderer color using the blinkCycle
                if (j%2 == 0)
                    p_Renderer.color = blinkColor;
                else
                    p_Renderer.color = oldColor;

                yield return new WaitForSeconds(durations[i]/blinkCycle);
            }
        }

        isTempImmune = false;
    }
}

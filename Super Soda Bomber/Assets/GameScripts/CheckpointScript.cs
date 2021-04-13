using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
CheckpointScript
    Responsible for the behaviors of the checkpoint
*/

public class CheckpointScript : PublicScripts
{

    public bool isTouched; //used to verify if the checkpoint has been already triggered
    public GameObject UILayer; //used for placing the notification whether the player triggers a checkpoint
    public GameObject notifyPrefab;
    SpriteRenderer sRenderer; //used to render the sprite
    Sprite checkActiveImg; //image of the checkpoint
    float notifyDuration = 3;

    void Awake(){
        checkActiveImg = Resources.Load<Sprite>("Gameplay/check_active");
        sRenderer = gameObject.GetComponent<SpriteRenderer>();

    }

    public void ChangeState(){
        //changes the sprite of the image if it's touched
        sRenderer.sprite = checkActiveImg;
        isTouched = true;
        
    }

    //generates a notification for 3 seconds
    public IEnumerator Notify(){
        var notifyObj = Instantiate(notifyPrefab, UILayer.transform) as GameObject;
        var notification = notifyObj.GetComponent<Text>();
        notification.text = descriptions["checkSave"];
        yield return new WaitForSeconds(notifyDuration);
        Destroy(notifyObj);
        

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
StageCompleteScript
    simply posts the current score
    when stage is complete + save and move stage.
*/

public class StageCompleteScript : MonoBehaviour
{
    public Text scoreText;

    void Start(){
        scoreText.text = "Score: " + PlayerPrefs.GetInt("CurrentScore", 0).ToString();
    }

    void PerkChoose(){
        
    }

}

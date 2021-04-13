﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
PublicScripts
    Contains all scripts that are mostly used in the game

    Make sure to add this script at the EventSystem 
    and then use this to the OnClick() buttons if needed.
*/

public class PublicScripts : MonoBehaviour
{
    //list of non-projectile scores
    protected readonly Dictionary<string, int> scores = new Dictionary<string, int>(){
        {"checkpoint", 125},
        {"ability", 10}
    };

    /*list of projectile scores
        formatting:
            if it's an explosive, add _s, _m and _l for variating scores
            otherwise, add the name as is
            
            you can find the name of the projectile at Projectile.cs, p_name
    */
    protected readonly Dictionary<string, int> projScores = new Dictionary<string, int>(){
        {"sodaBomb_s", 15},
        {"sodaBomb_m", 30},
        {"sodaBomb_l", 75},
        {"smallCluster_s", 15},
        {"smallCluster_m", 30},
        {"smallCluster_l", 75},
        {"shotgun", 5},
        {"fizztol", 15}
    };

    //description constants
    protected readonly Dictionary<string,string> descriptions = new Dictionary<string, string>(){
        {"checkSave", "Checkpoint Saved!"},
        {"confirmQuit", "Are sure you want to exit the level?"},
        {"confirmCheckpoint", "Are you sure you want to load the last checkpoint?"},
        {"confirmNew", "Are you sure you want to override your saved game data?"}
    };

    //firing rates of the weapons (shows cooldown in secs)
    protected readonly Dictionary <string, float> fireRates = new Dictionary <string, float>(){
        {"sodaBomb", .6f},
        {"fizztol", .4f},
        {"cannade", 1.2f},
        {"shotgun", .8f}

    };

    /*damage matrix for projectiles
        formatting:
            if it's an explosive, add _max and _min for damage range
            otherwise, add the name as is
    */
    protected readonly Dictionary <string, float> projDamage = new Dictionary<string, float>(){
        {"sodaBomb_max", 50f},
        {"sodaBomb_min", 20f},
        {"smallCluster_max", 30f},
        {"smallCluster_min", 10f},
        {"fizztol", 30f},
        {"shotgun", 15f}

    };

    //explosion types
    /// <summary>
    /// Explosion Type of the Projectile
    /// </summary>
    public enum ExplosionType{
        Contact = 0,        //collision triggers explosion (default)
        Detonate,           //player or time triggers explosion
        Delay,              //time triggers explosion
        Instant             //instantly explodes
    }

    /// <summary>
    /// Moves to selected scene
    /// </summary>
    public void _Move(string scene){
        SceneManager.LoadScene(sceneName: scene);
    }
    
    /// <summary>
    /// Toggles on/off the selected prompt
    /// </summary>
    public void _TogglePrompt(GameObject prompt){
        bool status = prompt.activeInHierarchy;
        prompt.SetActive(!status);
    }
}

//this will be used on abilities
[Flags]
public enum PlayerAbilities
{
    None, LongJump, DoubleJump, Dash = 4
}

public enum PlayerProjectiles{
    Undefined, SodaBomb, Fizztol, Cannade, Shotgun
}

public enum MapName{
    None, Test, Level1, Level2
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{

    Text scoreText;
    GameSession gameSession;//i think this is game session type because of the script class
    void Start()
    {
        //this is defining this var as the component that is in this game object text component field
        scoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();//this is grabbing the gameobject called game session
        //the type of the script is the name of the class which is an ibject
    }

    void Update()
    {
        scoreText.text = gameSession.GetScore().ToString();
    }
}

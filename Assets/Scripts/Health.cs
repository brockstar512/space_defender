using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{

    Text healthText;
    Player player;
    void Start()
    {
        healthText = GetComponent<Text>();
        player = FindObjectOfType<Player>();//grabbing the script that holds the function

    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = player.GetHealth().ToString();//grabbing what the function is returning and stringifying it 
    }
    //if I were to have multple levels I would need a singlton
}

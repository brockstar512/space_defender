using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float speedOfSpin = 1f; //i think this means how much do you want it to rotate in 1 second

    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0, speedOfSpin * Time.deltaTime);//rotate the sprite that has this script in the z plane this much independent of frame rate
    }
}

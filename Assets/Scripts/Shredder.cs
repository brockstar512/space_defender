using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
 private void OnTriggerEnter2D(Collider2D collision){
     Destroy(collision.gameObject);
 }
}


//in order for something to collide into the shredder it also needs a collider component

//rigid body - allows physics to effect it
//collider - gives it a phyical hit box presences
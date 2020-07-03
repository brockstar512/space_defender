using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //connect our waveConfig script
    //is the type Wave Config because we are referenceing a script??
    WaveConfig waveConfig;
    //a group of coordinates which is why the type fo list is transform(initializing a variable)
    List<Transform> waypoints;
    // [SerializeField] float moveSpeed = 2f;
    
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //this grabs the Getwaypoints function out of wave config
        waypoints = waveConfig.GetWaypoints();
        //the tranform component that holds this script. we want the position of the waypoints at the particular list.
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }

    public void setWaveConfig(WaveConfig waveConfig)
    {
        //this class waveConfig = the recieved waveConfig
        this.waveConfig=waveConfig;


    }

    private void Move()
    {
              //list we use Count. array we use length
        if(waypointIndex <= waypoints.Count-1)
        {
            //public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta);
            //var of the spoition we are headed
            var targetPosition = waypoints[waypointIndex].transform.position;
            //movement speed                    //this makes it independed of frame rate
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if(transform.position == targetPosition)
            {
                waypointIndex++;
            }

        }
        else{
            Destroy(gameObject);
        }
    }
}

//different scripts
//enemny spawner
//wave config
//enemy
//enemy pathing


//waves have the wave config which has hte path and the enemy info
//after i make the wave i need to drag it to enemy spawner
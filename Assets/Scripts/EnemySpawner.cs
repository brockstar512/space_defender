using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //a list of the waves in the ui
    //each game object wave will be dragged into this serilaized field
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    
    IEnumerator Start()
    {
        //endless loop of enemenies
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while(looping);
    }
 
    private IEnumerator SpawnAllWaves()
    {     //[SerializeField] List<WaveConfig> waveConfigs;    list of waves 
            // we starting the wave at whatever index we want to start at from our serialize feild          
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++) 
            {           //we are looping over our co routine
                        var currentWave = waveConfigs[waveIndex];
                        //start couroutine with the forst wave being past in(1. start it)
                        yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            }
    }

//Coroutine type and function. we pass in the wave config //for some reason i am not using get component for this coroutine. might be because 
//I am passing it in as an argumtn and I am grabbing it in the serializer.
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)//(2. instatitate it)
    {         //this will loop the number of enemies are returned from get number of enumies. the enemy count will increase
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++){
        //3. 
        var newEnemy = Instantiate(
            //the what
            waveConfig.GetEnemyPrefab(), 
            //the where
            waveConfig.GetWaypoints()[0].transform.position,
            //the rotation. this will prevent it from rotating
            Quaternion.identity);
            //4.what time do you want to allaps?

            //newEnemy.GetComponent<EnemyPathing> = get the component script rnrmy psthing and this function from that script
            //this wave config is sending the funtion in enemy pathing this config
        newEnemy.GetComponent<EnemyPathing>().setWaveConfig(waveConfig);
        yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        //we are going to yiled instead of time we are going to start after wave is finished spawning 
        }
        //5. create a for loop that will instantiate the scene with enemies 
    }
}

//component
//getting a component you are accessing a game object thhat is holding a script with a configuration
//each enemy that is spawned has an enemy pathing script 
// newEnemy.GetComponent<EnemyPathing>().setWaveConfig(waveConfig);
//
//I know now...its because its referencing something in its serialized field rather than another scripts components
// proof will be the wave configs in the enemy spaener component
//not sure why we are not getting a component but instead directly getting the function
//var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
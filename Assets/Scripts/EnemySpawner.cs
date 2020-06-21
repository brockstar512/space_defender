using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //a list of the waves in the ui
    //each game object wave will be dragged into this serilaized field
    [SerializeField] List<WaveConfig> waveConfigs;
    int startingWave = 0;
    
    void Start()
    {
        //first wave
        var currentWave = waveConfigs[startingWave];
        //start couroutine with the forst wave being past in(1. start it)
        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

//Coroutine type and function. we pass in the wave config
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)//(2. instatitate it)
    {         //this will loop the number of enemies are returned from get number of enumies. the enemy count will increase
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++){
        //3. 
        Instantiate(
            //the what
            waveConfig.GetEnemyPrefab(), 
            //the where
            waveConfig.GetWaypoints()[0].transform.position,
            //the rotation. this will prevent it from rotating
            Quaternion.identity);
            //4.what time do you want to allaps?
        yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
        //5. create a for loop that will instantiate the scene with enemies 
    }
}

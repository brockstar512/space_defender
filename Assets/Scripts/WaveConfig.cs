using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
//this is a script that will let you create a scriptable object config with this script
//i think a scriptbale object is basically a place that collects a lot of scripts
{
 
 [SerializeField] GameObject enemyPrefab;
 [SerializeField] GameObject pathPrefab;
 [SerializeField] float timeBetweenSpawns = 0.5f;
 [SerializeField] float spawnRandomFactor = 0.3f;
 [SerializeField] int numberOfEnemies = 5;
 [SerializeField] float moveSpeed = 2f;

public GameObject GetEnemyPrefab()
{
    return enemyPrefab;
}
public List<Transform> GetWaypoints()
{
    //this function is iterating over the pathPrefab and making a new list called waveway points
    //its a type of transform
    var waveWaypoints = new List<Transform>();
    //for each (-type-  indexed item- in - the list of items you wnt to iterate over)
    foreach (Transform child in pathPrefab.transform)
    {
        waveWaypoints.Add(child);
    }
    return waveWaypoints;
}
public float GetTimeBetweenSpawns()
{
    return timeBetweenSpawns;
}
public float GetSpawnRandomFactor()
{
    return spawnRandomFactor;
}
public float GetMoveSpeed()
{
    return moveSpeed;
}

public int GetNumberOfEnemies()
{
    return numberOfEnemies;
}

}
//we have making a list of paths and giving the enemy a path rather than making the same paths for each enemy
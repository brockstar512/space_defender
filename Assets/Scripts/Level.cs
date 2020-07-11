using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//in order to get this way to run you need to left click get ui and add event system
//https://www.udemy.com/course/unitycourse/learn/lecture/11666688#questions/10815170
//1
using UnityEngine.SceneManagement;
//you can load levels by name reference or by index
public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;
public void LoadStartMenu()
{
    //2. I know this will be the first scene
    SceneManager.LoadScene(0);
}
//3. main game
public void LoadDaGame()
{
    //loading the game referene. the name of the scene HAS TO BE THE SAME in this loader
    SceneManager.LoadScene("Game");
    // Debug.Log("Clicked!");
    FindObjectOfType<GameSession>().ResetGame();
}
//4 game over scene
public void LoadGameOver()
{
    StartCoroutine(WaitAndLoad());//1. call this function
    
}
IEnumerator WaitAndLoad()//2 this is the function you will call
{
    Debug.Log("first part of Game over");
    yield return new WaitForSeconds(delayInSeconds);//3 but the function will run when this is fullfilled.. above will run immediately but second part will wait
    SceneManager.LoadScene("Game Over");
    Debug.Log("second part of game over after the coroutine");
}


//5 get out of game
public void QuitGame()
{
    Application.Quit();
}


}

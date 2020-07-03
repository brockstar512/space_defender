using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingleton();//1
    }
    private void SetUpSingleton()//2
    {                       //this references the type class of MusicPlay...allows for more reusablitiy....MusicPlayer : MonoBehaviour
        if(FindObjectsOfType(GetType()).Length > 1)//if there is more than one destroy game object...if theres not more than one keep it alive!
        {
            Destroy(gameObject);

        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

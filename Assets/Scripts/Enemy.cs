using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;
    [Header("Shooting")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots=0.2f;
    [SerializeField] float maxTimeBetweenShots = 5f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [Header("Sound")]
    //Audio 2. serialize the sound
    [SerializeField] AudioClip deathSFX;
    //audio 3. serialize volume
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0,1)] float shootSoundVolume = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        //1. shot counter gets a random number
        shotCounter = Random.Range(minTimeBetweenShots,maxTimeBetweenShots);
        
    }

    // Update is called once per frame
    void Update()
    {
     CountDownAndShoot();
    }
    private void CountDownAndShoot()
    {
        //randomized countdown goes down regardless of frame rate capabilities
        shotCounter -= Time.deltaTime;
        //framerate independent
        if(shotCounter<= 0)
        {
            Fire();
            //reset counter
            shotCounter = Random.Range(minTimeBetweenShots,maxTimeBetweenShots);
        }
    }

    private void Fire(){
        GameObject laser = Instantiate(
            //what is the object
            projectile,
            //create yourself where the Enemy is
            transform.position,
            Quaternion.identity
        )as GameObject;//we are instatitating a gameobject as a game object
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);


    }

    private void OnTriggerEnter2D(Collider2D otherThing)
    {
        //class        variable   (othingThing is the thing that bumps into us)(it has a damage dealer component we are grabbing)
        DamageDealer damageDealer = otherThing.gameObject.GetComponent<DamageDealer>();
        //whaever pumps into us we are getting its component DamageDealer and then using the get damage function
        //we are getting the damage dealer component then getting the amount of damage the items gives

        if (!damageDealer) { return; }//if they dont have this component just return

        ProcessHit(damageDealer);
        //pass damageDealer to Process hit
    }
                //DamageDealer damageDealer is stuck inthat function show we take it and give it to ProcessHit
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        //theres a triiger box on the collider component i need to check off for the laser
        damageDealer.hit();
        if ( health <= 0){
            Die();
        }
    }

    private void Die()
        //to add the visual effects we pull in the fx game object then instantiate it when the player dies as a nwe game object at the location the player was and we destroy the game object, over a period of seconds
    {
        Destroy(gameObject);
        //instantiating... the what the where and the rotation
        GameObject explosion= Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        //AUDIO 1.
        //whats the clip, what z vector,
        //we want the sound to  play where the camera is. not the enemy otherwise that will be far away
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);

        //adding score to the function in gamesession... i guess script is object and what I drag is component?
        FindObjectOfType<GameSession>().AddToScore(scoreValue);

    }
}

//coroutine
//allows us to delay a function or line of code
//(execute the method but you run this method by saying) StartCoroutine(functionName())
//IEnumerator functionName(){
    // yield return new WaitForSeconds(amount of seconds you want to wait);
    //what you want to do after the seconds
// }


//what would this do?

// private IEnumerator OurFirstCoroutine()
// {
//       // make enemies start moving                       --this will happen
//       yield return StartCoroutine(OurSecondCoroutine()); --then this will execute the OurSecondCoroutine and wants that is done 
//       // make enemies stop moving                        --this will happen after our secondCoroutine is finshed running
// }


//USE LAYERS TO PREVENT FRIENDLY FIRE. MANAGE IN PROJECT SETTINGS- PHYSICS 2D this will tell you what should or shouldnt collide with things



                                                            //get component vs get object


// The GameObject.FindObjectOfType is more of a scene wide search and isn't the optimal way of getting an answer. Actually, Unity publicly said its super slow Unity3D API Reference - FindObjectOfType

// The GetComponent<LevelManager>(); is a local call. Meaning whatever file is making this call will only search the GameObject that it is attached to. So in the inspector, the file will only search other things in the same inspector window. Such as Mesh Renderer, Mesh Filter, Etc. Or that objects children. I believe there is a separate call for this, though.
// Also, you can use this to access other GameObject's components if you reference them first.


// 1.) GetComponent<T> finds a component only if it's attached to the same GameObject. GameObject.FindObjectOfType<T> on the other hand searches whole hierarchy and returns the first object that matches!

// 2.) GetComponent<T> returns only an object that inherits from Component, while GameObject.FindObjectOfType<T> doesn't really care.


// In short, a component is a part of a gameobject 

// So for example, an engine (component) and the car (gameobject).A car without an engine isn't super useful, so every gameobject has at least one component - that being a transform.

// To be able to get hold of a gameobject from your code, they each have a name - that's the name shown in the hierarchy window. This name can be used in GameObject.Find("thename") to get hold of a gameobject.

// Hopefully with me so far!

// So, lets say we add a light to a scene. That light is a gameobject with a light component attached to it. If you wanted to then change the colour of your light from script, you'd first grab the gameobject, then get the light component from it - with that, you can then change the colou
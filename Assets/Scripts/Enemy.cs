using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots=0.2f;
    [SerializeField] float maxTimeBetweenShots = 5f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
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
    {
        Destroy(gameObject);
        //instantiating... the what the where and the rotation
        GameObject explosion= Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        //AUDIO 1.
        //whats the clip, what z vector,
        //we want the sound to  play where the camera is. not the enemy otherwise that will be far away
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);

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
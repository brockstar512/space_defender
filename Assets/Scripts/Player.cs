using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //this will give these a heading
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = .5f;
    [SerializeField] int health = 200;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.25f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0,1)] float shootSoundVolume = 0.25f;


    //prefab is a game object
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    void Start()
    {
        SetUpMoveBoundaries();
      
    }


    // Update is called once per frame
    void Update(){
        //in order to make the player move we want his position to update every frome
        Move();
        Fire();     
    }


    private void Fire(){
        //this is found in axis is project settings
        if(Input.GetButtonDown("Fire1")){
            //when we start firing we start a coroutine
            firingCoroutine= StartCoroutine(FireContinuously());
        }
        if(Input.GetButtonUp("Fire1")){
            //when the button goes up we stop the coroutine
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously(){
        //if you hold down the fire button it will be true
        while(true){
            //if fire button is clicked.
            //instantiate(add) this gameObject                      //this wont apply any rotation                                   
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            //every time fire is clicked create a game object out of laser prefab and instatiate it as a game object
            //giving it a speed below
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        
        }
    }

    private void Move(){

        //this is the change in the x axis so left would be - and right would be +
        //this way is not based on a key input but by the keys that decrease and 
        //increase x and y so awd and the arrows
        //Time.deltaTime will make the movement independant of framerate. The default moves a certaun units per framright
        //this would also work with joysticks
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        //adding the change to the current position
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin,yMax);
        //position is the new x position and stay where you are in the y
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries(){
        Camera gameCamera = Camera.main; //setting variable to main camera
        xMin= gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - padding;
        yMin= gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0,1,0)).y - padding;
    } 




    private void OnTriggerEnter2D(Collider2D laser)
    {
        DamageDealer damageDealer = laser.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }//if they dont have this component just return
        //this conditional thing is in case we dumb a prefab that is expecting a
        //script that we forgot to check its box bc I think the defaul tis there
        //script are not always active... its to prevent a null object reference error
        ProcessHit(damageDealer);
        //pass damageDealer to Process hit
    }

   private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        //theres a triiger box on the collider component i need to check off for the laser
        damageDealer.hit();
        //this damage dealer is calling the hit function that destroys laser when it hits somehting
         if( health <= 0){
            Die();
        }
    }

    private void Die()
    {

        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
            
            
            //find this script and grab it
        FindObjectOfType<Level>().LoadGameOver();//load this script when this function happens
    }


}
//what is a coroutine? 
// a method that is called when a condition is met
//startCoroutine(nameOfCoroutine())
//return type
//IEnumerator nameOfCoroutine(){
//     things to do
//yield return SomeCondion(seconds)
// }

            //example

 // startCoroutine(PrintandWait());

     // IEnumerator PrintandWait(){
    //     Debug.Log("first message sent");
    //     yield return new WaitForSeconds(3);////the second part will wait 3 seconds
    //     Debug.Log("Second message sent");
    // } 
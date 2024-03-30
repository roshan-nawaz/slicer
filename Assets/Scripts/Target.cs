using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Target : MonoBehaviour
{
    private GameManager gameManager;

    public float minSpeed = 10.0f;
    public float maxSpeed = 15.0f;
    public float maxTorque = 5.0f;
    public float xRange = 4;
    public float yRange = 6;

    //score for each oject we destroy
    public int point;
    private Rigidbody targetRb;
    public ParticleSystem explodeParticle;
    //audio source
    [SerializeField]
    private AudioSource sliceEffectSound;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //adding force and torque to target 
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        targetRb.transform.position = GenerateSpawnPos();

    }


    //returning speed of object
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    //returning range of torque
    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    //returns the position which it gets spawned
    private Vector3 GenerateSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, 0));
    }

    /*
     *if we do this the object will exist without its rigid body
    private void OnMouseDown()
    {
         Destroy(targetRb);
    }
    */
    //the above only destroys rigid bod componenet
    
    //Destroy(gameoObject);
    //it will destroy object including everything
    
    //onMouseDown is a func to check if we clicked any or not 
    private void OnMouseDown()
    {
        //checks if the object with tag bad is pressed or not
        if (gameObject.CompareTag("Bad"))
        {
            sliceEffectSound.PlayOneShot(sliceEffectSound.clip);
            //sliceEffectSound.Play();
            gameManager.GameOver();
        }
        Destroy(gameObject);
        //sliceEffectSound.PlayOneShot(sliceEffectSound.clip);
        //sliceEffectSound.Play();
        gameManager.UpdateScore(point);
        Instantiate(explodeParticle, transform.position, Quaternion.identity);
        //explodeParticle.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Good") && gameObject.CompareTag("Bad")){
            gameManager.GameContinues();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }


}


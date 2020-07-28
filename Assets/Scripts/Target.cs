using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float minSpeed = 12f;
    public float maxSpeed = 16f;

    public float xRange = 4f;
    public float ySpawnPos = -6f;

    private Rigidbody targetRb;
    public float maxTorque = 10f;


    private GameManager gameManager;
    public int pointValue; // This is the point for prefab object when clicked
    public ParticleSystem explosiveparticles;


    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomUpwardForce(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 RandomUpwardForce()
    {
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        return Vector3.up * randomSpeed;
    }

    Vector3 RandomSpawnPos()
    {
        float randomXPos = Random.Range(-xRange, xRange);
        return new Vector3(randomXPos, ySpawnPos);
    }

    float RandomTorque()
    {
        return Random.Range(0, maxTorque);   
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(explosiveparticles, transform.position, explosiveparticles.transform.rotation);
        }
    }


    //Detects sensor, if it goes below sensor is destroyed
    private void OnTriggerEnter(Collider other)
    {
        

            gameManager.UpdateLives(1);
            if (!gameObject.CompareTag("Bad"))
            {
                gameManager.GameOver();
            }
            Destroy(gameObject);
        
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObject : MonoBehaviour
{
    public ParticleSystem debrisParticle;
    public AudioClip collideSound;

    private GameManager gameManager;
    private Vector3 cameraPos = new Vector3(0, 10, -26);
    private float yBound = 0;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < yBound)
        {
            Destroy(gameObject);
        }
    }

    // Obstacles decrease player health
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(debrisParticle, transform.position, debrisParticle.transform.rotation);
            AudioSource.PlayClipAtPoint(collideSound, cameraPos);
            gameManager.playerHealth--;
            gameManager.livesText.text = "Lives: " + gameManager.playerHealth + "/" + gameManager.maxHealth;
            gameObject.SetActive(false);
        }
    }

    // Powerup increases player health
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameManager.playerHealth < gameManager.maxHealth)
            {
                Instantiate(debrisParticle, transform.position, debrisParticle.transform.rotation);
                AudioSource.PlayClipAtPoint(collideSound, cameraPos);
                gameManager.playerHealth++;
                gameManager.livesText.text = "Lives: " + gameManager.playerHealth + "/" + gameManager.maxHealth;
                gameObject.SetActive(false);
            }
        }
    }
}

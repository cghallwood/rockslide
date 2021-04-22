using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public AudioClip explosionSound;

    private GameManager gameManager;
    private Vector3 cameraPos;
    private float turnSpeed = 10;
    private float forceSpeed = 10;
    private float xBound = 12.0f;
    private float stunTime = 0;
    private bool isStunned = false;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody>();
        cameraPos = new Vector3(0, 10, -26);
    }

    void FixedUpdate()
    {
        MovePlayer();
        ConstrainPlayer();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckHealth();
    }

    // If player health reaches zero, end game
    void CheckHealth()
    {
        if (gameManager.playerHealth <= 0)
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            AudioSource.PlayClipAtPoint(explosionSound, cameraPos);
            Destroy(gameObject);
            gameManager.isGameActive = false;
            gameManager.GameOver();
        }
    }

    // Move player left and right
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move player if not stunned, otherwise wait half a second
        if (!isStunned)
        {
            transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);
        } else
        {
            stunTime += Time.deltaTime;
            if (stunTime > 0.5f)
            {
                isStunned = false;
                stunTime = 0;
            }
        }
    }

    // Restricts player movement on x-axis
    void ConstrainPlayer()
    {
        // Restrict player movement to left bound
        if (transform.position.x < -xBound)
        {
            isStunned = true;
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
            playerRb.AddForce(Vector3.right * forceSpeed, ForceMode.Impulse);
        }

        // Restrict player movement to right bound
        if (transform.position.x > xBound)
        {
            isStunned = true;
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
            playerRb.AddForce(Vector3.left * forceSpeed, ForceMode.Impulse);
        }
    }
}

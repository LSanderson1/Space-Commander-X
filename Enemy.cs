using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    Rigidbody enemyRb;
    private GameObject player;
    public float movementSpeed = 20.0f;
    public float projectileSpeed = 100.0f;
    private int verticalMovement;
    private int horizontalMovement;
    private float startTime = 1.0f;
    private float verticalDirection;
    private float horizontalDirection;
    public Rigidbody projectile;
    public GameObject spawnLocation;
    public int hitPoints;
    public int pointValue;
    public AudioSource soundplayer;
    public AudioClip enemyshoot;
    public AudioClip enemyhit;
    public GameObject powerup;
    public TMP_Text score;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        InvokeRepeating("SetDirection", startTime, Random.Range(1, 3));
        InvokeRepeating("ShootProjectile", startTime, Random.Range(2, 5));
        enemyshoot = Resources.Load<AudioClip>("Audio/enemyshoot");
        soundplayer = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        score = GameObject.Find("Canvas/Score").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        
        //transfers between global coordinates and coordinates to viewport
        Vector3 CameraPosition = Camera.main.WorldToViewportPoint(transform.position);
        CameraPosition.x = Mathf.Clamp(CameraPosition.x, 0.05f, .95f);
        CameraPosition.y = Mathf.Clamp(CameraPosition.y, 0.05f, .95f);

        //converts back to global coordinates and stays in camera bounds
        transform.position = Camera.main.ViewportToWorldPoint(CameraPosition);
    }

    void SetDirection()
    {
        verticalMovement = Random.Range(0, 2);
        horizontalMovement = Random.Range(0, 2);

        if (verticalMovement == 1)
        {
            verticalDirection = -1;
        }
        else
        {
            verticalDirection = 1;
        }

        if (horizontalMovement == 1)
        {
            horizontalDirection = -1;
        }
        else
        {
            horizontalDirection = 1;
        }
    }

    void FixedUpdate()
    {
        enemyRb.velocity = new Vector3(horizontalDirection * movementSpeed, 0, verticalDirection* movementSpeed);
    }

    void ShootProjectile()
    {
        Rigidbody proj = Instantiate(projectile, spawnLocation.transform.position, Quaternion.identity);
        proj.velocity = spawnLocation.transform.forward * projectileSpeed;
        soundplayer.PlayOneShot(enemyshoot, PlayerPrefs.GetFloat("volume"));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy" || other.tag != "Enemy Projectile")
        {
            hitPoints--;
            if (hitPoints == 0)
            {
                Instantiate(powerup, spawnLocation.transform.position, Quaternion.identity);
                int currentscore = PlayerPrefs.GetInt("score");
                PlayerPrefs.SetInt("score", currentscore += pointValue);
                score.text = "Score: " + PlayerPrefs.GetInt("score");
                Destroy(gameObject);
                
            }
        }
    }
}

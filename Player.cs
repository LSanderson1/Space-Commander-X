using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    Rigidbody playerRb;
    public float speed = 20.0f;
    [SerializeField] private float degree = 45f;
    public float rotationSpeed = 10.0f;
    public float projectileSpeed = 100.0f;
    private float horizontalInput;
    private float verticalInput;
    public Rigidbody projectile;
    public GameObject spawnLocation;
    public int hitPoints = 3;
    public int ammo = 20;
    
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public TMP_Text ammotext;
    public AudioSource soundplayer;
    public AudioClip playershoot;
    public AudioClip playerhit;
    public AudioClip powerupsound;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playershoot = Resources.Load<AudioClip>("Audio/playershoot");
        powerupsound = Resources.Load<AudioClip>("Audio/volumetest");
        soundplayer = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        ammo = PlayerPrefs.GetInt("ammo");
    }

    // Update is called once per frame
    void Update()
    {
        //rotates to a point around the y axis to where the player can control where they can shoot
        float step = degree * Time.deltaTime * rotationSpeed;

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //right button mouse click
        if(Input.GetMouseButton(1))
        {
            //if the mouse moves to the left, rotate to the left
            if (Input.GetAxis("Mouse X") < 0)
            {
                transform.Rotate(Vector3.down, step);
            }
            //if the mouse moves to the right, rotate to the right
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.Rotate(Vector3.up, step);
            }
        }

        //transfers between global coordinates and coordinates to viewport
        Vector3 CameraPosition = Camera.main.WorldToViewportPoint(transform.position);
        CameraPosition.x = Mathf.Clamp(CameraPosition.x, 0.05f, .95f);
        CameraPosition.y = Mathf.Clamp(CameraPosition.y, 0.05f, .95f);

        //converts back to global coordinates and stays in camera bounds
        transform.position = Camera.main.ViewportToWorldPoint(CameraPosition);

        if(Input.GetKeyDown(KeyCode.Space) && ammo > 0)
        {
            //need to typecast it / shoots bullets in the direction of the player
            Rigidbody proj = Instantiate(projectile, spawnLocation.transform.position, Quaternion.identity) as Rigidbody;
            proj.velocity = transform.forward * projectileSpeed;
            soundplayer.PlayOneShot(playershoot, PlayerPrefs.GetFloat("volume"));
            ammo--;
        }
        
        
    }

    //where it happens every frame
    void FixedUpdate()
    {
        //where the player moves
        playerRb.velocity = new Vector3(horizontalInput * speed, 0, verticalInput * speed);
        ammotext.text = "Bullets: " + ammo;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Enemy Projectile")
        {
            hitPoints--;
            if (hitPoints == 2)
            {
                heart3.SetActive(false);
            }

            if (hitPoints == 1)
            {
                heart2.SetActive(false);
            }

            if (hitPoints == 0)
            {
                heart1.SetActive(false);
                Destroy(gameObject);
                gameManager.GameOver();
            }
        }
        else
        {
            ammo += 5;
            Destroy(other.gameObject);
            soundplayer.PlayOneShot(powerupsound, PlayerPrefs.GetFloat("volume"));
        }
        
    }
}

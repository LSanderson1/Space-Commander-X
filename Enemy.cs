using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        InvokeRepeating("SetDirection", startTime, Random.Range(1, 3));
        InvokeRepeating("ShootProjectile", startTime, Random.Range(2, 5));

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
    }

    void OnTriggerEnter(Collider other)
    {
        hitPoints--;
        Destroy(other.gameObject);
        if (hitPoints == 0)
        {
            Destroy(gameObject);
        }
    }
}

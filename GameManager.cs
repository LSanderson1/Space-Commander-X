using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject[] enemyPrefab;
    private float spawnLocationX = 92.0f;
    private float spawnLocationUp = 41.0f;
    private float spawnLocationDown = -61.0f;
    private int enemyIndex;
    private float startTime = 2.0f;
    private float repeatTime = 5.0f;

    public TextMeshProUGUI lives;
    public TextMeshProUGUI gameOverText;

    public Button restart;

    public bool isGameActive;
    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        InvokeRepeating("SpawnEnemies", startTime, repeatTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies()
    {
        //gets a random number from how many enemies there are in the array and chooses from random
        enemyIndex = Random.Range(0, enemyPrefab.Length);
        //gets the position on the Vector3 which the enemies will spawn
        Vector3 spawnPos = new Vector3(Random.Range(-spawnLocationX, spawnLocationX), -100, Random.Range(spawnLocationDown, spawnLocationUp));
        //creates those enemy objects
        Instantiate(enemyPrefab[enemyIndex], spawnPos, Quaternion.identity);
    }

    public void GameOver()
    {
        isGameActive = false;
        lives.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

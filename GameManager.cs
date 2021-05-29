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
    public GameObject pauseScreen;
    public GameObject optionsScreen;
    public AudioSource soundplayer;
    public AudioClip volumetest;

    public TextMeshProUGUI lives;
    public TextMeshProUGUI gameOverText;
    public TMP_Text scoretext;
    public Slider volumeslider;

    public Button restart;

    public bool isGameActive;
    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        InvokeRepeating("SpawnEnemies", startTime, repeatTime * PlayerPrefs.GetFloat("difficulty"));
        volumetest = Resources.Load<AudioClip>("Audio/volumetest");
        soundplayer = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        PlayerPrefs.SetInt("score", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGameActive = !isGameActive;

            if (isGameActive == true)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
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
        scoretext.gameObject.SetActive(true);

        if (PlayerPrefs.GetInt("score") > PlayerPrefs.GetInt("hiscore"))
        {
            PlayerPrefs.SetInt("hiscore", PlayerPrefs.GetInt("score"));
            scoretext.text = "New High Score! " + "Your Score: " + PlayerPrefs.GetInt("hiscore");
        }
        else
        {
            scoretext.text = "Current High Score: " + PlayerPrefs.GetInt("hiscore") + " Your Score: " + PlayerPrefs.GetInt("score");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        optionsScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Options()
    {
        pauseScreen.SetActive(false);
        optionsScreen.SetActive(true);
        volumeslider.value = PlayerPrefs.GetFloat("volume");
    }

    public void Volume()
    {
        PlayerPrefs.SetFloat("volume", volumeslider.value);
        soundplayer.PlayOneShot(volumetest,PlayerPrefs.GetFloat("volume"));
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/Menu");
    }

    public void Back()
    {
        optionsScreen.SetActive(false);
        pauseScreen.SetActive(true);
    }
}

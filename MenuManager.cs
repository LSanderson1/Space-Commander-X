using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;



public class MenuManager : MonoBehaviour
{
    public GameObject optionsmenu;
    public GameObject difficultymenu;
    public GameObject mainmenu;
    public Slider volumeslider;
    public AudioSource soundplayer;
    public AudioClip volumetest;

    // Start is called before the first frame update
    void Start()
    {
        volumetest = Resources.Load<AudioClip>("Audio/volumetest");
        soundplayer = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }

    public void DifficultyScreen()
    {
        difficultymenu.SetActive(true);
        mainmenu.SetActive(false);
    }

    public void ReturnToMenu()
    {
        optionsmenu.SetActive(false);
        difficultymenu.SetActive(false);
        mainmenu.SetActive(true);
    }

    public void Easy() 
    {
        PlayerPrefs.SetFloat("difficulty", 1.25f);
        PlayerPrefs.SetInt("ammo", 30);
    }

    public void Normal()
    {
        PlayerPrefs.SetFloat("difficulty", 1f);
        PlayerPrefs.SetInt("ammo", 20);
    }

    public void Hard()
    {
        PlayerPrefs.SetFloat("difficulty", 0.5f);
        PlayerPrefs.SetInt("ammo", 10);
    }

    public void OptionsMenu()
    {
        mainmenu.SetActive(false);
        optionsmenu.SetActive(true);
        volumeslider.value = PlayerPrefs.GetFloat("volume");
    }

    public void Volume()
    {
        PlayerPrefs.SetFloat("volume", volumeslider.value);
        soundplayer.PlayOneShot(volumetest, PlayerPrefs.GetFloat("volume"));
    }
}

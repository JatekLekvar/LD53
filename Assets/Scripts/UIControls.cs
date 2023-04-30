using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControls : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] public GameObject endScreen;
    public int score = 0;

    void Awake(){
        pauseMenu = GameObject.Find("Pause Menu");
        pauseMenu.gameObject.SetActive(false);
        endScreen = GameObject.Find("End UI");
        endScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1){
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
            }
            else{
                pauseMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
            }
        }

        //Debug Keys
        if (Input.GetKeyDown(KeyCode.K))
        {
            score++;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            endScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        scoreText.text = score.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControls : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TextMeshProUGUI scoreText;
    public int score = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1){
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else{
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        scoreText.text = score.ToString();
    }
}

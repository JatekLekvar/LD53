using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private UIControls UIControlsScript;
    [SerializeField] private TextMeshProUGUI endUIScoreText;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private int minScore;
    [SerializeField] private int twoStarMinScore;
    [SerializeField] private int threeStarMinScore;
    [SerializeField] private GameObject[] stars;

    void Awake(){
        UIControlsScript = this.gameObject.GetComponentInParent<UIControls>();
        winScreen = GameObject.Find("Win");
        loseScreen = GameObject.Find("Lose");

        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    void Update(){
        endUIScoreText.text = UIControlsScript.score.ToString();

        //One star
        if(UIControlsScript.score >= minScore){
            winScreen.SetActive(true);
            stars[0].gameObject.SetActive(true);

            //Two stars
            if(UIControlsScript.score >= twoStarMinScore){
                stars[1].gameObject.SetActive(true);

                //Three stars
                if(UIControlsScript.score >= threeStarMinScore){
                    stars[2].gameObject.SetActive(true);
                }
            }
        }
        else{
            loseScreen.gameObject.SetActive(true);
        }
    }


}

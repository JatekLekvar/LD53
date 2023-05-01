using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestUIUpdater : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> questTexts;
    [SerializeField] private List<Image> questImages;
    [SerializeField] private GameObject checkMark;
    void Start()
    {
        foreach(Transform child in transform){
            if(child.tag == "Quest Text"){
                questTexts.Add(child.GetComponent<TextMeshProUGUI>());
                child.gameObject.SetActive(false);
            }
            if(child.tag == "Quest Image"){
                questImages.Add(child.GetComponent<Image>());
                child.gameObject.SetActive(false);
            }
        }

        checkMark = GameObject.Find("Check Mark");
        checkMark.SetActive(false);
    }

    public void UpdateTextAndImage(string questText, Sprite questImage, int index){
        questTexts[index].gameObject.SetActive(true);
        questImages[index].gameObject.SetActive(true);
        questTexts[index].text = questText;
        questImages[index].sprite = questImage;
    }

    public void QuestIsComplete(){
        checkMark.SetActive(true);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MegaPackage : MonoBehaviour
{
    [SerializeField] private Canvas oneItemCanvas;
    [SerializeField] private Canvas twoItemCanvas;
    [SerializeField] private Canvas threeItemCanvas;

    public string[] items;
    public Dictionary<string, Sprite> sprites;
    public Sprite[] spriteList;

    void Awake(){
        sprites = new Dictionary<string, Sprite>();
        sprites.Add("barrel" , spriteList[0]);
    }

    void Update(){
        switch(items.Length){
            case 1 : oneItemCanvas.gameObject.SetActive(true);
            break;
            case 2 : twoItemCanvas.gameObject.SetActive(true);
            break;
            case 3 : threeItemCanvas.gameObject.SetActive(true);
            break;
        }
    }
}

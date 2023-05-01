using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private List<GameObject> questPanels;
    public Dictionary<string, Sprite> sprites;
    public Sprite[] spriteList;

    void Start()
    {
        foreach(Transform child in transform){
            questPanels.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }

        sprites = new Dictionary<string, Sprite>();
        sprites.Add("barrel" , spriteList[0]);
        sprites.Add("flash" , spriteList[1]);
        sprites.Add("radioactive" , spriteList[2]);
        sprites.Add("tv" , spriteList[3]);
        sprites.Add("vase" , spriteList[4]);
        sprites.Add("woodenBox" , spriteList[5]);
    }

    void Update()
    {
        int i = 0;
        foreach(Quest quest in questManager.questsForTheDay){
            questPanels[i].SetActive(true);
            QuestUIUpdater questUIUpdaterScript = questPanels[i].GetComponent<QuestUIUpdater>();

            if(quest.cleared){
                questUIUpdaterScript.QuestIsComplete();
            }

            int k = 0;
            foreach(string item in quest.requiredParts){
                switch(item){
                    case "barrel" : questUIUpdaterScript.UpdateTextAndImage("- Barrel", sprites["barrel"], k);
                    break;
                    case "flash" : questUIUpdaterScript.UpdateTextAndImage("- Flash", sprites["flash"], k);
                    break;
                    case "radioactive" : questUIUpdaterScript.UpdateTextAndImage("- Radioactive Barrel", sprites["radioactive"], k);
                    break;
                    case "tv" : questUIUpdaterScript.UpdateTextAndImage("- TV", sprites["tv"], k);
                    break;
                    case "vase" : questUIUpdaterScript.UpdateTextAndImage("- Vase", sprites["vase"], k);
                    break;
                    case "woodenBox" : questUIUpdaterScript.UpdateTextAndImage("- Wooden Box", sprites["woodenBox"], k);
                    break;
                    default : Debug.Log("Nincs ilyen nevű item!");
                    break;
                }
                k++;
            }

            i++;
        }
    }
}

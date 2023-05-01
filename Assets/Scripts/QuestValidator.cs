using System.Linq;
using UnityEngine;

public class QuestValidator : MonoBehaviour
{
    public QuestManager questManager;

    void OnTriggerEnter(Collider collider)
    {
        MegaPackage mp = collider.gameObject.GetComponent<MegaPackage>();
        if (mp != null)
        {
            for (int i = 0; i < questManager.questsForTheDay.Length; i++)
            {
                Quest quest = questManager.questsForTheDay[i];

                if (quest.cleared)
                {
                    continue;
                }

                quest.cleared = true;

                foreach (string item in mp.items)
                {
                    if (!quest.requiredParts.Contains(item))
                    {
                        quest.cleared = false;
                        break;
                    }
                }

                if(quest.requiredParts.Length < mp.items.Length){
                    quest.cleared = false;
                }

                if (quest.cleared)
                {
                    questManager.OnQuestCleared(i);
                }
            }

            Destroy(mp.gameObject);
        }
    }
}

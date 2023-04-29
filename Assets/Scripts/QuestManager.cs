using System;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest[] questsForTheDay;

    public void OnQuestCleared(int index)
    {
        Debug.Log($"Quest {index} cleared");
    }
}

[Serializable]
public class Quest
{
    public string[] requiredParts;
    public bool cleared;
}
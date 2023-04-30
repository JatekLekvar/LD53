using System;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest[] questsForTheDay;
    public UIControls UIControls;

    public void OnQuestCleared(int index)
    {
        UIControls.score++;
        Debug.Log($"Quest {index} cleared");
    }
}

[Serializable]
public class Quest
{
    public string[] requiredParts;
    public bool cleared;
}
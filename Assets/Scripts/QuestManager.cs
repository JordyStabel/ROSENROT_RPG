﻿using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public string[] questMarkerNames;
    public bool[] questMarkerComplete;

    public static QuestManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        questMarkerComplete = new bool[questMarkerNames.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(CheckForCompletion("quest test"));
            MarkQuestComplete("quest test");
            MarkQuestIncomplete("fight the demon");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveQuestData();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadQuestData();
        }
    }

    public bool CheckForCompletion(string quest)
    {
        int questIndex = FindQuestIndex(quest);

        if (questIndex != 0)
        {
            return questMarkerComplete[questIndex];
        }
        return false;
    }

    public void MarkQuestComplete(string quest)
    {
        questMarkerComplete[FindQuestIndex(quest)] = true;
        UpdateLocalQuests();
    }

    public void MarkQuestIncomplete(string quest)
    {
        questMarkerComplete[FindQuestIndex(quest)] = false;
        UpdateLocalQuests();
    }

    private int FindQuestIndex(string quest)
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            if (questMarkerNames[i] == quest)
            {
                return i;
            }
        }
        Debug.LogError($"Quest: {quest}, does not exist!");
        return 0;
    }

    public void UpdateLocalQuests()
    {
        QuestObjectTrigger[] questTriggers = FindObjectsOfType<QuestObjectTrigger>();

        foreach (QuestObjectTrigger trigger in questTriggers)
        {
            trigger.CheckCompletion();
        }
    }

    public void SaveQuestData()
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            // 1 = true / 0 = false
            PlayerPrefs.SetInt($"Questmarker_{questMarkerNames[i]}", questMarkerComplete[i] ? 1 : 0);
        }
    }

    public void LoadQuestData()
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            if (PlayerPrefs.HasKey($"Questmarker_{questMarkerNames[i]}"))
            {
                questMarkerComplete[i] = PlayerPrefs.GetInt($"Questmarker_{questMarkerNames[i]}") == 1 ? true : false;
            }
            else
            {
                questMarkerComplete[i] = false;
            }
        }
    }
}

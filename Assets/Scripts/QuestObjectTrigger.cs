using UnityEngine;

public class QuestObjectTrigger : MonoBehaviour
{
    public GameObject objectToTrigger;
    public string questToTrigger;
    public bool activeAfterCompletion;
    private bool initialCheckDone = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!initialCheckDone)
        {
            CheckCompletion();
            initialCheckDone = true;
        }
    }

    public void CheckCompletion()
    {
        if (QuestManager.instance.CheckForCompletion(questToTrigger))
        {
            objectToTrigger.SetActive(activeAfterCompletion);
        }
    }
}

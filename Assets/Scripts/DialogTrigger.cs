using UnityEngine;

public class DialogTrigger : MonoBehaviour
{

    [SerializeField]
    private string[] lines;

    [SerializeField]
    private bool canActivate;
    [SerializeField]
    private bool isPerson = true;

    public string questToMark;
    public bool shouldActivateQuest, markComplete;

    // Update is called once per frame
    void Update()
    {
        if (canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            DialogManager.instance.ShowDialog(lines, isPerson);
            DialogManager.instance.ShouldActivateQuestAfterDialog(questToMark, markComplete);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = false;
        }
    }
}
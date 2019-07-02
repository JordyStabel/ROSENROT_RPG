using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Image buttonImage;
    public Text amountText;
    public int buttonIndex;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click()
    {

        if (GameManager.instance.itemsInIventory[buttonIndex] != "")
        {
            GameMenu.instance.SelectItem(GameManager.instance.GetItemReference(GameManager.instance.itemsInIventory[buttonIndex]));
        }
    }
}

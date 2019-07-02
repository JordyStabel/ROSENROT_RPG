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
        if (GameMenu.instance.gameMenu.activeInHierarchy)
        {
            if (GameManager.instance.itemsInIventory[buttonIndex] != "")
            {
                GameMenu.instance.SelectItem(GameManager.instance.GetItemReference(GameManager.instance.itemsInIventory[buttonIndex]));
            }
        }
        if (Shop.instance.shopMenu.activeInHierarchy)
        {
            if (Shop.instance.buyMenu.activeInHierarchy)
            {
                Shop.instance.SelectBuyItem(GameManager.instance.GetItemReference(Shop.instance.itemsForSale[buttonIndex]));
            }

            if (Shop.instance.sellMenu.activeInHierarchy)
            {
                Shop.instance.SelectSellItem(GameManager.instance.GetItemReference(GameManager.instance.itemsInIventory[buttonIndex]));
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    [SerializeField]
    private float resellValueMultiplier = .65f;

    public GameObject shopMenu, buyMenu, sellMenu;
    public Text goldAmount;

    public string[] itemsForSale;

    public ItemButton[] buyItems, sellItems;

    public Item selectedItem;
    public Text buyItemName, buyItemDescription, buyItemValue;
    public Text sellItemName, sellItemDescription, sellItemValue;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !shopMenu.activeInHierarchy)
        {
            OpenShop();
        }
    }

    public void OpenShop()
    {
        shopMenu.SetActive(true);
        OpenBuyMenu();

        GameManager.instance.shopActive = true;
        goldAmount.text = $"{GameManager.instance.currentGold} G";
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);

        GameManager.instance.shopActive = false;
    }

    public void OpenBuyMenu()
    {
        // Select first item for sale and prevent empty UI text elements
        buyItems[0].Click();

        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        for (int i = 0; i < buyItems.Length; i++)
        {
            buyItems[i].buttonIndex = i;

            if (itemsForSale[i] != "")
            {
                buyItems[i].buttonImage.gameObject.SetActive(true);
                buyItems[i].buttonImage.sprite = GameManager.instance.GetItemReference(itemsForSale[i]).itemSprite;
                buyItems[i].amountText.text = "";
            }
            else
            {
                buyItems[i].buttonImage.gameObject.SetActive(false);
                buyItems[i].amountText.text = "";
            }
        }
    }

    public void OpenSellMenu()
    {
        // Select first item in inventory and prevent empty UI text elements
        sellItems[0].Click();

        sellMenu.SetActive(true);
        buyMenu.SetActive(false);
        UpdateSellItems();
    }

    private void UpdateSellItems()
    {
        GameManager.instance.SortItems();
        for (int i = 0; i < sellItems.Length; i++)
        {
            sellItems[i].buttonIndex = i;

            if (GameManager.instance.itemsInIventory[i] != "")
            {
                sellItems[i].buttonImage.gameObject.SetActive(true);
                sellItems[i].buttonImage.sprite = GameManager.instance.GetItemReference(GameManager.instance.itemsInIventory[i]).itemSprite;
                sellItems[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                sellItems[i].buttonImage.gameObject.SetActive(false);
                sellItems[i].amountText.text = "";
            }
        }
    }

    public void SelectBuyItem(Item item)
    {
        selectedItem = item;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.description;
        buyItemValue.text = $"Price: {selectedItem.value} G";
    }

    public void SelectSellItem(Item item)
    {
        selectedItem = item;
        sellItemName.text = selectedItem.itemName;
        sellItemDescription.text = selectedItem.description;
        sellItemValue.text = $"Price: {Mathf.FloorToInt(selectedItem.value * resellValueMultiplier)} G";
    }

    public void BuyItem()
    {
        if (selectedItem != null)
        {
            if (GameManager.instance.currentGold >= selectedItem.value)
            {
                GameManager.instance.currentGold -= selectedItem.value;
                GameManager.instance.AddItem(selectedItem.itemName);
            }
            goldAmount.text = $"{GameManager.instance.currentGold} G";
        }
    }

    public void SellItem()
    {
        if (selectedItem != null)
        {
            GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.value * resellValueMultiplier);
            GameManager.instance.RemoveItem(selectedItem.itemName);
            goldAmount.text = $"{GameManager.instance.currentGold} G";
            UpdateSellItems();
        }
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharacterStats[] characterStats;

    public bool gameMenuOpen, dialogActive, fadingBetweenAreas;

    public string[] itemsInIventory;
    public int[] numberOfItems;
    public Item[] referenceItems;

    // Awake is called before the first frame update
    void Awake()
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
        SortItems();
    }

    void Update()
    {
        if (gameMenuOpen || dialogActive || fadingBetweenAreas)
        {
            PlayerController.instance.isAllowedToMove = false;
        }
        else
        {
            PlayerController.instance.isAllowedToMove = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            AddItem("Iron Armor");
            AddItem("henk");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            RemoveItem("Iron Armor");
            AddItem("henk");
        }
    }

    public Item GetItemReference(string itemName)
    {
        foreach (Item item in referenceItems)
        {
            if (item.name == itemName)
            {
                return item;
            }
        }
        return null;
    }

    public void SortItems()
    {
        bool itemAfterInventorySpot = true;

        while (itemAfterInventorySpot)
        {
            itemAfterInventorySpot = false;
            for (int i = 0; i < itemsInIventory.Length - 1; i++)
            {
                if (itemsInIventory[i] == "")
                {
                    itemsInIventory[i] = itemsInIventory[i + 1];
                    itemsInIventory[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if (itemsInIventory[i] != "")
                    {
                        itemAfterInventorySpot = true;
                    }
                }
            }
        };
    }

    public void AddItem(string itemName)
    {
        int newItemIndex = 0;
        bool foundSpace = false;
        bool itemValid = false;

        for (int i = 0; i < referenceItems.Length; i++)
        {
            if (referenceItems[i].name == itemName)
            {
                itemValid = true;
                break;
            }
        }

        if (itemValid)
        {
            for (int i = 0; i < itemsInIventory.Length; i++)
            {
                if (itemsInIventory[i] == "" || itemsInIventory[i] == itemName)
                {
                    newItemIndex = i;
                    foundSpace = true;
                    break;
                }
            }

            if (foundSpace)
            {
                itemsInIventory[newItemIndex] = itemName;
                numberOfItems[newItemIndex]++;
            }
        }
        else
        {
            Debug.LogError(itemName + " Invalid item!");
        }
        GameMenu.instance.ShowItems();
    }

    public void RemoveItem(string itemName)
    {
        bool foundItem = false;
        int itemIndex = 0;

        for (int i = 0; i < itemsInIventory.Length; i++)
        {
            if (itemsInIventory[i] == itemName)
            {
                foundItem = true;
                itemIndex = i;
                break;
            }
        }

        if (foundItem)
        {
            numberOfItems[itemIndex]--;

            if (numberOfItems[itemIndex] <= 0)
            {
                itemsInIventory[itemIndex] = "";
            }

            GameMenu.instance.ShowItems();
        }
        else
        {
            Debug.LogError(itemName + " Not found!");
        }
    }
}
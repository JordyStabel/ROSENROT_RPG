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
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
}
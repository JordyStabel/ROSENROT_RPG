using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharacterStats[] characterStats;

    public bool gameMenuOpen, dialogActive, fadingBetweenAreas, shopActive;

    public string[] itemsInIventory;
    public int[] numberOfItems;
    public Item[] referenceItems;

    public int currentGold;

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
        if (gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive)
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

        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadData();
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

    public void SaveData()
    {
        // Save scene & player position
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerController.instance.transform.position.z);

        // Save gold amount
        PlayerPrefs.SetInt($"CurrentGold", currentGold);

        // Save character stats
        for (int i = 0; i < characterStats.Length; i++)
        {
            string playerName = characterStats[i].characterName;

            PlayerPrefs.SetInt($"Player_{playerName}_active", characterStats[i].gameObject.activeInHierarchy ? 1 : 0);
            PlayerPrefs.SetInt($"Player_{playerName}_Level", characterStats[i].level);
            PlayerPrefs.SetInt($"Player_{playerName}_CurrentExp", characterStats[i].currentEXP);
            PlayerPrefs.SetInt($"Player_{playerName}_CurrentHP", characterStats[i].currentHP);
            PlayerPrefs.SetInt($"Player_{playerName}_CurrentMP", characterStats[i].currentMP);
            PlayerPrefs.SetInt($"Player_{playerName}_MaxHP", characterStats[i].maxHP);
            PlayerPrefs.SetInt($"Player_{playerName}_MaxMP", characterStats[i].maxMP);
            PlayerPrefs.SetInt($"Player_{playerName}_Strength", characterStats[i].strength);
            PlayerPrefs.SetInt($"Player_{playerName}_Defense", characterStats[i].defense);
            PlayerPrefs.SetInt($"Player_{playerName}_WeaponPower", characterStats[i].weaponPower);
            PlayerPrefs.SetInt($"Player_{playerName}_ArmorPower", characterStats[i].armorPower);
            PlayerPrefs.SetString($"Player_{playerName}_EquippedWeapon", characterStats[i].equippedWeapon);
            PlayerPrefs.SetString($"Player_{playerName}_EquippedArmor", characterStats[i].equippedArmor);
        }

        // Save inventory data
        for (int i = 0; i < itemsInIventory.Length; i++)
        {
            PlayerPrefs.SetString($"ItemInInventory_{i}", itemsInIventory[i]);
            PlayerPrefs.SetInt($"ItemAmount_{i}", numberOfItems[i]);
        }
    }

    public void LoadData()
    {
        PlayerController.instance.transform.position = new Vector3(
            PlayerPrefs.GetFloat("Player_Position_x"),
            PlayerPrefs.GetFloat("Player_Position_y"),
            PlayerPrefs.GetFloat("Player_Position_z")
        );

        // Load gold amount
        currentGold = PlayerPrefs.GetInt($"CurrentGold");

        // Loading character stats
        for (int i = 0; i < characterStats.Length; i++)
        {
            string playerName = characterStats[i].characterName;

            characterStats[i].gameObject.SetActive((PlayerPrefs.GetInt($"Player_{characterStats[i].characterName}_active") == 1) ? true : false);
            characterStats[i].level = PlayerPrefs.GetInt($"Player_{playerName}_Level");
            characterStats[i].currentEXP = PlayerPrefs.GetInt($"Player_{playerName}_CurrentExp");
            characterStats[i].currentHP = PlayerPrefs.GetInt($"Player_{playerName}_CurrentHP");
            characterStats[i].currentMP = PlayerPrefs.GetInt($"Player_{playerName}_CurrentMP");
            characterStats[i].maxHP = PlayerPrefs.GetInt($"Player_{playerName}_MaxHP");
            characterStats[i].maxMP = PlayerPrefs.GetInt($"Player_{playerName}_MaxMP");
            characterStats[i].strength = PlayerPrefs.GetInt($"Player_{playerName}_Strength");
            characterStats[i].defense = PlayerPrefs.GetInt($"Player_{playerName}_Defense");
            characterStats[i].weaponPower = PlayerPrefs.GetInt($"Player_{playerName}_WeaponPower");
            characterStats[i].armorPower = PlayerPrefs.GetInt($"Player_{playerName}_ArmorPower");
            characterStats[i].equippedWeapon = PlayerPrefs.GetString($"Player_{playerName}_EquippedWeapon");
            characterStats[i].equippedArmor = PlayerPrefs.GetString($"Player_{playerName}_EquippedArmor");
        }

        // Loading character inventory items
        for (int i = 0; i < itemsInIventory.Length; i++)
        {
            itemsInIventory[i] = PlayerPrefs.GetString($"ItemInInventory_{i}");
            numberOfItems[i] = PlayerPrefs.GetInt($"ItemAmount_{i}");
        }
    }
}
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;

    [Header("General information")]
    public string itemName;
    public string description;
    public int value;

    [Header("Item image")]
    public Sprite itemSprite;

    [Header("Effect amount and type")]
    public int amountToChange;
    public bool effectHP, effectMP, effectStrength, effectDefense;

    [Header("Armor/weapon stats")]
    public int weaponStrength;
    public int armorStrength;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

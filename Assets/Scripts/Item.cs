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

    public void Use(int characterIndex)
    {
        CharacterStats selectedCharacter = GameManager.instance.characterStats[characterIndex];

        if (isItem)
        {
            if (effectHP)
            {
                selectedCharacter.currentHP += amountToChange;

                if (selectedCharacter.currentHP > selectedCharacter.maxHP)
                {
                    selectedCharacter.currentHP = selectedCharacter.maxHP;
                }
            }
            if (effectMP)
            {
                selectedCharacter.currentMP += amountToChange;

                if (selectedCharacter.currentMP > selectedCharacter.maxMP)
                {
                    selectedCharacter.currentMP = selectedCharacter.maxMP;
                }
            }
            if (effectStrength)
            {
                selectedCharacter.strength += amountToChange;
            }
            if (effectDefense)
            {
                selectedCharacter.defense += amountToChange;
            }
        }
        if (isWeapon)
        {
            if (selectedCharacter.equippedWeapon != "")
            {
                // Add currently equipped weapon back to inventory
                GameManager.instance.AddItem(selectedCharacter.equippedWeapon);
            }
            // Equip new weapon
            selectedCharacter.equippedWeapon = itemName;
            selectedCharacter.weaponPower = weaponStrength;
        }
        if (isArmor)
        {
            if (selectedCharacter.equippedArmor != "")
            {
                // Add currently equipped armor back to inventory
                GameManager.instance.AddItem(selectedCharacter.equippedArmor);
            }
            // Equip new armor
            selectedCharacter.equippedArmor = itemName;
            selectedCharacter.armorPower = armorStrength;
        }

        // Remove (or decrease amount inv.) item from inventory
        GameManager.instance.RemoveItem(itemName);
    }
}

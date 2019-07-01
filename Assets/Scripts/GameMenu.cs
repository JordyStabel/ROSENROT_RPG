using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject gameMenu;
    public GameObject[] windows;
    private CharacterStats[] characterStats;
    public Text[] nameText, hpText, mpText, levelText, expText;
    public Slider[] expSlider;
    public Image[] characterImage;
    public GameObject[] characterStatHolder;

    public GameObject[] statusButtons;

    public Text statName, statHP, statMP, statStength, statDefense, statWeaponEquipped, statWeaponPower, statArmorEquipped, statArmorPower, statExp;

    public Image statImage;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (gameMenu.activeInHierarchy)
            {
                CloseMenu();
            }
            else
            {
                gameMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }

    public void UpdateMainStats()
    {
        characterStats = GameManager.instance.characterStats;

        for (int i = 0; i < characterStats.Length; i++)
        {
            if (characterStats[i].gameObject.activeInHierarchy)
            {
                characterStatHolder[i].SetActive(true);

                nameText[i].text = characterStats[i].characterName;
                hpText[i].text = $"HP: {characterStats[i].currentHP}/{characterStats[i].maxHP}";
                hpText[i].text = $"MP: {characterStats[i].currentMP}/{characterStats[i].maxMP}";
                levelText[i].text = $"Level: {characterStats[i].level}";
                expText[i].text = $"{characterStats[i].currentEXP}/{characterStats[i].expToNextLevel[characterStats[i].level]}";
                expSlider[i].maxValue = characterStats[i].expToNextLevel[characterStats[i].level];
                expSlider[i].value = characterStats[i].currentEXP;
                characterImage[i].sprite = characterStats[i].characterImage;
            }
            else
            {
                characterStatHolder[i].SetActive(false);
            }
        }
    }

    public void ToggleWindow(int windowIndex)
    {
        UpdateMainStats();

        for (int i = 0; i < windows.Length; i++)
        {
            if (i == windowIndex)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        gameMenu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
    }

    public void OpenStats()
    {
        UpdateMainStats();

        // Update all information that will be shown
        ShowCharacterStats(0);

        for (int i = 0; i < statusButtons.Length; i++)
        {
            statusButtons[i].SetActive(characterStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = characterStats[i].characterName;
        }
    }

    public void ShowCharacterStats(int index)
    {
        statName.text = characterStats[index].characterName;
        statHP.text = $"{characterStats[index].currentHP}/{characterStats[index].maxHP}";
        statMP.text = $"{characterStats[index].currentMP}/{characterStats[index].maxMP}";
        statStength.text = characterStats[index].strength.ToString();
        statDefense.text = characterStats[index].defense.ToString();
        statWeaponEquipped.text = characterStats[index].equippedWeapon != "" ? "None" : characterStats[index].equippedWeapon;
        statWeaponPower.text = characterStats[index].weaponPower.ToString();
        statArmorEquipped.text = characterStats[index].equippedArmor != "" ? "None" : characterStats[index].equippedArmor;
        statArmorPower.text = characterStats[index].armorPower.ToString();
        statExp.text = (characterStats[index].expToNextLevel[characterStats[index].level] - characterStats[index].currentEXP).ToString();
        statImage.sprite = characterStats[index].characterImage;
    }
}

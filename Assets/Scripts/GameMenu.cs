using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject gameMenu;
    private CharacterStats[] characterStats;
    public Text[] nameText, hpText, mpText, levelText, expText;
    public Slider[] expSlider;
    public Image[] characterImage;
    public GameObject[] characterStatHolder;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (gameMenu.activeInHierarchy)
            {
                gameMenu.SetActive(false);
                GameManager.instance.gameMenuOpen = false;
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
                hpText[i].text = $"HP: {characterStats[i].currentHP} / {characterStats[i].maxHP}";
                hpText[i].text = $"MP: {characterStats[i].currentMP} / {characterStats[i].maxMP}";
                levelText[i].text = $"Level: {characterStats[i].level}";
                expText[i].text = $"{characterStats[i].currentEXP} / {characterStats[i].expToNextLevel[characterStats[i].level]}";
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
}

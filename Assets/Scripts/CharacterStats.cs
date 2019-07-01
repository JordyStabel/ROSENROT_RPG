using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
    public string characterName;
    public int level = 1;
    public int currentEXP;
    public int[] expToNextLevel;
    public int maxLevel = 100;
    public int baseEXP = 1000;

    public int currentHP;
    public int maxHP = 100;
    public int currentMP;
    public int maxMP = 30;
    public int[] mpLevelBonus;
    public int strength;
    public int defense;
    public int weaponPower;
    public int armorPower;
    public string equippedWeapon;
    public string equippedArmor;
    public Sprite characterImage;

    // Start is called before the first frame update
    void Start () {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        mpLevelBonus = new int[maxLevel];

        for (int i = 2; i < expToNextLevel.Length; i++) {
            expToNextLevel[i] = Mathf.FloorToInt (expToNextLevel[i - 1] * 1.05f);

            if (i % 3 == 0) {
                mpLevelBonus[i] = Mathf.FloorToInt (Mathf.Pow (i, 1.35f));
            }
        }
    }

    // Update is called once per frame
    void Update () {
        // For testing only!
        if (Input.GetKey (KeyCode.K)) {
            AddExp (250);
        }
    }

    public void AddExp (int expToAdd) {
        currentEXP += expToAdd;

        if (level < maxLevel) {
            if (currentEXP >= expToNextLevel[level]) {
                currentEXP -= expToNextLevel[level];
                level++;

                // Determine whether to increase strenght or defense, depening on odd or even level
                if (level % 2 == 0) {
                    strength++;
                } else {
                    defense++;
                }

                maxHP = Mathf.FloorToInt (maxHP * 1.05f);
                currentHP = maxHP;

                maxMP += mpLevelBonus[level];
                currentMP = maxMP;
            }
        }

        if (level >= maxLevel) {
            currentEXP = 0;
        }
    }
}
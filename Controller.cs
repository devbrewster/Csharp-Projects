using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BreakInfinity;
using System.Linq; // needed for creating Lists

public class Controller : MonoBehaviour
{
    public static Controller instance;

    private void Awake() => instance = this;

    public Data data; // Class Holder for all data information

    [SerializeField] public TMP_Text cashText; // Cash TMP Text Display
    [SerializeField] public TMP_Text playerLevelDisplay; // Player Level Display
    [SerializeField] public TMP_Text playerExperienceDisplay; // Player Experience Display
    [SerializeField] public TMP_Text clickUpgradeLevelDisplay; // Upgrade Levels

    public BigDouble ClickPower()
    {
        BigDouble total = 0;
        for(int i = 0; i < data.clickUpgradeLevel.Count; i++)
        {
            total += UpgradesManager.instance.clickUpgradesBasePower[i] * data.clickUpgradeLevel[i];
        }

        return total;
    }
    


    //create levels
    public List<BigDouble> upgradeLevels;


    private void Start() // any nesseccary data is started at the beginning of the first frame
    {
        upgradeLevels = CreateList<BigDouble>(2);


        data = new Data(); // Starts all data at the start of the game application
        var upgradesManager = UpgradesManager.instance;
        upgradesManager.StartUpgradeManager();
    }

    private void Update() // All is Updated according to functions
    {
        cashText.text = " $" + data.cash.ToString("F2");
        playerLevelDisplay.text = "Rank: " + data.playerLevels;
        playerExperienceDisplay.text = "XP: " + data.playerExperience;
        clickUpgradeLevelDisplay.text = "Upgrade: " + data.clickUpgradeLevel;


        // Increase Levels when cash hits a certain level

    }

    public void addShibroki()
    {
        data.cash += ClickPower();
    }

    //Create Lists and Returns the method | Used for storing levels, XP bars 
    public List<T> CreateList<T>(int capacity) => Enumerable.Repeat(default(T), capacity).ToList();

}
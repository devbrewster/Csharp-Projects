using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;
using UnityEngine.UI;


public class UpgradesManager : MonoBehaviour
{

    public static UpgradesManager instance;
    private void Awake() => instance = this;

    public List<Upgrades> clickUpgrades;
    public Upgrades clickUpgradePrefab;

    public ScrollRect clickUpgradesScroll;
    public GameObject clickUpgradesPanel; // used for holding upgrades

    public string[] clickUpgradeNames;

    public BigDouble[] clickUpgradeBaseCost;
    public BigDouble[] clickUpgradeCostMult;
    public BigDouble[] clickUpgradesBasePower; // adding to the default clicklevel power


    public void StartUpgradeManager()
    {
        clickUpgradeNames = new [] {"Click Power +1", "Click Power +10", "Click Power +15"};
        clickUpgradeBaseCost = new BigDouble[] {10, 50, 100}; // base cost for upgrades
        clickUpgradeCostMult = new BigDouble[] {1.25, 1.35, 1.55 }; // power each upgrade is Multiplied by
        clickUpgradesBasePower = new BigDouble[] {1, 5, 10 };

        for (int i = 0; i < Controller.instance.data.clickUpgradeLevel.Count;)
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel.transform);
            upgrade.UpgradeID = i;
            clickUpgrades.Add(upgrade); //Adds the prefab upgrades to the list
        }

        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);

        UpdateClickUpgradeUI(); // Base Upgrade loads at the start of the first frame
    }

    public void UpdateClickUpgradeUI(int UpgradeID = -1) // loops through all of the Upgrades
    {
        var data = Controller.instance.data;
        if (UpgradeID == -1)
        {
            for(int i = 0; i < clickUpgrades.Count; i++) UpdateUI(i);
            
        } else UpdateUI(UpgradeID);
        
        void UpdateUI(int ID)
        {
            clickUpgrades[ID].UpgradeLevel.text = " Level: " + data.clickUpgradeLevel[ID].ToString();
            clickUpgrades[ID].UpgradeCost.text = $"Cost: ${clickUpgradeCost(ID):F2}"; //Cost() returns whatever is passed
            clickUpgrades[ID].UpgradeText.text = clickUpgradeNames[ID];
        }
 
    }


    public BigDouble clickUpgradeCost(int UpgradeID) => clickUpgradeBaseCost[UpgradeID] * BigDouble.Pow(clickUpgradeCostMult[UpgradeID], Controller.instance.data.clickUpgradeLevel[UpgradeID]); //Onliner code can use '=>'


    public void BuyUpgrade(int UpgradeID)
    {
        var data = Controller.instance.data;
        if(data.cash >= clickUpgradeCost(UpgradeID))
        {
            data.cash -= clickUpgradeCost(UpgradeID);
            data.clickUpgradeLevel[UpgradeID] += 1;
        }
        UpdateClickUpgradeUI(UpgradeID); // Updates once you buy an upgrade
    }


}

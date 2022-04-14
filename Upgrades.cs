using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BreakInfinity;

public class Upgrades : MonoBehaviour
{

    public int UpgradeID;

    public Image Upgrade; // Prefab Upgrade Image
    public TMP_Text UpgradeText; // Prefab Button Text
    public TMP_Text UpgradeLevel; // Prefab Level Text
    public TMP_Text UpgradeCost; // Prefab Cost Text


    public void BuyClickUpgrade() => UpgradesManager.instance.BuyUpgrade(UpgradeID);

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;


public class Data
{
    public BigDouble cash;
    public BigDouble currentCash;
    public BigDouble playerLevels;  //Used for Player Leveling
    public BigDouble playerExperience;
    public BigDouble newPlayerExperience;

    public List<BigDouble> clickUpgradeLevel;


    public Data()
    {
        clickUpgradeLevel = Methods.CreateList<BigDouble>(capacity: 3); // creates three items for the list
    }
}

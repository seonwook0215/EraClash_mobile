using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoldierManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI swordCount;
    [SerializeField] private TextMeshProUGUI archerCount;
    [SerializeField] private TextMeshProUGUI lancerCount;
    [SerializeField] private TextMeshProUGUI shieldCount;
    [SerializeField] private GameObject mainCanvas;
    public GameObject soldierCanvas;
    public static SoldierManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void clickSoldierButton()
    {
        updateInfantry();
        mainCanvas.SetActive(false);
        soldierCanvas.SetActive(true);
    }
    public void clickSoldierXButton()
    {
        mainCanvas.SetActive(true);
        soldierCanvas.SetActive(false);
    }
    public void updateInfantry()
    {
        //PUnitManager.instance.P_units.Count.ToString()+"\nArcher: "+ PUnitManager.instance.A_units.Count.ToString() + "\nLancer: " + PUnitManager.instance.L_units.Count.ToString() + "\nShieldBearer: " + PUnitManager.instance.S_units.Count.ToString();
        swordCount.text = PUnitManager.instance.Paladin.ToString()+ " Sword Infantry";
        archerCount.text = PUnitManager.instance.Archer.ToString() + " Archer Infantry";
        lancerCount.text = PUnitManager.instance.Lancer.ToString() + " Lancer Infantry";
        shieldCount.text = PUnitManager.instance.Shield.ToString() + " Shield Infantry";
    }

}

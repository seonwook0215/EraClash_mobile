using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AttackManager : MonoBehaviour
{
    [SerializeField]private GameObject bannedHeal;
    [SerializeField] private GameObject bannedRage;
    [SerializeField] private GameObject bannedArdrenaline;
    [SerializeField] private GameObject bannedMercenary;
    [SerializeField] private TextMeshProUGUI playerUnits;
    [SerializeField] private TextMeshProUGUI enemyUnits;
    [SerializeField] private GameObject playerHPbar;
    [SerializeField] private GameObject enemyHPbar;
    
    private float playerFullHp = 0f;
    private float enemyFullHp = 0f;
    private float playerCurrentHp = 0f;
    private float enemyCurrentHp = 0f;
    public static AttackManager instance;
    
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (TurnManager.instance.StartWar)
        {
            checkCurrentHPAmount();
            updateSoldierAmount();
        }
    }
    private void updateSoldierAmount()
    {
        playerUnits.text= "Sword : "+PUnitManager.instance.P_units.Count.ToString()+"\nArcher: "+ PUnitManager.instance.A_units.Count.ToString() + "\nLancer: " + PUnitManager.instance.L_units.Count.ToString() + "\nShieldBearer: " + PUnitManager.instance.S_units.Count.ToString();
        enemyUnits.text = "Sword : " + EUnitManager.instance.P_units.Count.ToString() + "\nArcher: " + EUnitManager.instance.A_units.Count.ToString() + "\nLancer: " + EUnitManager.instance.L_units.Count.ToString() + "\nShieldBearer: " + EUnitManager.instance.S_units.Count.ToString();
    }
    public void checkFullHPAmount()
    {
        Debug.Log("½ÇÇà");
        checkBanResearchSkill();
        playerFullHp = PUnitManager.instance.Paladin * 20f + PUnitManager.instance.Archer * 10f + PUnitManager.instance.Shield * 50f + PUnitManager.instance.Lancer * 30f;
        enemyFullHp  = EUnitManager.instance.Paladin * 20f + EUnitManager.instance.Archer * 10f + EUnitManager.instance.Shield * 50f + EUnitManager.instance.Lancer * 30f;
        
    }
   
    public void checkCurrentHPAmount()
    {
        
        playerCurrentHp = 0;
        enemyCurrentHp = 0;
        for (int i = 0; i < PUnitManager.instance.P_units.Count; i++)
        {
            if (PUnitManager.instance.P_units[i].GetComponent<Life>().amount < 0f)
            {

            }
            else
            {
                playerCurrentHp += PUnitManager.instance.P_units[i].GetComponent<Life>().amount;
            }
        }
        for (int i = 0; i < PUnitManager.instance.A_units.Count; i++)
        {
            if (PUnitManager.instance.A_units[i].GetComponent<Life>().amount < 0f)
            {

            }
            else
            {
                playerCurrentHp += PUnitManager.instance.A_units[i].GetComponent<Life>().amount;
            }
        }
        for (int i = 0; i < PUnitManager.instance.L_units.Count; i++)
        {
            if (PUnitManager.instance.L_units[i].GetComponent<Life>().amount < 0f)
            {

            }
            else
            {
                playerCurrentHp += PUnitManager.instance.L_units[i].GetComponent<Life>().amount;
            }
        }
        for (int i = 0; i < PUnitManager.instance.S_units.Count; i++)
        {
            if (PUnitManager.instance.S_units[i].GetComponent<Life>().amount < 0f)
            {

            }
            else
            {
                playerCurrentHp += PUnitManager.instance.S_units[i].GetComponent<Life>().amount;
            }
        }

        for (int i = 0; i < EUnitManager.instance.P_units.Count; i++)
        {
            if (EUnitManager.instance.P_units[i].GetComponent<Life>().amount < 0f)
            {

            }
            else
            {
                enemyCurrentHp += EUnitManager.instance.P_units[i].GetComponent<Life>().amount;
            }
        }
        for (int i = 0; i < EUnitManager.instance.A_units.Count; i++)
        {
            if (EUnitManager.instance.A_units[i].GetComponent<Life>().amount < 0f)
            {

            }
            else
            {
                enemyCurrentHp += EUnitManager.instance.A_units[i].GetComponent<Life>().amount;
            }
        }
        for (int i = 0; i < EUnitManager.instance.L_units.Count; i++)
        {
            if (EUnitManager.instance.L_units[i].GetComponent<Life>().amount < 0f)
            {

            }
            else
            {
                enemyCurrentHp += EUnitManager.instance.L_units[i].GetComponent<Life>().amount;
            }
        }
        for (int i = 0; i < EUnitManager.instance.S_units.Count; i++)
        {
            if (EUnitManager.instance.S_units[i].GetComponent<Life>().amount < 0f)
            {

            }
            else
            {
                enemyCurrentHp += EUnitManager.instance.S_units[i].GetComponent<Life>().amount;
            }
        }
        playerHPbar.GetComponent<Image>().fillAmount = playerCurrentHp / playerFullHp;
        enemyHPbar.GetComponent<Image>().fillAmount = enemyCurrentHp / enemyFullHp;

        Debug.Log(enemyCurrentHp);
        Debug.Log(enemyCurrentHp / enemyFullHp);
        
    }
    private void checkBanResearchSkill()
    {
        if (ResearchManager.instance.isAbleHeal)
        {
            bannedHeal.SetActive(false);
        }
        if (ResearchManager.instance.isAbleRage)
        {
            bannedRage.SetActive(false);
        }
        if (ResearchManager.instance.isAbleArdrenaline)
        {
            bannedArdrenaline.SetActive(false);
        }
        if (ResearchManager.instance.isAbleMercenary)
        {
            bannedMercenary.SetActive(false);
        }
    }
    public void clickHealButton()
    {
        for (int i = 0; i < PUnitManager.instance.P_units.Count; i++)
        {
            PUnitManager.instance.P_units[i].GetComponent<Life>().amount+=20;
        }
        for (int i = 0; i < PUnitManager.instance.A_units.Count; i++)
        {
            PUnitManager.instance.A_units[i].GetComponent<Life>().amount += 20;
        }
        for (int i = 0; i < PUnitManager.instance.L_units.Count; i++)
        {
            PUnitManager.instance.L_units[i].GetComponent<Life>().amount += 20;
        }
        for (int i = 0; i < PUnitManager.instance.S_units.Count; i++)
        {
            PUnitManager.instance.S_units[i].GetComponent<Life>().amount += 20;
        }

        bannedHeal.SetActive(true);
    }
    public void clickRageButton()
    {
        bannedRage.SetActive(true);
    }
    public void clickArdrenalineButton()
    {
        bannedArdrenaline.SetActive(true);
    }
    public void clickMercenaryButton()
    {
        bannedMercenary.SetActive(true);
    }
}

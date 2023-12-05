using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private GameObject bannedHeal;
    [SerializeField] private GameObject bannedRage;
    [SerializeField] private GameObject bannedArdrenaline;
    [SerializeField] private GameObject bannedMercenary;
    [SerializeField] private TextMeshProUGUI playerUnits;
    [SerializeField] private TextMeshProUGUI enemyUnits;
    [SerializeField] private GameObject playerHPbar;
    [SerializeField] private GameObject enemyHPbar;
    [SerializeField] private GameObject fastForwardObj;
    [SerializeField] private GameObject normalForwardObj;

    private float playerFullHp = 0f;
    private float enemyFullHp = 0f;
    private float playerCurrentHp = 0f;
    private float enemyCurrentHp = 0f;
    private string manyPlayerUnit;
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
        playerUnits.text = "Sword : " + PUnitManager.instance.P_units.Count.ToString() + "\nArcher: " + PUnitManager.instance.A_units.Count.ToString() + "\nLancer: " + PUnitManager.instance.L_units.Count.ToString() + "\nShieldBearer: " + PUnitManager.instance.S_units.Count.ToString();
        enemyUnits.text = "Sword : " + EUnitManager.instance.P_units.Count.ToString() + "\nArcher: " + EUnitManager.instance.A_units.Count.ToString() + "\nLancer: " + EUnitManager.instance.L_units.Count.ToString() + "\nShieldBearer: " + EUnitManager.instance.S_units.Count.ToString();
    }
    public void checkFullHPAmount()
    {
        Debug.Log("����");
        checkBanResearchSkill();
        playerFullHp = PUnitManager.instance.Paladin * 30f + PUnitManager.instance.Archer * 20f + PUnitManager.instance.Shield * 50f + PUnitManager.instance.Lancer * 40f;
        enemyFullHp = EUnitManager.instance.Paladin * 30f + EUnitManager.instance.Archer * 20f + EUnitManager.instance.Shield * 50f + EUnitManager.instance.Lancer * 40f;
        lotsofUnit();
        Debug.Log(manyPlayerUnit);
         
    }
    private void lotsofUnit()
    {
        
        if (PUnitManager.instance.Paladin >= PUnitManager.instance.Archer && PUnitManager.instance.Paladin >= PUnitManager.instance.Lancer && PUnitManager.instance.Paladin >= PUnitManager.instance.Shield)
        {
            EnemyAI.Instance.PlayermainUnit = "Sword";
            
        }
        else if (PUnitManager.instance.Archer >= PUnitManager.instance.Paladin && PUnitManager.instance.Archer >= PUnitManager.instance.Lancer && PUnitManager.instance.Archer >= PUnitManager.instance.Shield)
        {
            EnemyAI.Instance.PlayermainUnit = "Archer";
            
        }
        else if (PUnitManager.instance.Lancer >= PUnitManager.instance.Archer && PUnitManager.instance.Lancer >= PUnitManager.instance.Paladin && PUnitManager.instance.Lancer >= PUnitManager.instance.Shield)
        {
            EnemyAI.Instance.PlayermainUnit = "Spear";
        }
        else if (PUnitManager.instance.Shield >= PUnitManager.instance.Archer && PUnitManager.instance.Shield >= PUnitManager.instance.Lancer && PUnitManager.instance.Shield >= PUnitManager.instance.Paladin)
        {
            EnemyAI.Instance.PlayermainUnit = "Shield";
        }
        else
        {
            EnemyAI.Instance.PlayermainUnit = "None";
        }
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
        
  

    }

    public void clickFastForward()
    {
        Time.timeScale = 2f;
        fastForwardObj.SetActive(false);
        normalForwardObj.SetActive(true);
    }

    public void clickNormalForward()
    {
        Time.timeScale = 1f;
        fastForwardObj.SetActive(true);
        normalForwardObj.SetActive(false);
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
            PUnitManager.instance.P_units[i].GetComponent<Life>().amount += 20;
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
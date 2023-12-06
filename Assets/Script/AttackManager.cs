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
    public bool useArdrenaline = false;
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
        useArdrenaline = false;
        Debug.Log("실행");
        checkBanResearchSkill();
        playerFullHp = 0;
        enemyFullHp = 0;
        if (PUnitManager.instance.Paladin >= 20)
        {
            playerFullHp += 20 * 30f;
        }
        else
        {
            playerFullHp += PUnitManager.instance.Paladin * 30f;
        }
        if (PUnitManager.instance.Archer >= 20)
        {
            playerFullHp += 20 * 20f;
        }
        else
        {
            playerFullHp += PUnitManager.instance.Archer * 20f;
        }
        if (PUnitManager.instance.Lancer >= 20)
        {
            playerFullHp += 20 * 40f;
        }
        else
        {
            playerFullHp += PUnitManager.instance.Lancer * 40f;
        }
        if (PUnitManager.instance.Shield >= 20)
        {
            playerFullHp += 20 * 50f;
        }
        else
        {
            playerFullHp += PUnitManager.instance.Shield * 30f;
        }

        if (EUnitManager.instance.Paladin >= 20)
        {
            enemyFullHp += 20 * 30f;
        }
        else
        {
            enemyFullHp += EUnitManager.instance.Paladin * 30f;
        }
        if (EUnitManager.instance.Archer >= 20)
        {
            enemyFullHp += 20 * 20f;
        }
        else
        {
            enemyFullHp += EUnitManager.instance.Archer * 20f;
        }
        if (EUnitManager.instance.Lancer >= 20)
        {
            enemyFullHp += 20 * 40f;
        }
        else
        {
            enemyFullHp += EUnitManager.instance.Lancer * 40f;
        }
        if (EUnitManager.instance.Shield >= 20)
        {
            enemyFullHp += 20 * 50f;
        }
        else
        {
            enemyFullHp += EUnitManager.instance.Shield * 30f;
        }
        lotsofUnit();

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
            if (PUnitManager.instance.P_units[i].GetComponent<Life>().amount + 15f >= 30f)
            {
                PUnitManager.instance.P_units[i].GetComponent<Life>().amount = 30f;
            }
            else
            {
                PUnitManager.instance.P_units[i].GetComponent<Life>().amount += 15f;
            }
        }
        for (int i = 0; i < PUnitManager.instance.A_units.Count; i++)
        {
            
            if (PUnitManager.instance.A_units[i].GetComponent<Life>().amount + 10f >= 20f)
            {
                PUnitManager.instance.A_units[i].GetComponent<Life>().amount = 20f;
            }
            else
            {
                PUnitManager.instance.A_units[i].GetComponent<Life>().amount += 10f;
            }
        }
        for (int i = 0; i < PUnitManager.instance.L_units.Count; i++)
        {
            
            if (PUnitManager.instance.L_units[i].GetComponent<Life>().amount + 20f >= 40f)
            {
                PUnitManager.instance.L_units[i].GetComponent<Life>().amount = 40f;
            }
            else
            {
                PUnitManager.instance.L_units[i].GetComponent<Life>().amount += 20f;
            }
        }
        for (int i = 0; i < PUnitManager.instance.S_units.Count; i++)
        {
            
            if (PUnitManager.instance.S_units[i].GetComponent<Life>().amount + 25f >= 50f)
            {
                PUnitManager.instance.S_units[i].GetComponent<Life>().amount = 50f;
            }
            else
            {
                PUnitManager.instance.S_units[i].GetComponent<Life>().amount = 25f;
            }
        }

        bannedHeal.SetActive(true);
    }
    public void clickRageButton()
    {
        for (int i = 0; i < PUnitManager.instance.P_units.Count; i++)
        {
            PUnitManager.instance.P_units[i].GetComponent<Unit_FSM>().damage *= 1.2f;
            PUnitManager.instance.P_units[i].GetComponent<Unit_FSM>().attackRate *= 0.8f;
        }
        for (int i = 0; i < PUnitManager.instance.A_units.Count; i++)
        {
            PUnitManager.instance.A_units[i].GetComponent<Unit_FSM>().damage *= 1.2f;
            PUnitManager.instance.A_units[i].GetComponent<Unit_FSM>().attackRate *= 0.8f;
        }
        for (int i = 0; i < PUnitManager.instance.L_units.Count; i++)
        {
            PUnitManager.instance.L_units[i].GetComponent<Unit_FSM>().damage *= 1.2f;
            PUnitManager.instance.L_units[i].GetComponent<Unit_FSM>().attackRate *= 0.8f;
        }
        for (int i = 0; i < PUnitManager.instance.S_units.Count; i++)
        {
            PUnitManager.instance.S_units[i].GetComponent<Unit_FSM>().damage *= 1.2f;
            PUnitManager.instance.S_units[i].GetComponent<Unit_FSM>().attackRate *= 0.8f;
        }
        bannedRage.SetActive(true);
        Debug.Log("화가난다");
        StartCoroutine(rageWait());
    }
    IEnumerator rageWait()
    {
        yield return new WaitForSecondsRealtime(10.0f);
        for (int i = 0; i < PUnitManager.instance.P_units.Count; i++)
        {
            PUnitManager.instance.P_units[i].GetComponent<Unit_FSM>().damage *= 0.84f;
            PUnitManager.instance.P_units[i].GetComponent<Unit_FSM>().attackRate *= 1.25f;
        }
        for (int i = 0; i < PUnitManager.instance.A_units.Count; i++)
        {
            PUnitManager.instance.A_units[i].GetComponent<Unit_FSM>().damage *= 0.84f;
            PUnitManager.instance.A_units[i].GetComponent<Unit_FSM>().attackRate *= 1.25f;
        }
        for (int i = 0; i < PUnitManager.instance.L_units.Count; i++)
        {
            PUnitManager.instance.L_units[i].GetComponent<Unit_FSM>().damage *= 0.84f;
            PUnitManager.instance.L_units[i].GetComponent<Unit_FSM>().attackRate *= 1.25f;
        }
        for (int i = 0; i < PUnitManager.instance.S_units.Count; i++)
        {
            PUnitManager.instance.S_units[i].GetComponent<Unit_FSM>().damage *= 0.84f;
            PUnitManager.instance.S_units[i].GetComponent<Unit_FSM>().attackRate *= 1.25f;
        }
        Debug.Log("화가안난다");
    }
    public void clickArdrenalineButton()
    {
        useArdrenaline = true;
        bannedArdrenaline.SetActive(true);
    }
    IEnumerator ardrenalineWait()
    {
        yield return new WaitForSecondsRealtime(6f);
        useArdrenaline = false;
    }
    public void clickMercenaryButton()
    {
        bannedMercenary.SetActive(true);
    }
}
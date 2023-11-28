using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
        
    }
    public void checkFullHPAmount()
    {
        for(int i=0;i<PUnitManager.instance.P_units.Count; i++)
        {
            playerFullHp += PUnitManager.instance.P_units[i].GetComponent<Life>().amount;
        }
        for (int i = 0; i < PUnitManager.instance.A_units.Count; i++)
        {
            playerFullHp += PUnitManager.instance.A_units[i].GetComponent<Life>().amount;
        }
        for (int i = 0; i < PUnitManager.instance.L_units.Count; i++)
        {
            playerFullHp += PUnitManager.instance.L_units[i].GetComponent<Life>().amount;
        }
        for (int i = 0; i < PUnitManager.instance.S_units.Count; i++)
        {
            playerFullHp += PUnitManager.instance.S_units[i].GetComponent<Life>().amount;
        }

        for (int i = 0; i < EUnitManager.instance.P_units.Count; i++)
        {
            enemyFullHp += EUnitManager.instance.P_units[i].GetComponent<Life>().amount;
        }
        for (int i = 0; i < EUnitManager.instance.A_units.Count; i++)
        {
            enemyFullHp += EUnitManager.instance.A_units[i].GetComponent<Life>().amount;
        }
        for (int i = 0; i < EUnitManager.instance.L_units.Count; i++)
        {
            enemyFullHp += EUnitManager.instance.L_units[i].GetComponent<Life>().amount;
        }
        for (int i = 0; i < EUnitManager.instance.S_units.Count; i++)
        {
            enemyFullHp += EUnitManager.instance.S_units[i].GetComponent<Life>().amount;
        }
    }
    public void checkCurrentHPAmount()
    {
        for (int i = 0; i < PUnitManager.instance.P_units.Count; i++)
        {
            playerCurrentHp += PUnitManager.instance.P_units[i].GetComponent<Life>().amount;
        }
        for (int i = 0; i < PUnitManager.instance.A_units.Count; i++)
        {
            playerCurrentHp += PUnitManager.instance.A_units[i].GetComponent<Life>().amount;
        }
        for (int i = 0; i < PUnitManager.instance.L_units.Count; i++)
        {
            playerCurrentHp += PUnitManager.instance.L_units[i].GetComponent<Life>().amount;
        }
        for (int i = 0; i < PUnitManager.instance.S_units.Count; i++)
        {
            playerCurrentHp += PUnitManager.instance.S_units[i].GetComponent<Life>().amount;
        }

        for (int i = 0; i < EUnitManager.instance.P_units.Count; i++)
        {
            enemyCurrentHp += EUnitManager.instance.P_units[i].GetComponent<Life>().amount;
        }
        for (int i = 0; i < EUnitManager.instance.A_units.Count; i++)
        {
            enemyCurrentHp += EUnitManager.instance.A_units[i].GetComponent<Life>().amount;
        }
        for (int i = 0; i < EUnitManager.instance.L_units.Count; i++)
        {
            enemyCurrentHp += EUnitManager.instance.L_units[i].GetComponent<Life>().amount;
        }
        for (int i = 0; i < EUnitManager.instance.S_units.Count; i++)
        {
            enemyCurrentHp += EUnitManager.instance.S_units[i].GetComponent<Life>().amount;
        }
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

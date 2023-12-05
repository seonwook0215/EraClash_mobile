using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class TurnManager: MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] private GameObject messageCanvas;
    private bool isDisplayingMessage = false;
    private int messagecnt = 0;
    public static TurnManager instance;

    public float Day;
    public float Phase;
    private float rotationSpeed = 540f;
    public ParticleSystem SunShine;
    public ParticleSystem MoonShine;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Camera UICamera;
    [SerializeField] private TextMeshProUGUI DayText;
    [SerializeField] private TextMeshProUGUI GoldText;
    [SerializeField] private TextMeshProUGUI GainGoldText;
    [SerializeField] private TextMeshProUGUI ResearchText;
    [SerializeField] private TextMeshProUGUI BuildingText;
    [SerializeField] private GameObject ResearchTab;
    [SerializeField] private GameObject BuildButton;
    [SerializeField] private GameObject ResearchButton;
    [SerializeField] private GameObject SoldierButton;
    [SerializeField] private GameObject EndDayButton;
    [SerializeField] private GameObject AttackButton;
    [SerializeField] private GameObject base_Canvas;
    [SerializeField] private GameObject research_Canvas;
    [SerializeField] private GameObject attack_Canvas;
    [SerializeField] private GameObject day_Canvas;
    public bool Onattack=false;
    public bool EnemyAttack = false;
    public bool StartWar = false;
    private float Paladin;
    private float Lancer;
    private float Archer;
    
    private float R_building;
    private float P_building;
    private float L_building;
    private float A_building;
    //private float Fortress_hp;
    //private float Castle_hp;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Duplicated Turn, ignoring this one", gameObject);
        }
    }
    private void Start()
    {
        Day = 0;
        TurnStart();
    }
    private void Update()
    {
        if (isDisplayingMessage&&messagecnt==0)
        {
            Debug.Log("메시지실행");
            messagecnt = 1;
            StartCoroutine(DisplayMessageForSeconds());
        }
        
    }
    IEnumerator DisplayMessageForSeconds()
    {
        
        yield return new WaitForSecondsRealtime(2);
        {
            
            messageText.text = ""; // 메시지를 숨깁니다.
            isDisplayingMessage = false;
            messageCanvas.SetActive(false);
            messagecnt = 0;
        }
        
    }
    public void ChangeBuildingText()
    {
        BuildingText.text = "Granary: " + (PBuildingManager.instance.R_building.Count + PBuildingManager.instance.Fortress_R_building.Count).ToString() + "\nSword: " + (PBuildingManager.instance.P_building.Count + PBuildingManager.instance.Fortress_P_building.Count).ToString() +
            "\nLancer: " + (PBuildingManager.instance.L_building.Count + PBuildingManager.instance.Fortress_L_building.Count).ToString() + "\nArcher: " + (PBuildingManager.instance.A_building.Count + PBuildingManager.instance.Fortress_A_building.Count).ToString() +
            "\nShieldBearer: " + (PBuildingManager.instance.S_building.Count + PBuildingManager.instance.Fortress_S_building.Count).ToString();
    }
    public void ChangeDay()
    {
        DayText.text = Day+" Day";
    }
    public void ChangeGold()
    {
        GoldText.text = PResourceManager.instance.MP.ToString() + "G";
    }
    public void ChangeGainGold()
    {
        GainGoldText.text = "1D + "+ ((PBuildingManager.instance.R_building.Count + PBuildingManager.instance.Fortress_R_building.Count) * 50 + 50).ToString() + "G";
    }
    public void DoNotHaveMoney()
    {
        messageCanvas.SetActive(true);
        messageText.text = "Not enough Military Provision."; // 표시할 메시지를 여기에 입력
        isDisplayingMessage = true;
    }
    public void ShowMessageCannotAttack0Army()
    {
        messageText.text = "You cannot attack with an army of 0 people."; // 표시할 메시지를 여기에 입력
        isDisplayingMessage = true;
        
    }
    public void ShowMessageCannotAttackDay()
    {
        messageText.text = "You cannot attack. Your soldiers are exhausted"; // 표시할 메시지를 여기에 입력
        isDisplayingMessage = true;
        
    }
    public void TouchBuildButton()
    {
        CameraSwitch.instance.MoveToBuild();
        CraftManual.instance.OpenWindow();
        ResearchTab.SetActive(false);
        BuildButton.SetActive(false);
        ResearchButton.SetActive(false);
        SoldierButton.SetActive(false);
        EndDayButton.SetActive(false);
        AttackButton.SetActive(false);
    }
    public void TouchReturnButton()
    {
        CameraSwitch.instance.MoveToMain();
        CraftManual.instance.CloseWindow();
        ResearchTab.SetActive(true);
        BuildButton.SetActive(true);
        ResearchButton.SetActive(true);
        SoldierButton.SetActive(true);
        EndDayButton.SetActive(true);
        AttackButton.SetActive(true);
    }
    public void TouchResearchButton()
    {
        base_Canvas.SetActive(false);
        research_Canvas.SetActive(true);
    }
    public void TouchSoldierButton()
    {
        SoldierManager.instance.clickSoldierButton();
    }
    public void TouchEndDayButton()
    {
        if (EnemyAttack)
        {
            AttackManager.instance.checkFullHPAmount();
            StartWar = true;
            attack_Canvas.SetActive(true);
            base_Canvas.SetActive(false);
            BattleManager.instance.StartWar();
            
        }
        else
        {
            dayEndWithNoCombat();
            TurnStart();
        }
    }
    public void TouchAttackButton()
    {
        if (PUnitManager.instance.Archer + PUnitManager.instance.Lancer + PUnitManager.instance.Paladin <= 0)
        {
            messageCanvas.SetActive(true);
            ShowMessageCannotAttack0Army();
        }
        else
        {
            if (EnemyAttack)
            {
                StartWar = true;
            }
            AttackManager.instance.checkFullHPAmount();
            attack_Canvas.SetActive(true);
            base_Canvas.SetActive(false);
            Onattack = true;
            BattleManager.instance.StartWar();
        }

    }
    public void TurnStart()
    {
        Day++;
        if (ResearchManager.instance.researchDayLeft > 0)
        {
            ResearchManager.instance.researchDayLeft -= 1;
        }
        PResourceManager.instance.MP += (PBuildingManager.instance.R_building.Count + PBuildingManager.instance.Fortress_R_building.Count) * 50 + 50;
        ChangeDay();
        ChangeGold();
        ChangeGainGold();
        ResearchManager.instance.updateResearchTab();
        PUnitManager.instance.TurnChangeGainArmy();
        EUnitManager.instance.TurnChangeGainArmy();
    }

    private void dayEndWithNoCombat()
    {
        Debug.Log("!!");
        day_Canvas.transform.Find("Moon").gameObject.transform.GetChild(1).gameObject.transform.Rotate(0f, 0f, 0f);
        day_Canvas.transform.Find("Sun").gameObject.transform.GetChild(1).gameObject.transform.Rotate(0f, 0f, 0f);
        MainCamera.enabled=false;
        UICamera.enabled = true;
        StartCoroutine(dayEndWithNoCombat_IEnumerator());

    }
    IEnumerator dayEndWithNoCombat_IEnumerator()
    {
        base_Canvas.SetActive(false);
        day_Canvas.SetActive(true);
        day_Canvas.transform.Find("Sun").gameObject.SetActive(true);
        day_Canvas.transform.Find("Moon").gameObject.SetActive(false);
        day_Canvas.transform.Find("Sun").gameObject.transform.GetChild(0).gameObject.GetComponent<Animation>().Play();
        SunShine.Play();
        yield return new WaitForSecondsRealtime(1f);
        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            day_Canvas.transform.Find("Sun").gameObject.transform.GetChild(1).gameObject.transform.Rotate(0f, 360f * Time.deltaTime, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(0.05f);
        day_Canvas.transform.Find("Sun").gameObject.SetActive(false);
        day_Canvas.transform.Find("Moon").gameObject.SetActive(true);
        day_Canvas.transform.Find("MoonBackground").gameObject.SetActive(true);
        day_Canvas.transform.Find("Moon").gameObject.transform.GetChild(0).gameObject.GetComponent<Animation>().Play();

        SunShine.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        MoonShine.Play();
        elapsedTime = 0f;
        duration = 0.5f;

        while (elapsedTime < duration)
        {
            day_Canvas.transform.Find("Moon").gameObject.transform.GetChild(1).gameObject.transform.Rotate(0f, 720f * Time.deltaTime, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;
        duration = 0.5f;

        while (elapsedTime < duration)
        {
            day_Canvas.transform.Find("Moon").gameObject.transform.GetChild(1).gameObject.transform.Rotate(0f, 360f * Time.deltaTime, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;
        duration = 1.5f;

        while (elapsedTime < duration)
        {
            day_Canvas.transform.Find("Moon").gameObject.transform.GetChild(1).gameObject.transform.Rotate(0f, 180f * Time.deltaTime, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;
        duration = 0.5f;

        while (elapsedTime < duration)
        {
            day_Canvas.transform.Find("Moon").gameObject.transform.GetChild(1).gameObject.transform.Rotate(0f, 360f * Time.deltaTime, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;
        duration = 0.5f;

        while (elapsedTime < duration)
        {
            day_Canvas.transform.Find("Moon").gameObject.transform.GetChild(1).gameObject.transform.Rotate(0f, 720f * Time.deltaTime, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SunShine.Play();
        MoonShine.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        yield return new WaitForSecondsRealtime(0.05f);
        day_Canvas.transform.Find("Sun").gameObject.SetActive(true);
        day_Canvas.transform.Find("Moon").gameObject.SetActive(false);
        day_Canvas.transform.Find("MoonBackground").gameObject.SetActive(false);
        day_Canvas.transform.Find("Sun").gameObject.transform.GetChild(0).gameObject.GetComponent<Animation>().Stop();
        elapsedTime = 0f;
        duration = 0.5f;

        while (elapsedTime < duration)
        {
            day_Canvas.transform.Find("Sun").gameObject.transform.GetChild(1).gameObject.transform.Rotate(0f, 720f * Time.deltaTime, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(1f);

        MainCamera.enabled = true;
        UICamera.enabled = false;
        base_Canvas.SetActive(true);
        day_Canvas.SetActive(false);
    }

    
    public void checkWinorLose()
    {
        if (!EUnitManager.instance.castle)
        {
            SceneManager.LoadScene("Win");
            Debug.Log("승리");
        }
        else if (!PUnitManager.instance.castle)
        {
            SceneManager.LoadScene("Lose");
            Debug.Log("패배");
            //패배
        }
        else
        {
            attack_Canvas.SetActive(false);
            base_Canvas.SetActive(true);
            StartWar = false;
            Onattack = false;
            EnemyAttack = false;
            TurnStart();
        }
    }

}

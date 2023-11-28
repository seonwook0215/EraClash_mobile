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

    }
    public void TouchSoldierButton()
    {

    }
    public void TouchEndDayButton()
    {
        if (EnemyAttack)
        {
            StartWar = true;
            BattleManager.instance.StartWar();
        }
        else
        {
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
            Onattack = true;
            BattleManager.instance.StartWar();
        }

    }
    public void TurnStart()
    {
        Day++;
        PResourceManager.instance.MP += (PBuildingManager.instance.R_building.Count + PBuildingManager.instance.Fortress_R_building.Count) * 50 + 50;
        ChangeDay();
        ChangeGold();
        ChangeGainGold();
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
            StartWar = false;
            Onattack = false;
            EnemyAttack = false;
            TurnStart();
        }
    }

}

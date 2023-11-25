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
    public float attackday=0;
    [SerializeField] private GameObject firstphase;
    [SerializeField] private GameObject firstXbutton;
    [SerializeField] private GameObject secondphase;
    [SerializeField] private GameObject secondXbutton;
    [SerializeField] private GameObject thirdphase;
    [SerializeField] private GameObject thirdXbutton;
    [SerializeField] private GameObject thirdAttackbutton;
    
    public bool Onattack=false;
    public bool EnemyAttack = false;
    public bool StartWar = false;
    private float Paladin;
    private float Lancer;
    private float Archer;
    private float MP;
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

    public void TurnStart()
    {

        Day++;
        if (Day == 20)
        {
            SceneManager.LoadScene("Lose");
            Debug.Log("패배");
        }
        PResourceManager.instance.MP += 50;
        Phase1();
        
    }
    public void TurnEnd()
    {
        //턴 끝나면 자원 보충
        //PResourceManager.instance.MP += 50 + PBuildingManager.instance.R_building.Count * 50;
        //EResourceManager.instance.MP += 50 + EBuildingManager.instance.R_building.Count * 50;
        //유닛 보충
        
    }
    public void Phase1() //전날이랑 비교
    {
        attackday -= 1;
        GameManager.instance.Phase = 1;
        if (Day == 1) //첫날은 안보여줘도됨  
        {
            Debug.Log(Day);
            Phase2();
        }
        else
        {
            Time.timeScale = 1;
            Debug.Log("Phase1 start");
            Debug.Log(Day);
            Turn_phase1.instance.phase1 = true;
            thirdphase.SetActive(false);
            firstphase.SetActive(true);
            Time.timeScale=0;
            
        }
    }
    public void Phase2() // 건물 짓기
    {
        Time.timeScale = 1;
        firstphase.SetActive(false);
        secondphase.SetActive(true);
        Turn_phase1.instance.phase1 = false;
        GameManager.instance.Phase = 2;
        Debug.Log("Phase2 start");
    }
    public void Phase3() // 공격여부 결정
    {
        GameManager.instance.Phase = 3;
        Time.timeScale = 0;
        secondphase.SetActive(false);
        thirdphase.SetActive(true);
    }
    public void Attack()
    {
        if (attackday > 0) //can't attack 
        {
            
            messageCanvas.SetActive(true);
            ShowMessageCannotAttackDay();
            Phase3();
        }
        else if (PUnitManager.instance.Archer + PUnitManager.instance.Lancer + PUnitManager.instance.Paladin <= 0)
        {
            messageCanvas.SetActive(true);
            ShowMessageCannotAttack0Army();
            
            Phase3();
        }
        else
        {
            
            Onattack = true;
            attackday = 3;
            checkAttacked();
        }
        //BattleManager.instance.phase3 = true; //버튼 누르고 실행
    }
    public void checkAttacked()
    {
        if (EnemyAttack||Onattack)
        {
            thirdphase.SetActive(false);
            Time.timeScale = 1;
            StartWar = true;
        }
        else
        {
            TurnStart();
        }
    }

    //BattleManager.instance.phase3 = true; //버튼 누르고 실행
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

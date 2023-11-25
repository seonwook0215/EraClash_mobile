using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    
    public static BattleManager instance;
    public float cameranum = -1;
    [Space(10)] [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip war_begin;
    [SerializeField] AudioClip war_end;
    private float cnt = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("배틀 매니저가 두개이상!");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnManager.instance.StartWar &&cnt==0)
        {
            cnt = 1;
            Debug.Log("BMstart");
            selectPlace();
            audioSource.Stop();
            audioSource.clip=war_begin;
            audioSource.Play();
        }
       
     
    }
    private void LateUpdate()
    {
        if (cameranum != -1)
        {
            EndFight();

        }
    }

    private void EndFight()
    {
        int player_unit = PUnitManager.instance.P_units.Count + PUnitManager.instance.L_units.Count + PUnitManager.instance.A_units.Count;
        int enemy_unit = EUnitManager.instance.P_units.Count + EUnitManager.instance.L_units.Count + EUnitManager.instance.A_units.Count;
        
        switch (cameranum)
        {

            case 0:
                if (player_unit == 0 || enemy_unit == 0)
                {
                    Debug.Log("끝났음");
                    ReturnArmy();
                    makefieldArmyzero();
                    cnt = 0;
                    cameranum = -1;
                    PUnitManager.instance.units.Add(GameObject.Find("Player Castle Unit").GetComponent<PUnit>());
                    EUnitManager.instance.units.Add(GameObject.Find("Enemy Castle Unit").GetComponent<EUnit>());
                    if (PUnitManager.instance.fortress)
                    {
                        PUnitManager.instance.units.Add(GameObject.Find("Player Fortress Unit").GetComponent<PUnit>());
                    }
                    if (EUnitManager.instance.fortress)
                    {
                        EUnitManager.instance.units.Add(GameObject.Find("Enemy Fortress Unit").GetComponent<EUnit>());
                    }
                    audioSource.Stop();
                    audioSource.clip = war_end;
                    audioSource.loop = true;
                    audioSource.Play();
                    TurnManager.instance.checkWinorLose();
                }
                break;
            case 1:
                if (!EUnitManager.instance.fortress || player_unit == 0)
                {
                    Debug.Log("끝났음2");
                    ReturnArmy();
                    makefieldArmyzero();
                    if (!EUnitManager.instance.fortress)
                    {
                        destroyEnemyfortress();
                    }
                    cnt = 0;
                    cameranum = -1;
                    PUnitManager.instance.units.Add(GameObject.Find("Player Castle Unit").GetComponent<PUnit>());
                    
                    if (PUnitManager.instance.fortress)
                    {
                        PUnitManager.instance.units.Add(GameObject.Find("Player Fortress Unit").GetComponent<PUnit>());
                    }

                    audioSource.Stop();
                    audioSource.clip = war_end;
                    audioSource.loop = true;
                    audioSource.Play();
                    TurnManager.instance.checkWinorLose();
                }
                break;
            case 2:
                if (!EUnitManager.instance.castle || player_unit == 0)
                {
                    Debug.Log("끝났음3");
                    ReturnArmy();
                    makefieldArmyzero();
                    cnt = 0;
                    cameranum = -1;
                    PUnitManager.instance.units.Add(GameObject.Find("Player Castle Unit").GetComponent<PUnit>());

                    if (PUnitManager.instance.fortress)
                    {
                        PUnitManager.instance.units.Add(GameObject.Find("Player Fortress Unit").GetComponent<PUnit>());
                    }

                    audioSource.Stop();
                    audioSource.clip = war_end;
                    audioSource.loop = true;
                    audioSource.Play();
                    TurnManager.instance.checkWinorLose();
                }
                break;
            case 3:
                if (!PUnitManager.instance.fortress || enemy_unit == 0)
                {
                    if (enemy_unit == 0)
                    {
                        Debug.Log("끝났음4");
                    }
                    if (!PUnitManager.instance.fortress)
                    {
                        Debug.Log("성벽컷투끝났음");
                    }
                    ReturnArmy();
                    makefieldArmyzero();
                    if (!PUnitManager.instance.fortress)
                    {
                        destroyPlayerfortress();
                    }
                    cnt = 0;
                    cameranum = -1;

                    EUnitManager.instance.units.Add(GameObject.Find("Enemy Castle Unit").GetComponent<EUnit>());

                    if (EUnitManager.instance.fortress)
                    {
                        EUnitManager.instance.units.Add(GameObject.Find("Enemy Fortress Unit").GetComponent<EUnit>());
                    }
                    audioSource.Stop();
                    audioSource.clip = war_end;
                    audioSource.loop = true;
                    audioSource.Play();
                    TurnManager.instance.checkWinorLose();
                }
                break;
            case 4:
                if (!PUnitManager.instance.castle || enemy_unit == 0)
                {
                    Debug.Log("끝났음5");
                    ReturnArmy();
                    makefieldArmyzero();
                    cnt = 0;
                    cameranum = -1;
                    EUnitManager.instance.units.Add(GameObject.Find("Enemy Castle Unit").GetComponent<EUnit>());

                    if (EUnitManager.instance.fortress)
                    {
                        EUnitManager.instance.units.Add(GameObject.Find("Enemy Fortress Unit").GetComponent<EUnit>());
                    }
                    audioSource.Stop();
                    audioSource.clip = war_end;
                    audioSource.loop = true;
                    audioSource.Play();
                    TurnManager.instance.checkWinorLose();
                }
                break;
            default:
                break;
        }
    }
    private void destroyEnemyfortress()
    {
        for (int i = EBuildingManager.instance.Fortress_A_building.Count - 1; i >= 0; i--)
        {
            
            Destroy(EBuildingManager.instance.Fortress_A_building[i].gameObject);
            EBuildingManager.instance.Fortress_A_building.RemoveAt(i);
        }
        for (int i = EBuildingManager.instance.Fortress_R_building.Count - 1; i >= 0; i--)
        {
            Destroy(EBuildingManager.instance.Fortress_R_building[i].gameObject);
            EBuildingManager.instance.Fortress_R_building.RemoveAt(i);
        }
        for (int i = EBuildingManager.instance.Fortress_L_building.Count - 1; i >= 0; i--)
        {
            Destroy(EBuildingManager.instance.Fortress_L_building[i].gameObject);

            EBuildingManager.instance.Fortress_L_building.RemoveAt(i);
        }
        for (int i = EBuildingManager.instance.Fortress_P_building.Count - 1; i >= 0; i--)
        {
            Destroy(EBuildingManager.instance.Fortress_P_building[i].gameObject);
            EBuildingManager.instance.Fortress_P_building.RemoveAt(i);
        }
        if(EUnitManager.instance.fortress)
            EUnitManager.instance.units.Remove(GameObject.Find("Enemy Fortress Unit").GetComponent<EUnit>());
    }
    private void destroyPlayerfortress()
    {
        for (int i = PBuildingManager.instance.Fortress_A_building.Count - 1; i >= 0; i--)
        {
            Destroy(PBuildingManager.instance.Fortress_A_building[i].gameObject);
            PBuildingManager.instance.Fortress_A_building.RemoveAt(i);
        }
        for (int i = PBuildingManager.instance.Fortress_R_building.Count - 1; i >= 0; i--)
        {
            Destroy(PBuildingManager.instance.Fortress_R_building[i].gameObject);
            PBuildingManager.instance.Fortress_R_building.RemoveAt(i);
        }
        for (int i = PBuildingManager.instance.Fortress_L_building.Count - 1; i >= 0; i--)
        {
            Destroy(PBuildingManager.instance.Fortress_L_building[i].gameObject);
            PBuildingManager.instance.Fortress_L_building.RemoveAt(i);
        }
        for (int i = PBuildingManager.instance.Fortress_P_building.Count - 1; i >= 0; i--)
        {
            Destroy(PBuildingManager.instance.Fortress_P_building[i].gameObject);
            PBuildingManager.instance.Fortress_P_building.RemoveAt(i);
        }
        if (PUnitManager.instance.fortress)
            PUnitManager.instance.units.Remove(GameObject.Find("Player Fortress Unit").GetComponent<PUnit>());
    }
    private void ReturnArmy()
    {
        EUnitManager.instance.Archer = EUnitManager.instance.A_units.Count;
        EUnitManager.instance.Lancer = EUnitManager.instance.L_units.Count;
        EUnitManager.instance.Paladin = EUnitManager.instance.P_units.Count;
        PUnitManager.instance.Archer = PUnitManager.instance.A_units.Count;
        PUnitManager.instance.Lancer = PUnitManager.instance.L_units.Count;
        PUnitManager.instance.Paladin = PUnitManager.instance.P_units.Count;
   
}
    private void makeArmyzero()
    {
        EUnitManager.instance.Archer = 0;
        EUnitManager.instance.Lancer = 0;
        EUnitManager.instance.Paladin = 0;
        PUnitManager.instance.Archer = 0;
        PUnitManager.instance.Lancer = 0;
        PUnitManager.instance.Paladin = 0;
    }
    private void makefieldArmyzero()
    { 
            for (int i = PUnitManager.instance.P_units.Count - 1; i >= 0; i--)
            {
            //Debug.Log("실행");
            Destroy(PUnitManager.instance.P_units[i].gameObject);
            //Destroy(PUnitManager.instance.P_units[i]);
        }
        for (int i = PUnitManager.instance.A_units.Count - 1; i >= 0; i--)
        {
            //  Debug.Log("실행");
            Destroy(PUnitManager.instance.A_units[i].gameObject);
            //Destroy(PUnitManager.instance.P_units[i]);
        }
        for (int i = PUnitManager.instance.L_units.Count - 1; i >= 0; i--)
        {
            //   Debug.Log("실행");
            Destroy(PUnitManager.instance.L_units[i].gameObject);
            //Destroy(PUnitManager.instance.P_units[i]);
        }


        for (int i = EUnitManager.instance.P_units.Count - 1; i >= 0; i--)
        {
            // Debug.Log("실행");
            Destroy(EUnitManager.instance.P_units[i].gameObject);
            //Destroy(PUnitManager.instance.P_units[i]);
        }
        for (int i = EUnitManager.instance.A_units.Count - 1; i >= 0; i--)
        {
            // Debug.Log("실행");
            Destroy(EUnitManager.instance.A_units[i].gameObject);
            //Destroy(PUnitManager.instance.P_units[i]);
        }
        for (int i = EUnitManager.instance.L_units.Count - 1; i >= 0; i--)
        {
            // Debug.Log("실행");
            Destroy(EUnitManager.instance.L_units[i].gameObject);
            //Destroy(PUnitManager.instance.P_units[i]);
        }
    }
    
    
    
    private void selectPlace()
    {
        if (TurnManager.instance.Onattack && TurnManager.instance.EnemyAttack) //attack both 평야
        {
            cameranum = 0;
            //카메라 옮기기
            //평야에 있는 스포너에 유닛 할당
            PUnitManager.instance.units.Remove(GameObject.Find("Player Castle Unit").GetComponent<PUnit>());
            if (PUnitManager.instance.fortress)
            {
                PUnitManager.instance.units.Remove(GameObject.Find("Player Fortress Unit").GetComponent<PUnit>());
            }
            EUnitManager.instance.units.Remove(GameObject.Find("Enemy Castle Unit").GetComponent<EUnit>());
            if (EUnitManager.instance.fortress)
            {
                EUnitManager.instance.units.Remove(GameObject.Find("Enemy Fortress Unit").GetComponent<EUnit>());
            }
            GameObject.Find("BattleFieldSpawner").GetComponent<EArcherSpawn>().num = EUnitManager.instance.Archer;
            GameObject.Find("BattleFieldSpawner").GetComponent<ELancerSpawn>().num = EUnitManager.instance.Lancer;
            GameObject.Find("BattleFieldSpawner").GetComponent<EPaladinSpawn>().num = EUnitManager.instance.Paladin;
            GameObject.Find("BattleFieldSpawner").GetComponent<PArcherSpawn>().num = PUnitManager.instance.Archer;
            GameObject.Find("BattleFieldSpawner").GetComponent<PLancerSpawn>().num = PUnitManager.instance.Lancer;
            GameObject.Find("BattleFieldSpawner").GetComponent<PPaladinSpawn>().num = PUnitManager.instance.Paladin;
        }
        else if (TurnManager.instance.Onattack) // player attack, enemy don't 
        {
            if (EUnitManager.instance.fortress)
            { //2차성루 안부서졌음
                cameranum = 1;
                PUnitManager.instance.units.Remove(GameObject.Find("Player Castle Unit").GetComponent<PUnit>());
                if (PUnitManager.instance.fortress)
                {
                    PUnitManager.instance.units.Remove(GameObject.Find("Player Fortress Unit").GetComponent<PUnit>());

                }
                GameObject.Find("1st Enemy Site Spawner").GetComponent<EArcherSpawn>().num = EUnitManager.instance.Archer;
                GameObject.Find("1st Enemy Site Spawner").GetComponent<ELancerSpawn>().num = EUnitManager.instance.Lancer;
                GameObject.Find("1st Enemy Site Spawner").GetComponent<EPaladinSpawn>().num = EUnitManager.instance.Paladin;
                GameObject.Find("1st Enemy Site Spawner").GetComponent<PArcherSpawn>().num = PUnitManager.instance.Archer;
                GameObject.Find("1st Enemy Site Spawner").GetComponent<PLancerSpawn>().num = PUnitManager.instance.Lancer;
                GameObject.Find("1st Enemy Site Spawner").GetComponent<PPaladinSpawn>().num = PUnitManager.instance.Paladin;
            }
            else
            { //2차성루 부서짐
                //Debug.Log("2nd destroyed , lancer unit : " + PUnitManager.instance.Lancer);
                cameranum = 2;
                PUnitManager.instance.units.Remove(GameObject.Find("Player Castle Unit").GetComponent<PUnit>());
                if (PUnitManager.instance.fortress)
                {
                    PUnitManager.instance.units.Remove(GameObject.Find("Player Fortress Unit").GetComponent<PUnit>());
                }
                GameObject.Find("2nd Enemy Site Spawner").GetComponent<EArcherSpawn>().num = EUnitManager.instance.Archer;
                GameObject.Find("2nd Enemy Site Spawner").GetComponent<ELancerSpawn>().num = EUnitManager.instance.Lancer;
                GameObject.Find("2nd Enemy Site Spawner").GetComponent<EPaladinSpawn>().num = EUnitManager.instance.Paladin;
                GameObject.Find("2nd Enemy Site Spawner").GetComponent<PArcherSpawn>().num = PUnitManager.instance.Archer;
                GameObject.Find("2nd Enemy Site Spawner").GetComponent<PLancerSpawn>().num = PUnitManager.instance.Lancer;
                GameObject.Find("2nd Enemy Site Spawner").GetComponent<PPaladinSpawn>().num = PUnitManager.instance.Paladin;
            }
        }
        else if (TurnManager.instance.EnemyAttack) //enemy attack, player don't
        {
            Debug.Log("공격왔음");
            if (PUnitManager.instance.fortress)
            { //2차성루 안부서졌음
                cameranum = 3;

                EUnitManager.instance.units.Remove(GameObject.Find("Enemy Castle Unit").GetComponent<EUnit>());
                if (EUnitManager.instance.fortress)
                {
                    EUnitManager.instance.units.Remove(GameObject.Find("Enemy Fortress Unit").GetComponent<EUnit>());
                }
                GameObject.Find("1st Player Site Spawner").GetComponent<EArcherSpawn>().num = EUnitManager.instance.Archer;
                GameObject.Find("1st Player Site Spawner").GetComponent<ELancerSpawn>().num = EUnitManager.instance.Lancer;
                GameObject.Find("1st Player Site Spawner").GetComponent<EPaladinSpawn>().num = EUnitManager.instance.Paladin;
                GameObject.Find("1st Player Site Spawner").GetComponent<PArcherSpawn>().num = PUnitManager.instance.Archer;
                GameObject.Find("1st Player Site Spawner").GetComponent<PLancerSpawn>().num = PUnitManager.instance.Lancer;
                GameObject.Find("1st Player Site Spawner").GetComponent<PPaladinSpawn>().num = PUnitManager.instance.Paladin;
            }
            else
            { //2차성루 부서짐
                cameranum = 4;

                EUnitManager.instance.units.Remove(GameObject.Find("Enemy Castle Unit").GetComponent<EUnit>());
                if (EUnitManager.instance.fortress)
                {
                    EUnitManager.instance.units.Remove(GameObject.Find("Enemy Fortress Unit").GetComponent<EUnit>());
                }
                GameObject.Find("2nd Player Site Spawner").GetComponent<EArcherSpawn>().num = EUnitManager.instance.Archer;
                GameObject.Find("2nd Player Site Spawner").GetComponent<ELancerSpawn>().num = EUnitManager.instance.Lancer;
                GameObject.Find("2nd Player Site Spawner").GetComponent<EPaladinSpawn>().num = EUnitManager.instance.Paladin;
                GameObject.Find("2nd Player Site Spawner").GetComponent<PArcherSpawn>().num = PUnitManager.instance.Archer;
                GameObject.Find("2nd Player Site Spawner").GetComponent<PLancerSpawn>().num = PUnitManager.instance.Lancer;
                GameObject.Find("2nd Player Site Spawner").GetComponent<PPaladinSpawn>().num = PUnitManager.instance.Paladin;
            }
        }
        else //don't attack both
        {

        }
        makeArmyzero();
    }

    
}

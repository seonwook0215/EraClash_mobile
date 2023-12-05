using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public static EnemyAI Instance;
    public float Day;
    public GameObject ResourceBuilding;
    public GameObject SwordBuilding;
    public GameObject LancerBuilding;
    public GameObject ArcherBuilding;
    public GameObject ShieldBuilding;

    private Vector3 CastleBuildPos;
    private Vector3 FortressBuildPos;
    private float Castlebuildingcnt;
    private float Fortressbuildingcnt;
    public string PlayermainUnit;
    private bool inCastle;


    Vector3 position1 = new Vector3(415, 20, 69);
    Vector3 position2 = new Vector3(415, 20, 55);
    Vector3 position3 = new Vector3(283, 3, 29);//그거
    Vector3 position4 = new Vector3(412, 13, 5);
    Vector3 position5 = new Vector3(282, 3, 15);//그거
    Vector3 position6 = new Vector3(411, 10, -19);
    Vector3 position7 = new Vector3(321, 3, -20);//그거
    Vector3 position8 = new Vector3(291, 3, -34);//그거
    Vector3 position9 = new Vector3(406, 4, -51);
    Vector3 position10 = new Vector3(405, 4, -67);
    Vector3 position11 = new Vector3(375, 4, -51);
    Vector3 position12 = new Vector3(376, 4, -67);

    private void Awake()
    {
        if(Instance != null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("EnemyAI Error");
        }
    }
    private void Start()
    {
        inCastle = true;
        Day = 0;
        Castlebuildingcnt = 1;
        Fortressbuildingcnt = 1;
        PlayermainUnit = null;
        CastleBuildPos = new Vector3(415, 0, -75); //1,2,3,4 -> z +10 | 5->x=393,z=-75 | 6,7,8 ->z +10 | 9->x=371,z=75 | 10~16 ->z +10 | 17-> x=415,y=18,z=60 | 18 ->z +10
        FortressBuildPos = new Vector3(321, 0, -25); // 1,2,3,4,5 ->z+10 | 6 ->x=295,z=-38 | 7~13 ->z+10
    }
    private void Update()
    {
        if (TurnManager.instance.Day > Day)
        {
            Debug.Log("AI act");
            Alact();
            Day = TurnManager.instance.Day;
        }
        
    }

    private void GenerateBuilding(GameObject building)
    {
        if(inCastle)
        {
            Instantiate(building, CastleBuildPos, Quaternion.identity);
            Castlebuildingcnt++;
            inCastle = false;
            if(Castlebuildingcnt < 5 || (Castlebuildingcnt > 5 && Castlebuildingcnt < 9) || (Castlebuildingcnt > 9 && Castlebuildingcnt < 17) || (Castlebuildingcnt ==18))
            {
                CastleBuildPos.z += 10;
            }
            else if(Castlebuildingcnt ==5 ||Castlebuildingcnt ==9)
            {
                CastleBuildPos.x -= 22;
                CastleBuildPos.z = -75;
            }
            else if (Castlebuildingcnt == 17)
            {
                CastleBuildPos = new Vector3(415, 18, 60);
            }
            else // >18
            {
                Castlebuildingcnt = 1;
                CastleBuildPos = new Vector3(415, 0, -75);
            }
        }
        else
        {
            Instantiate(building, CastleBuildPos, Quaternion.identity);
            inCastle = true;
            Fortressbuildingcnt++;
            if(Fortressbuildingcnt == 6)
            {
                FortressBuildPos = new Vector3(295, 0, -38);
            }
            else if(Fortressbuildingcnt == 14)
            {
                FortressBuildPos = new Vector3(321, 0, -25);
            }
            else
            {
                FortressBuildPos.z += 10;
            }
        }
    }

    private bool canBuild(string name)
    {
        if(name == "Resouce" && EResourceManager.instance.MP >= 50)
        {
            return true;
        }
        else if(name == "Spear" && EResourceManager.instance.MP >= 250)
        {
            return true;
        }
        else if (name == "Sword" && EResourceManager.instance.MP >= 150)
        {
            return true;
        }
        else if(name == "Archer" && EResourceManager.instance.MP >= 200)
        {
            return true;
        }
        else if(name == "Shield" && EResourceManager.instance.MP >= 250) //방패병 건물 얼마?
        {
            return true;
        }
        else { return false; }
    }

    private void Alact() {
        //처음 시작 200원 시작
        CounterUnitBuild();

        switch (Day)
        {
            //Day 1 자원
            case 0:
                if (canBuild("Resouce")) GenerateBuilding(ResourceBuilding);    //TurnManager.instance.EnemyAttack = true;
                break;
            //Day 2 자원
            case 1:
                if (canBuild("Resouce")) GenerateBuilding(ResourceBuilding);
                break;
            //Day 3 창   공격x
            case 2:
                if (canBuild("Spear")) GenerateBuilding(LancerBuilding);
                break;
            //Day 4 자원
            case 3:
                if (canBuild("Resouce")) GenerateBuilding(ResourceBuilding);
                break;
            //Day 5 검
            case 4:
                if (canBuild("Sword")) GenerateBuilding(SwordBuilding);
                break;
            //Day 6 창   공격o
            case 5:
                if (canBuild("Archer")) GenerateBuilding(ArcherBuilding);
                break;
            //Day 7 궁
            case 6:
                if (canBuild("Spear")) GenerateBuilding(LancerBuilding);
                break;
            //Day 8 검
            case 7:
                if (canBuild("Sword")) GenerateBuilding(SwordBuilding);
                break;
            //Day 9 창   공격o
            case 8:
                if (canBuild("Spear")) GenerateBuilding(LancerBuilding);
                break;
            //Day 10궁
            case 9:
                if (canBuild("Shield")) GenerateBuilding(ShieldBuilding);
                break;

            //Day 11검 ->계속 뽑기
            case 10:
                if (canBuild("Sword"))
                {
                    GenerateBuilding(SwordBuilding);
                    BuildLoop();
                    break;
                }
                break;
            //Day 12궁   공격o
            case 11:
                if (canBuild("Archer"))
                {
                    GenerateBuilding(ArcherBuilding);
                    BuildLoop();
                    break;
                }
                break;
            case 12:
                if (canBuild("Archer"))
                {
                    GenerateBuilding(ArcherBuilding);
                    BuildLoop();
                    break;
                }
                break;
            case 13:
                if (canBuild("Shield"))
                {
                    GenerateBuilding(ShieldBuilding);
                    BuildLoop();
                    break;
                }
                    break;
            case 14:
                if (canBuild("Shield"))
                {
                    GenerateBuilding(ShieldBuilding);
                    BuildLoop();
                    break;
                }
                    break;
            case 15:
                if (canBuild("Archer"))
                {
                    GenerateBuilding(ArcherBuilding);
                    BuildLoop();
                    break;
                }
                    break;
            case 16:
                if (canBuild("Sword"))
                {
                    GenerateBuilding(SwordBuilding);
                    BuildLoop();
                    break;
                }
                    break;
            case 17:
                if (canBuild("Archer"))
                {
                    GenerateBuilding(ArcherBuilding);
                    BuildLoop();
                    break;
                }
                    break;
            case 18:
                if (canBuild("Spear"))
                {
                    GenerateBuilding(LancerBuilding);
                    BuildLoop();
                    break;
                }
                    break;
            case 19:
                if (canBuild("Sword"))
                {
                    GenerateBuilding(SwordBuilding);
                    BuildLoop();
                    break;
                }
                    break;
            case 20:
                if (canBuild("Sword"))
                {
                    GenerateBuilding(SwordBuilding);
                    BuildLoop();
                    break;
                }
                    break;
            //마지막 전투 후 끝
        }

    }

    //spear>archer>shield>sword>spear
    private void CounterUnitBuild()
    {
        //PlayermainUnit = PUnitManager.instance.mainUnit;
        Debug.Log(PlayermainUnit);
        if (PlayermainUnit == "Sword")
        {
            //shield
            GenerateBuilding(ShieldBuilding);
        }
        else if(PlayermainUnit == "Spear")
        {
            //sword
            GenerateBuilding(SwordBuilding);
        }
        else if(PlayermainUnit == "Archer")
        {
            //spear
            GenerateBuilding(LancerBuilding);
        }
        else if (PlayermainUnit == "Shield")
        {
            //archer
            GenerateBuilding(ArcherBuilding);
        }
        else
        {
            return;
        }
    }

    private void BuildLoop()
    {
        while (true)
        {
            float rand=Random.Range(0,4);
            if (rand == 0)
            {
                if (canBuild("Resouce")) GenerateBuilding(ResourceBuilding);
                else break;
            }
            else if(rand == 1)
            {
                if (canBuild("Sword")) GenerateBuilding(SwordBuilding);
                else break;
            }
            else if (rand == 2)
            {
                if (canBuild("Spear")) GenerateBuilding(LancerBuilding);
                else break;
            }
            else
            {
                if (canBuild("Archer")) GenerateBuilding(ArcherBuilding);
                else break;
            }
        }
    }
}

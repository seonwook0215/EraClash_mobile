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

    private void Awake()
    {
        if(Instance == null)
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
        PlayermainUnit = "None";
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
                //Castlebuildingcnt = 1;
                //CastleBuildPos = new Vector3(415, 0, -75);
            }
        }
        else
        {
            Instantiate(building, FortressBuildPos, Quaternion.identity);
            inCastle = true;
            Fortressbuildingcnt++;
            if(Fortressbuildingcnt == 6)
            {
                FortressBuildPos = new Vector3(295, 0, -38);
            }
            else if(Fortressbuildingcnt == 14)
            {
                //Fortressbuildingcnt = 1;
                //FortressBuildPos = new Vector3(321, 0, -25);
            }
            else
            {
                FortressBuildPos.z += 10;
            }
        }
    }

    private bool canBuild(string name)
    {
        if(Castlebuildingcnt>18 || Fortressbuildingcnt > 13)
        {
            return false;
        }
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
                if (canBuild("Resouce"))
                {
                    GenerateBuilding(ResourceBuilding);
                    EResourceManager.instance.MP -= 50;
                    break;
                }
                break;
            //Day 2 자원
            case 1:
                if (canBuild("Spear"))
                {
                    GenerateBuilding(LancerBuilding);
                    EResourceManager.instance.MP -= 250;
                    break;
                }
                break;
            //Day 3 창   공격x
            case 2:
                if (canBuild("Resouce"))
                {
                    GenerateBuilding(ResourceBuilding);
                    EResourceManager.instance.MP -= 50;
                    break;
                }
                break;
            //Day 4 자원
            case 3:
                if (canBuild("Resouce"))
                {
                    GenerateBuilding(ResourceBuilding);
                    EResourceManager.instance.MP -= 50;
                    break;
                }
                break;
            //Day 5 검
            case 4:
                if (canBuild("Sword"))
                {
                    GenerateBuilding(SwordBuilding);
                    EResourceManager.instance.MP -= 150;
                    break;
                }
                break;
            //Day 6 창   공격o
            case 5:
                if (canBuild("Archer"))
                {
                    GenerateBuilding(ArcherBuilding);
                    EResourceManager.instance.MP -= 200;
                    break;
                }
                break;
            //Day 7 궁
            case 6:
                TurnManager.instance.EnemyAttack = true;
                if (canBuild("Spear"))
                {
                    GenerateBuilding(LancerBuilding);
                    EResourceManager.instance.MP -= 250;
                    break;
                }
                break;
            //Day 8 검
            case 7:
                if (canBuild("Sword"))
                {
                    GenerateBuilding(SwordBuilding);
                    EResourceManager.instance.MP -= 150;
                    break;
                }
                break;
            //Day 9 창   공격o
            case 8:
                if (canBuild("Spear"))
                {
                    GenerateBuilding(LancerBuilding);
                    EResourceManager.instance.MP -= 250;
                    break;
                }
                break;
            //Day 10궁
            case 9:
                TurnManager.instance.EnemyAttack = true;
                if (canBuild("Shield"))
                {
                    GenerateBuilding(ShieldBuilding);
                    EResourceManager.instance.MP -= 250;
                    break;
                }
                break;

            //Day 11검 ->계속 뽑기
            case 10:
                if (canBuild("Sword"))
                {
                    GenerateBuilding(SwordBuilding);
                    EResourceManager.instance.MP -= 150;
                    BuildLoop();
                    break;
                }
                break;
            //Day 12궁   공격o
            case 11:
                if (canBuild("Archer"))
                {
                    GenerateBuilding(ArcherBuilding);
                    EResourceManager.instance.MP -= 200;
                    BuildLoop();
                    break;
                }
                break;
            case 12:
                TurnManager.instance.EnemyAttack = true;
                if (canBuild("Archer"))
                {
                    GenerateBuilding(ArcherBuilding);
                    EResourceManager.instance.MP -= 200;
                    BuildLoop();
                    break;
                }
                break;
            case 13:
                if (canBuild("Shield"))
                {
                    GenerateBuilding(ShieldBuilding);
                    EResourceManager.instance.MP -= 250;
                    BuildLoop();
                    break;
                }
                break;
            case 14:
                TurnManager.instance.EnemyAttack = true;
                if (canBuild("Shield"))
                {
                    GenerateBuilding(ShieldBuilding);
                    EResourceManager.instance.MP -= 250;
                    BuildLoop();
                    break;
                }
                break;
            case 15:
                if (canBuild("Archer"))
                {
                    GenerateBuilding(ArcherBuilding);
                    EResourceManager.instance.MP -= 200;
                    BuildLoop();
                    break;
                }
                break;
            case 16:
                TurnManager.instance.EnemyAttack = true;
                if (canBuild("Sword"))
                {
                    GenerateBuilding(SwordBuilding);
                    EResourceManager.instance.MP -= 150;
                    BuildLoop();
                    break;
                }
                break;
            case 17:
                if (canBuild("Archer"))
                {
                    GenerateBuilding(ArcherBuilding);
                    EResourceManager.instance.MP -= 200;
                    BuildLoop();
                    break;
                }
                break;
            case 18:
                TurnManager.instance.EnemyAttack = true;
                if (canBuild("Spear"))
                {
                    GenerateBuilding(LancerBuilding);
                    EResourceManager.instance.MP -= 250;
                    BuildLoop();
                    break;
                }
                break;
            case 19:
                if (canBuild("Sword"))
                {
                    GenerateBuilding(SwordBuilding);
                    EResourceManager.instance.MP -= 150;
                    BuildLoop();
                    break;
                }
                break;
            case 20:
                TurnManager.instance.EnemyAttack = true;
                if (canBuild("Sword"))
                {
                    GenerateBuilding(SwordBuilding);
                    EResourceManager.instance.MP -= 150;
                    BuildLoop();
                    break;
                }
                break;
            case 21:
                TurnManager.instance.EnemyAttack = true;
                if (canBuild("Spear"))
                {
                    GenerateBuilding(LancerBuilding);
                    EResourceManager.instance.MP -= 150;
                    BuildLoop();
                    break;
                }
                break;
            case 22:
                TurnManager.instance.EnemyAttack = true;
                if (canBuild("Archer"))
                {
                    GenerateBuilding(ArcherBuilding);
                    EResourceManager.instance.MP -= 150;
                    BuildLoop();
                    break;
                }
                break;
            case 23:
                TurnManager.instance.EnemyAttack = true;
                if (canBuild("Archer"))
                {
                    GenerateBuilding(ArcherBuilding);
                    EResourceManager.instance.MP -= 150;
                    BuildLoop();
                    break;
                }
                break;
            case 24:
                TurnManager.instance.EnemyAttack = true;
                if (canBuild("Spear"))
                {
                    GenerateBuilding(SwordBuilding);
                    EResourceManager.instance.MP -= 150;
                    BuildLoop();
                    break;
                }
                break;
            case 25:
                TurnManager.instance.EnemyAttack = true;
                if (canBuild("Shield"))
                {
                    GenerateBuilding(ShieldBuilding);
                    EResourceManager.instance.MP -= 150;
                    BuildLoop();
                    break;
                }
                break;
        }

    }

    //spear>archer>shield>sword>spear
    private void CounterUnitBuild()
    {
        if (PlayermainUnit == "Sword")
        {
            //shield
            if (canBuild("Spear"))
                GenerateBuilding(LancerBuilding);
        }
        else if(PlayermainUnit == "Spear")
        {
            //sword
            if (canBuild("Archer"))
                GenerateBuilding(ArcherBuilding);
        }
        else if(PlayermainUnit == "Archer")
        {
            //spear
            if (canBuild("Shield"))
                GenerateBuilding(ShieldBuilding);
        }
        else if (PlayermainUnit == "Shield")
        {
            //archer
            if (canBuild("Sword"))
                GenerateBuilding(SwordBuilding);
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
            int rand=Random.Range(0,4);
            Debug.Log("rand: "+rand);
            if (rand == 0)
            {
                if (canBuild("Resouce"))
                {
                    GenerateBuilding(ResourceBuilding);
                    EResourceManager.instance.MP -= 50;
                }
                else break;
            }
            else if(rand == 1)
            {
                if (canBuild("Sword"))
                {
                    GenerateBuilding(SwordBuilding);
                    EResourceManager.instance.MP -= 150;
                }
                else break;
            }
            else if (rand == 2)
            {
                if (canBuild("Spear"))
                {
                    GenerateBuilding(LancerBuilding);
                    EResourceManager.instance.MP -= 250;
                }
                else break;
            }
            else if(rand == 3)
            {
                if (canBuild("Archer"))
                {
                    GenerateBuilding(ArcherBuilding);
                    EResourceManager.instance.MP -= 200;
                }
                else break;
            }
            else
            {
                if (canBuild("Shield"))
                {
                    GenerateBuilding(ShieldBuilding);
                    EResourceManager.instance.MP -= 250;
                }
                else break;
            }
        }
    }
}

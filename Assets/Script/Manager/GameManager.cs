using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject Player;
    public GameObject Enemy;

    public float Day;
    public float Phase;
    private int person;
    private int AI_choose;
    public float P_multi = 1;
    public float E_multi = 1;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("게임 매니저가 두개이상!");
            Destroy(gameObject);
        }


    }
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        person = PersonController.instance.cur; //0 leo 1 lee 3 na
        if (person == 0)
        {
            //처음 군량 2배
            Debug.Log("레오");
            PResourceManager.instance.MP = 300;
        }
        else if (person == 1)
        {
            //성, 성루 공격력 50%추가
            GameObject.Find("Player Castle Unit").GetComponent<PUnit>().damage = GameObject.Find("Player Castle Unit").GetComponent<PUnit>().damage * 3 / 2;
            GameObject.Find("Player Fortress Unit").GetComponent<PUnit>().damage = GameObject.Find("Player Fortress Unit").GetComponent<PUnit>().damage * 3 / 2;
            Debug.Log("이순신");
        }
        else if (person == 2)
        {
            // 유닛 공격력 20%추가
            P_multi = (float)1.2;
            Debug.Log(P_multi);
            Debug.Log("나폴레옹");
        }

        AI_choose = Random.Range(0, 3);
        if (AI_choose == 0)
        {
            //처음 군량 2배
            Debug.Log("레오");
            EResourceManager.instance.MP = 300;
        }
        else if (AI_choose == 1)
        {
            //성, 성루 공격력 50%추가
            GameObject.Find("Enemy Castle Unit").GetComponent<EUnit>().damage = GameObject.Find("Enemy Castle Unit").GetComponent<EUnit>().damage * 3 / 2;
            GameObject.Find("Enemy Fortress Unit").GetComponent<EUnit>().damage = GameObject.Find("Enemy Fortress Unit").GetComponent<EUnit>().damage * 3 / 2;
            Debug.Log("이순신");
        }
        else if (AI_choose == 2)
        {
            // 유닛 공격력 20%추가
            E_multi = (float)1.2;
            Debug.Log(E_multi);
            Debug.Log("나폴레옹");
        }
    }


}

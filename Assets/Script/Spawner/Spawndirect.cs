using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Spawndirect : MonoBehaviour
{
    public GameObject rangeobject;

    BoxCollider rangeCollider;
    public GameObject unit;
    public int num;
    private void Awake()
    {
    }
    private void Start()
    {
        //StartCoroutine(RandomRespawn_Coroutine());
    }

    private void Update()
    {
        if (num > 0) // 스폰 한번만
        {
            RandomSpawn(num);
            num = 0;
        }
    }

    private void RandomSpawn(int n)
    {
        Vector3 originPosition = rangeobject.transform.position;
        int pos_x = 0;
        if (num > 10)
        {
            int pos_z = 0;
            if (unit.layer == LayerMask.NameToLayer("Player"))
            {
                pos_z = -2;
            }
            else if (unit.layer == LayerMask.NameToLayer("Enemy"))
            {
                pos_z = 2;
            }
            pos_x = (20 - num) / 2;
            Vector3 pos = new Vector3(0, 0, 0);
            for (int i = 0; i < 10; i++)
            {
                GameObject instantUnit = Instantiate(unit, originPosition + pos, Quaternion.identity);
                pos.x+=2;
            }

            pos = new Vector3(pos_x, 0, pos_z);
            for (int i = 0; i < num - 10; i++)
            {
                GameObject instantUnit = Instantiate(unit, originPosition + pos, Quaternion.identity);
                pos.x+=2;
            }
        }
        else
        {
            pos_x = (10 - num) / 2;
            Vector3 pos= new Vector3(pos_x, 0, 0);
            for (int i = 0; i < num; i++)
            {
                GameObject instantUnit = Instantiate(unit, originPosition + pos, Quaternion.identity);
                pos.x+=2;
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELancerSpawn : MonoBehaviour
{
    public GameObject rangeobject;
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
            RandomSpawn();
            num = 0;
        }
    }

    private void RandomSpawn()
    {
        Vector3 originPosition = rangeobject.transform.position;
        int pos_z = 0;
        if (num > 20)
        {
            num = 20;
        }
        if (num > 10)
        {
            int pos_x = 2;
            pos_z = (20 - num) / 2;
            Vector3 pos = new Vector3(0, 0, 0);
            for (int i = 0; i < 10; i++)
            {
                Instantiate(unit, originPosition + pos, unit.transform.rotation);
                //pos.x+=2;
                pos.z += 2;
            }

            pos = new Vector3(pos_x, 0, pos_z);
            for (int i = 0; i < num - 10; i++)
            {
                Instantiate(unit, originPosition + pos, unit.transform.rotation);
                //pos.x+=2;
                pos.z += 2;
            }
        }
        else
        {
            pos_z = (10 - num) / 2;
            Vector3 pos = new Vector3(pos_z, 0, 0);
            for (int i = 0; i < num; i++)
            {
                Instantiate(unit, originPosition + pos, unit.transform.rotation);
                //pos.x+=2;
                pos.z += 2;
            }
        }
    }
}

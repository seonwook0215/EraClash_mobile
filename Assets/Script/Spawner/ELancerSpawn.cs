using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELancerSpawn : MonoBehaviour
{
    public GameObject rangeobject;

    BoxCollider rangeCollider;
    public GameObject unit;
    public int num;
    private void Start()
    {
       // StartCoroutine(RandomRespawn_Coroutine());
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
        for (int i = 0; i < num; i++)
        {
            GameObject instantUnit = Instantiate(unit, Return_RandomPosition(), Quaternion.identity);
        }
    }
    private void Awake()
    {
        rangeCollider = rangeobject.GetComponent<BoxCollider>();
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeobject.transform.position;

        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPosition = new Vector3(range_X, 1, range_Z);

        Vector3 respawnPosition = originPosition + RandomPosition;

        return respawnPosition;
    }
}

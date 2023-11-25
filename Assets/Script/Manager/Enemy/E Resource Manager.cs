using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EResourceManager : MonoBehaviour //자원 얼마나 있는지 관리
{
    public static EResourceManager instance;
    public float MP;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Duplicated UnitManager, ignoring this one", gameObject);
        }
        MP = 150;
    }
}

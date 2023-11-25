using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject ResourceManger;
    public GameObject BuildingManger;
    public GameObject UnitManger;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("매니저가 두개이상!");
            Destroy(gameObject);
        }
    }
}

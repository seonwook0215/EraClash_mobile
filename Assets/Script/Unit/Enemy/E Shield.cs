using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EShield : MonoBehaviour
{
    void Start()
    {
        EUnitManager.instance.S_units.Add(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        EUnitManager.instance.S_units.Remove(this);
    }
}

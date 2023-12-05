using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EUnit: MonoBehaviour
{
    private void Start()
    {
        if (this.tag != "Building")
        {

        }
        EUnitManager.instance.units.Add(this);
    }
    private void Update()
    {
    }
    private void OnDestroy()
    {
        EUnitManager.instance.units.Remove(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUnit: MonoBehaviour
{
    private void Start()
    {
        if (this.tag != "Building")
        {
        }
        PUnitManager.instance.units.Add(this);
    }
    private void Update()
    {
    }
    private void OnDestroy()
    {
        PUnitManager.instance.units.Remove(this);
    }
}

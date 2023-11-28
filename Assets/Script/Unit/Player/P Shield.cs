using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PShield : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PUnitManager.instance.S_units.Add(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        PUnitManager.instance.S_units.Remove(this);
    }
}

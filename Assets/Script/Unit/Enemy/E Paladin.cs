using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPaladin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EUnitManager.instance.P_units.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        EUnitManager.instance.P_units.Remove(this);
    }
}

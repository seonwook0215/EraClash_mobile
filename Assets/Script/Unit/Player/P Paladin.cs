using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPaladin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PUnitManager.instance.P_units.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        PUnitManager.instance.P_units.Remove(this);
    }
}

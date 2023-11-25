using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLancer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PUnitManager.instance.L_units.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        PUnitManager.instance.L_units.Remove(this);
    }
}

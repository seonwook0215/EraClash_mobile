using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PArcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PUnitManager.instance.A_units.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        PUnitManager.instance.A_units.Remove(this);
    }
}

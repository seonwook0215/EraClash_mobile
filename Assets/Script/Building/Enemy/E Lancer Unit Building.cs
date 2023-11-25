using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELancerUnitBuilding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.position.x < 340) //2nd fortress
        {
            EBuildingManager.instance.Fortress_L_building.Add(this);
        }
        else
        {
            EBuildingManager.instance.L_building.Add(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

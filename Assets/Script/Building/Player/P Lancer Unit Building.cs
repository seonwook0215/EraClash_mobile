using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLancerUnitBuilding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.position.x > -15) //2nd fortress
        {
            PBuildingManager.instance.Fortress_L_building.Add(this);
        }
        else
        {
            PBuildingManager.instance.L_building.Add(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

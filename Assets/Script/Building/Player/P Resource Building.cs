using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PResourceBuilding : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.position.x > -15) //2nd fortress
        {
            PBuildingManager.instance.Fortress_R_building.Add(this);
        }
        else
        {
            PBuildingManager.instance.R_building.Add(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

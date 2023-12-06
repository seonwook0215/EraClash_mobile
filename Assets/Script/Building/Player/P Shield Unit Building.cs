using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PShieldUnitBuilding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.position.x < 340) //2nd fortress
        {
            PBuildingManager.instance.Fortress_S_building.Add(this);
        }
        else
        {
            PBuildingManager.instance.S_building.Add(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

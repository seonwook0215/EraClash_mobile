using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCastleState : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnDestroy()
    {
        EUnitManager.instance.castle = false;
    }

}

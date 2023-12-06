using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastleState : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnDestroy()
    {
        PUnitManager.instance.castle = false;
    }

}

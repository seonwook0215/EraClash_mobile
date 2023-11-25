using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFortressState : MonoBehaviour
{
    private void OnDestroy()
    {
        PUnitManager.instance.fortress = false;
    }
}

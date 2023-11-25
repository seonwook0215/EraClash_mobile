using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFortressState : MonoBehaviour
{
    private void OnDestroy()
    {
        EUnitManager.instance.fortress = false;
    }
}

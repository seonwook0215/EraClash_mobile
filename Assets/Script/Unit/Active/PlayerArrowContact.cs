using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowContact : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }

    }
}

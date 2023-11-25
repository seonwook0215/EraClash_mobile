using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private float delay = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }
}

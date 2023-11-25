using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MPControll : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MP;
    // Start is called before the first frame update
    void Start()
    {
        MP.text = PResourceManager.instance.MP.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        MP.text = PResourceManager.instance.MP.ToString();
    }
}

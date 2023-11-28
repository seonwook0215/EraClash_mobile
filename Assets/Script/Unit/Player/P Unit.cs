using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUnit: MonoBehaviour
{
    //public float speed; //unit speed
    //public float distance; //unit attack range
    //public float damage; // unit attack damage
    //public bool atk = true;
    public AudioSource attackaudioSource;
    public AudioSource hurtaudioSource;
    public AudioClip attack_clip;
    public AudioClip hurt_clip;
    // public float hp; //unit HP

    GameObject obj;

    private void Start()
    {
        if (this.tag != "Building")
        {
            //damage = damage * GameManager.instance.P_multi;
        }
        PUnitManager.instance.units.Add(this);
        //this.GetComponent<overlapspere>().radius = distance;
    }
    private void Update()
    {
    }
    private void OnDestroy()
    {
        PUnitManager.instance.units.Remove(this);
    }
}

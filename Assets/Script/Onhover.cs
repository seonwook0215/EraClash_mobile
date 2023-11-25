using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Onhover : MonoBehaviour
{
    [Space(10)] [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip sound_click;
    [SerializeField] AudioClip sound_hover;
    public Image buttonImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UIClick()
    {
        audioSource.PlayOneShot(sound_click);

    }


    public void OnMouseOver()
    {
        audioSource.PlayOneShot(sound_hover);

        
    }

    public void OnMouseExit()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

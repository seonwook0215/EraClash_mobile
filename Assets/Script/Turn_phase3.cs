using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Turn_phase3 : MonoBehaviour //전날이랑 비교
{
    public static Turn_phase3 instance;
  
    
    [SerializeField] TextMeshProUGUI thirdphase_text;
    [SerializeField] TextMeshProUGUI thirdphase_des;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Duplicated Turn_phase3, ignoring this one");
        }
    }

    private void Start()
    {

        Debug.Log("Phase3 start");
        
        
    }
    private void Update()
    {
        setStateText();

    }
    void setStateText()
    {

        thirdphase_text.text = "How many troops do I have...\n";
        thirdphase_des.text= PUnitManager.instance.Paladin.ToString() + " Sword Soldier\n" +
             PUnitManager.instance.Archer.ToString() + " Archer Soldier\n" +
            PUnitManager.instance.Lancer.ToString() + " Lancer Soldier\n";


    }
}

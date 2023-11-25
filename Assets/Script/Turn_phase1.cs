using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Turn_phase1 : MonoBehaviour //전날이랑 비교
{
    public static Turn_phase1 instance;
    public float Day;
    public bool phase1;
    
    [SerializeField] TextMeshProUGUI firstphase_turn;
    [SerializeField] TextMeshProUGUI firstphase_text;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Duplicated Turn_phase1, ignoring this one");
        }
    }

    private void Start()
    {

        phase1 = false;
        Debug.Log("Phase1 start");
        
        
    }
    private void Update()
    {
        setStateText();

    }
    void setStateText()
    {
        if (phase1)
        {
            Day = TurnManager.instance.Day;
            firstphase_turn.text = Day+"  Day";
            firstphase_text.text = "      What do I have...\n"+ (PBuildingManager.instance.R_building.Count+PBuildingManager.instance.Fortress_R_building.Count)+" Granary\n" +
                   (PBuildingManager.instance.P_building.Count+PBuildingManager.instance.Fortress_P_building.Count)+ " Sword Building\n" +
             (PBuildingManager.instance.A_building.Count + PBuildingManager.instance.Fortress_A_building.Count) + " Archer Building\n" +
            (PBuildingManager.instance.L_building.Count + PBuildingManager.instance.Fortress_L_building.Count) + " Lancer Building\n";

        }
        else
        {
            firstphase_text.text = "";
        }
    }
}

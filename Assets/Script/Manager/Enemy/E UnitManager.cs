using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EUnitManager : MonoBehaviour
{
    public static EUnitManager instance;
    private float pos_x;
    private float pos_y;
    private float pos_z;
    public int Paladin;
    public int Lancer;
    public int Archer;
    public Vector3 pos;
    public Vector3 enemy_pos;
    private float TurnChange;
    public bool fortress;
    public bool castle;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Duplicated UnitManager, ignoring this one", gameObject);
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        fortress = true;
        castle = true;
        Paladin = 0;
        Lancer = 0;
        Archer = 0;
        TurnChange = TurnManager.instance.Day;
    }

    public List<EUnit> units;
    public List<EPaladin> P_units;
    public List<ELancer> L_units;
    public List<EArcher> A_units;
    public List<Vector3> unit_position;

    private void FixedUpdate()
    {
        //Paladin = EBuildingManager.instance.P_building.Count * 10;
        //Lancer = EBuildingManager.instance.L_building.Count * 10;
        //Archer = EBuildingManager.instance.A_building.Count * 10;

        pos_x = pos_y = pos_z = 0;
        foreach (EUnit u in units)
        {
            pos_x += u.transform.position.x;
            pos_y += u.transform.position.y;
            pos_z += u.transform.position.z;
        }
    }
    private void Update()
    {
        if (fortress)
        {
            if (GameObject.Find("Enemy Fortress Unit").GetComponent<Life>().amount <= 0)
            {
                EUnitManager.instance.units.Remove(GameObject.Find("Enemy Fortress Unit").GetComponent<EUnit>());
                fortress = false;
            }
        }
        if (castle)
        {
            if (GameObject.Find("Enemy Castle Unit").GetComponent<Life>().amount <= 0)
            {
                castle = false;
            }
        }
        if (TurnChange != TurnManager.instance.Day)
        {
            TurnChangeGainArmy();
            TurnChange = TurnManager.instance.Day;
        }

        float count = units.Count;
        //Debug.Log("P2 " + count);
        pos = new Vector3(pos_x / count, pos_y / count, pos_z / count);
        enemy_pos = PUnitManager.instance.pos;
    }

    private void TurnChangeGainArmy()
    {
        Paladin += (EBuildingManager.instance.P_building.Count+ EBuildingManager.instance.Fortress_P_building.Count) * 3;
        Lancer += (EBuildingManager.instance.L_building.Count + EBuildingManager.instance.Fortress_L_building.Count) * 2;
        Archer += (EBuildingManager.instance.A_building.Count + EBuildingManager.instance.Fortress_A_building.Count) * 3;
    }
    public void destroyarmy()
    {
        for (int i = 0; i < P_units.Count; i++)
        {
            Debug.Log("destroy");
            Destroy(P_units[i]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResearchManager : MonoBehaviour
{
    public static ResearchManager instance;
    public bool isAbleArcher=false;
    public bool isAbleLancer = false;
    public bool isAbleShield = false;
    public bool isAbleHeal = false;
    public bool isAbleRage = false;
    public bool isAbleArdrenaline = false;
    public bool isAbleMercenary = false;
    private float costArcher = 150f;
    private float costLancer = 200f;
    private float costShield = 200f;
    private float costHeal = 250f;
    private float costRage = 250f;
    private float costArdrenaline = 250f;
    private float costMercenary = 500f;
    public int researchDayLeft = -1;
    private int whatResearchIs = 0;
    private bool willResearch = false;
    private bool userChoice = false;
    [SerializeField] private GameObject canNotTouch;
    [SerializeField] private GameObject dialogueTab;
    public GameObject base_Canvas;
    [SerializeField] private GameObject goto_Canvas;
    [SerializeField] private GameObject skillScrollView;
    [SerializeField] private GameObject soldierScrollView;
    [SerializeField] private GameObject skillClickButton;
    [SerializeField] private GameObject soldierClickButton;
    [SerializeField] private GameObject textResearchNothingTab;
    [SerializeField] private GameObject textResearchGoingTab;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (researchDayLeft == 0)
        {
            researchDayLeft = -1;
            switch (whatResearchIs)
            {
                case 1:
                    isAbleArcher = true;
                    break;
                case 2:
                    isAbleLancer = true;
                    break;
                case 3:
                    isAbleShield = true;
                    break;
                case 4:
                    isAbleHeal = true;
                    break;
                case 5:
                    isAbleRage = true;
                    break;
                case 6:
                    isAbleArdrenaline = true;
                    break;
                case 7:
                    isAbleMercenary = true;
                    break;
                default:
                    break;
            }
        }
    }
    public void updateResearchTab()
    {
        if (researchDayLeft <= 0)
        {
            textResearchNothingTab.SetActive(true);
            textResearchGoingTab.SetActive(false);
        }
        else
        {
            textResearchNothingTab.SetActive(false);
            textResearchGoingTab.SetActive(true);
            switch (whatResearchIs)
            {
                case 1:
                    textResearchGoingTab.GetComponent<TextMeshProUGUI>().text = "To Complete Archer Research : "+researchDayLeft.ToString()+"Day left";
                    break;
                case 2:
                    textResearchGoingTab.GetComponent<TextMeshProUGUI>().text = "To Complete Lancer Research : " + researchDayLeft.ToString() + "Day left";
                    break;
                case 3:
                    textResearchGoingTab.GetComponent<TextMeshProUGUI>().text = "To Complete Shield Research : " + researchDayLeft.ToString() + "Day left";
                    break;
                case 4:
                    textResearchGoingTab.GetComponent<TextMeshProUGUI>().text = "To Complete Heal Research : " + researchDayLeft.ToString() + "Day left";
                    break;
                case 5:
                    textResearchGoingTab.GetComponent<TextMeshProUGUI>().text = "To Complete Rage Research : " + researchDayLeft.ToString() + "Day left";
                    break;
                case 6:
                    textResearchGoingTab.GetComponent<TextMeshProUGUI>().text = "To Complete Ardrenaline Research : " + researchDayLeft.ToString() + "Day left";
                    break;
                case 7:
                    textResearchGoingTab.GetComponent<TextMeshProUGUI>().text = "To Complete Mercenary Research : " + researchDayLeft.ToString() + "Day left";
                    break;
                default:
                    break;
            }
            
        }
    }
    private void makeCanNotTouch()
    {
        
        canNotTouch.SetActive(true);
        dialogueTab.SetActive(true);
        
    }
    private void makeCanTouch()
    {
        updateResearchTab();
        canNotTouch.SetActive(false);
        dialogueTab.SetActive(false);
    }
    public void clickYesButton()
    {
        switch (whatResearchIs)
        {
            case 1:
                PResourceManager.instance.MP -= costArcher;
                researchDayLeft = 2;
                break;
            case 2:
                PResourceManager.instance.MP -= costLancer;
                researchDayLeft = 2;
                break;
            case 3:
                PResourceManager.instance.MP -= costShield;
                researchDayLeft = 2;
                break;
            case 4:
                PResourceManager.instance.MP -= costHeal;
                researchDayLeft = 2;
                break;
            case 5:
                PResourceManager.instance.MP -= costRage;
                researchDayLeft = 2;
                break;
            case 6:
                PResourceManager.instance.MP -= costArdrenaline;
                researchDayLeft = 2;
                break;
            case 7:
                PResourceManager.instance.MP -= costMercenary;
                researchDayLeft = 3;
                break;
            default:
                break;
        }
        makeCanTouch();
    }
    public void clickNoButton()
    {
        makeCanTouch();
    }
    public void clickXButton()
    {
        base_Canvas.SetActive(false);
        goto_Canvas.SetActive(true);    
    }
    public void skillClick()
    {
        skillScrollView.SetActive(true);
        soldierScrollView.SetActive(false);
        skillClickButton.SetActive(false);
        soldierClickButton.SetActive(true);
    }
    public void soldierClick()
    {
        skillScrollView.SetActive(false);
        soldierScrollView.SetActive(true);
        skillClickButton.SetActive(true);
        soldierClickButton.SetActive(false);
    }
    private bool isResearchAble()
    {
        if (researchDayLeft == -1)
        {
            return true;
        }
        else
        {
            Debug.Log("�ٸ� ������ �����߿� �ּ�");
            return false;
        }
    }
    private bool isMoneyAble(float money,float cost)
    {
        if (money >= cost)
        {
            return true;
        }
        else
        {
            Debug.Log("���� ����");
            return false;
        }
    }

    public void researchArcher()
    {
        if (isAbleArcher)
        {
            Debug.Log("������ ����");
            return;
        }
        if (isResearchAble()&&isMoneyAble(PResourceManager.instance.MP, costArcher))
        {
            makeCanNotTouch();
            whatResearchIs = 1;
        }
    }
    public void researchLancer()
    {
        if (isAbleLancer)
        {
            Debug.Log("������ ����");
            return;
        }
        if (isResearchAble() && isMoneyAble(PResourceManager.instance.MP, costLancer))
        {
            makeCanNotTouch();
            whatResearchIs = 2;
        }
    }

    public void researchShield()
    {
        if (isAbleShield)
        {
            Debug.Log("������ ����");
            return;
        }
        if (isResearchAble() && isMoneyAble(PResourceManager.instance.MP, costShield))
        {

            makeCanNotTouch();
            whatResearchIs = 3;

        }
    }
    public void researchHeal()
    {
        if (isAbleHeal)
        {
            Debug.Log("������ ����");
            return;
        }
        if (isResearchAble() && isMoneyAble(PResourceManager.instance.MP, costHeal))
        {
            makeCanNotTouch();
            whatResearchIs = 4;

        }
    }
    public void researchRage()
    {
        if (isAbleRage)
        {
            Debug.Log("������ ����");
            return;
        }
        if (isResearchAble() && isMoneyAble(PResourceManager.instance.MP, costRage))
        {
            makeCanNotTouch();
            whatResearchIs = 5;

        }
    }
    public void researchArdrenaline()
    {

        if (isAbleArdrenaline)
        {
            Debug.Log("������ ����");
            return;
        }
        if (isResearchAble() && isMoneyAble(PResourceManager.instance.MP, costArdrenaline))
        {
            makeCanNotTouch();
            whatResearchIs = 6;

        }
    }
    public void researchMercenary()
    {
        if (isAbleMercenary)
        {
            Debug.Log("������ ����");
            return;
        }
        if (isResearchAble() && isMoneyAble(PResourceManager.instance.MP, costMercenary))
        {
            makeCanNotTouch();
            whatResearchIs = 7;

        }
    }

}
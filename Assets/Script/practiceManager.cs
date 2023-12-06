using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class practiceManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI practiceDialogueText;
    [SerializeField] private TextMeshProUGUI practiceDialogueText2;
    [SerializeField] private GameObject practiceDialogueObj;
    [SerializeField] private GameObject practiceCanvas;
    [SerializeField] private GameObject normalCanvas;
    [SerializeField] private GameObject hideImage;
    [SerializeField] private GameObject haveGoldObj;
    [SerializeField] private GameObject gainGoldObj;
    [SerializeField] private GameObject attackObj;
    [SerializeField] private GameObject tutorialObj;
    [SerializeField] private GameObject totalCheck;
    [SerializeField] private GameObject firstCheck;
    [SerializeField] private GameObject secondCheck;
    [SerializeField] private GameObject thirdCheck;
    [SerializeField] private GameObject fourthCheck;
    [SerializeField] private GameObject explanation;
    private bool isDialogue = false;
    private bool stopWhatText = false;
    private bool firstDone = false;
    private bool secondDone = false;
    private bool thirdDone = false;
    private bool fourthDone = false;
    private bool isExplanation = false;
    private int whatTextToShow = -1;
    private int whatTextToShow2 = 0;
    private string[] textSet1 = new string[5];
    private string[] textSet2 = new string[5];
    private void Start()
    {
        PResourceManager.instance.MP = 100000000;
        StartCoroutine(NormalChat("HI, Welcome To Era Clash"));
        textSet1[0] = "If we've met before or you know how to play this game, please press the skip button at the top.";
        textSet1[1] = "If not, let me introduce you Ela Clash.";
        textSet1[2] = "Era Clash is a game where you produce resources, construct buildings, or conduct research.";
        textSet1[3] = "The goal is to destroy the opponent's castle before they destroy yours.";
        textSet1[4] = "I'll show you the screen that you'll see in the actual game.";

        textSet2[0] = "Can you see a lot of buttons? You need to create your own army through construction and research on the left.";
        textSet2[1] = "Of course, to do that, you need the resources shown in the top right, but since this is a practice mode, I'll give you unlimited resources.";
        textSet2[2] = "And in the bottom right, you can end your turn or launch an attack. However, since there are no enemies in practice mode, I'll remove this button as well.";
        textSet2[3] = "To clear this practice mode, all you have to do is complete the checklist in the top right.";
        textSet2[4] = "Good luck!";
        
    }
    private void Update()
    {
        
        //research lancer
        if (ResearchManager.instance.isAbleLancer&& !firstDone &&!isExplanation)
        {
            firstDone = true;
            firstCheck.SetActive(true);
            isExplanation = true;
            explanation.SetActive(true);
            explanation.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Well done. Through research, you can acquire buildings that produce stronger soldiers!";
            StartCoroutine(explainationText());
        }
        //construct lancer building
        if ((PBuildingManager.instance.L_building.Count != 0 || PBuildingManager.instance.Fortress_L_building.Count != 0)&&!secondDone && !isExplanation)
        {
            secondDone = true;
            secondCheck.SetActive(true);
            isExplanation = true;
            explanation.SetActive(true);
            explanation.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Well done. By constructing the unlocked buildings through research, you can produce powerful soldiers!";
            StartCoroutine(explainationText());
        }
        //construct any building in forward fortress
        if ((PBuildingManager.instance.Fortress_P_building.Count != 0 || PBuildingManager.instance.Fortress_L_building.Count != 0|| PBuildingManager.instance.Fortress_A_building.Count!=0|| PBuildingManager.instance.Fortress_S_building.Count != 0)&&!thirdDone && !isExplanation)
        {
            thirdDone = true;
            thirdCheck.SetActive(true);
            isExplanation = true;
            explanation.SetActive(true);
            explanation.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Well done. Not only your main base but also the forts in front can be constructed with your buildings. However, if the front fort is destroyed by the enemy's attack, the adjacent buildings will also be destroyed.";
            StartCoroutine(explainationText());

        }
        //research any skill in combat
        if ((ResearchManager.instance.isAbleArdrenaline || ResearchManager.instance.isAbleHeal || ResearchManager.instance.isAbleRage)&&!fourthDone && !isExplanation)
        {
            fourthDone = true;
            fourthCheck.SetActive(true);
            isExplanation = true;
            explanation.SetActive(true);
            explanation.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Well done. The skills you researched can lead to opportunities for a comeback in war if used in crucial situations!";
            StartCoroutine(explainationText());
        }

        if (firstDone && fourthDone && secondDone && thirdDone && !isExplanation)
        {
            totalCheck.SetActive(true);
            explanation.SetActive(true);
            isExplanation = true;
            explanation.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Well done. Use what you have learned so far to achieve victory in actual gameplay!";
            StartCoroutine(checkDone());
        }
        if (!stopWhatText)
        {
            if (whatTextToShow2 > 4)
            {
                practiceDialogueObj.SetActive(false);
                hideImage.SetActive(false);
                stopWhatText = true;
            }
            else if (whatTextToShow > 6)
            {
                Debug.Log(whatTextToShow2);
                if (whatTextToShow2 == 2)
                {
                    haveGoldObj.SetActive(false);
                    gainGoldObj.SetActive(false);
                }
                else if (whatTextToShow2 == 3)
                {
                    Destroy(attackObj);
                }
                else if (whatTextToShow2 == 4)
                {
                    tutorialObj.SetActive(true);
                }
                if (Input.GetMouseButtonDown(0) && !isDialogue)
                {
                    StartCoroutine(NormalChat2(textSet2[whatTextToShow2]));
                }
            }
            else if (whatTextToShow == 6)
            {
                practiceCanvas.SetActive(false);
                normalCanvas.SetActive(true);
                StartCoroutine(NormalChat2(textSet2[whatTextToShow2]));
                whatTextToShow++;
            }
            else if (whatTextToShow == 5)
            {
                if (Input.GetMouseButtonDown(0) && !isDialogue)
                {
                    whatTextToShow++;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && !isDialogue)
                {
                    StartCoroutine(NormalChat(textSet1[whatTextToShow]));
                }
            }
        }
    }

    IEnumerator explainationText()
    {

        yield return new WaitForSecondsRealtime(10f);
        explanation.SetActive(false);
        isExplanation = false;
    }
    IEnumerator checkDone()
    {

        yield return new WaitForSecondsRealtime(5f);
        clickSkipButton();
    }
    IEnumerator NormalChat2(string dialogue_text)
    {
        whatTextToShow2++;
        isDialogue = true;
        string writerText = "";
        int a = 0;
        for (a = 0; a < dialogue_text.Length; a++)
        {
            writerText += dialogue_text[a];
            practiceDialogueText2.text = writerText;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(0.1f);
        isDialogue = false;
    }
    IEnumerator NormalChat(string dialogue_text)
    {
        whatTextToShow++;
        isDialogue = true;
        string writerText = "";
        int a = 0;
        for (a = 0; a < dialogue_text.Length; a++)
        {
            writerText += dialogue_text[a];
            practiceDialogueText.text = writerText;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(0.1f);
        isDialogue = false;
    }

    public void clickSkipButton()
    {
        SceneManager.LoadScene("Person choose");
    }
}

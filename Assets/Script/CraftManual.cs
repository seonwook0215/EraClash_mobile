using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Craft
{
    public string craftName;//이름
    public GameObject go_Prefab;//실제 설치될  프리팹.
    public GameObject go_PreviewPrefab;//미리보기 프리팹
    public int[] craftNeedMP;
    
}
public class CraftManual : MonoBehaviour
{
    [Space(10)] [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip build_success;
    public Camera mainCamera;
    public Camera firstCamera;
    public Camera secondCamera;
    public Camera movingCamera;
    private float Speed = 750.0f;
    private Vector3 BuildingUP = new Vector3(0, 160f, 0);
    private Vector3 BuildingDown = new Vector3(0, -150f, 0);
    public static CraftManual instance;

    bool isCameraMovingA = false;
    private bool isActivated = false;
    private bool isPreviewActivated = false;

    private int selectedSlotNumber;
    [SerializeField]
    private GameObject go_BaseUI;
    [SerializeField] private GameObject BuildingTab;
    [SerializeField] private GameObject GetUpBuildingTab;
    [SerializeField] private GameObject GetDownBuildingTab;
    [SerializeField] private GameObject GetDownCamera;
    [SerializeField] private GameObject GetUpCamera;
    [SerializeField] private GameObject ReturnButton;
    [SerializeField] private GameObject BuildingList;
    [SerializeField] private GameObject bannedArcher;
    [SerializeField] private GameObject bannedLancer;
    [SerializeField] private GameObject bannedShield;
    [SerializeField] private GameObject yesorno;
    public GameObject canNotTouchTab;
    private bool IsDownButtonActive = true;


    [SerializeField]
    private Craft[] craft_building;

    private GameObject go_Preview;//미리보기 담을 변수
    private GameObject go_Prefab; // 실제 생성될 프리팹을 담을 변수 
    //RayCast 필요 변수 선언
    private RaycastHit hitInfo;

     [SerializeField]
     private LayerMask layerMask;
    //private int layerMask;
    private void Awake()
    {
        instance = this;
    }
    private void checkBanResearch()
    {
        if (ResearchManager.instance.isAbleArcher)
        {
            bannedArcher.SetActive(false);
        }
        if (ResearchManager.instance.isAbleLancer)
        {
            bannedLancer.SetActive(false);
        }
        if (ResearchManager.instance.isAbleShield)
        {
            bannedShield.SetActive(false);
        }
    }
    public void clickGetDownButton()
    {
        GetDownCamera.SetActive(false);
        GetUpCamera.SetActive(true);
        IsDownButtonActive = false;
    }
    public void clickGetUpButton()
    {
        GetDownCamera.SetActive(true);
        GetUpCamera.SetActive(false);
        IsDownButtonActive = true;
    }

    public void SlotClick(int _slotNumber)
    {
        Vector3 mousePositionScreen = Input.mousePosition;
        selectedSlotNumber = _slotNumber;
        // 마우스 좌표를 월드 좌표로 변환합니다.
        var mousePos = Input.mousePosition;
        if (firstCamera.enabled)
        {
            mousePos.z = firstCamera.transform.position.y - 20;
            Vector3 mousePositionWorld = firstCamera.ScreenToWorldPoint(mousePos);
            go_Preview = Instantiate(craft_building[_slotNumber].go_PreviewPrefab, mousePositionWorld, Quaternion.identity);
        }
        else
        {
            mousePos.z = secondCamera.transform.position.y - 20;
            Vector3 mousePositionWorld = secondCamera.ScreenToWorldPoint(mousePos);
            go_Preview = Instantiate(craft_building[_slotNumber].go_PreviewPrefab, mousePositionWorld, Quaternion.identity);
        }
        yesorno.SetActive(true);
        go_Prefab = craft_building[_slotNumber].go_Prefab;
        isPreviewActivated = true;
        canNotTouchTab.SetActive(false);
        BuildingTab.SetActive(false);
        BuildingList.SetActive(false);
        ReturnButton.SetActive(false);
        GetUpCamera.SetActive(false);
        GetDownCamera.SetActive(false);
        TouchDownBuildingTabButton();
    }
    void Update()
    {
        TurnManager.instance.ChangeGold();
        TurnManager.instance.ChangeGainGold();
        TurnManager.instance.ChangeBuildingText();
        //GetMouseCursorpos();
      /*  if (movingCamera.enabled||TurnManager.instance.StartWar)
        {
            Cancel();
        }
        else
        {
            Window();
        }*/

        if (isPreviewActivated)
            PreviewPositionUpdate();
       
    }
    public void touchYes()
    {
        Build();
        yesorno.SetActive(false);
    }
    public void touchNo()
    {
        Cancel();
        BuildingTab.SetActive(true);
        BuildingList.SetActive(true);
        ReturnButton.SetActive(true);
        if (IsDownButtonActive)
            GetDownCamera.SetActive(true);
        else
            GetUpCamera.SetActive(true);
        yesorno.SetActive(false);

    }
    public void TouchUpBuildingTabButton()
    {
        checkBanResearch();
        GetUpBuildingTab.SetActive(false);
        GetDownBuildingTab.SetActive(true);
        canNotTouchTab.SetActive(true);
        StartCoroutine(MoveUIDownToUpForA(BuildingTab, BuildingDown, BuildingUP));
    }    
    public void TouchDownBuildingTabButton()
    {
        GetUpBuildingTab.SetActive(true);
        GetDownBuildingTab.SetActive(false);
        canNotTouchTab.SetActive(false);
        StartCoroutine(MoveUIDownToUpForA(BuildingTab, BuildingUP, BuildingDown));
    }
    IEnumerator MoveUIDownToUpForA(GameObject obj, Vector3 Start, Vector3 End)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();

        isCameraMovingA = true;

        float journeyLength = Vector3.Distance(Start, End);
        float startTime = Time.time;

        while (isCameraMovingA)
        {
            float distanceCovered = (Time.time - startTime) * Speed * 2.08f;
            float journeyFraction = distanceCovered / journeyLength;
            Vector3 newPosition = Vector3.Lerp(Start, End, journeyFraction);
            rectTransform.anchoredPosition = new Vector3(newPosition.x, newPosition.y, newPosition.z); 

            if (journeyFraction >= 1.0f)
            {
                isCameraMovingA = false;
            }

            yield return null;
        }
    }
    private void PreviewPositionUpdate()
    {
        Vector3 mousePositionScreen = Input.mousePosition;
        Vector3 mousePositionWorld;
    
        var mousePos = Input.mousePosition;

        if (firstCamera.enabled)
        {
            mousePos.z = firstCamera.transform.position.y - 20;
            mousePositionWorld = firstCamera.ScreenToWorldPoint(mousePos);
            
        }
        else
        {
            mousePos.z = secondCamera.transform.position.y-20;
            mousePositionWorld = secondCamera.ScreenToWorldPoint(mousePos);
        }

        if (Physics.Raycast(mousePositionWorld, Vector3.down, out hitInfo, Mathf.Infinity, layerMask))
        {
            Vector3 _location = hitInfo.point;
            //Debug.Log($"Raycast {hitInfo.collider.gameObject.name}");
            go_Preview.transform.position = _location;

        }   
    }
    private void Build()
    {
        if (isPreviewActivated && go_Preview.GetComponent<PreviewObject>().isBuildable())
        {
            if (PResourceManager.instance.MP - craft_building[selectedSlotNumber].craftNeedMP[0] >= 0)
            {

                PResourceManager.instance.MP = PResourceManager.instance.MP - craft_building[selectedSlotNumber].craftNeedMP[0];

                Instantiate(go_Prefab, hitInfo.point, Quaternion.identity);
                Destroy(go_Preview);
                audioSource.PlayOneShot(build_success);

                isActivated = false;
                isPreviewActivated = false;
                go_Preview = null;
                go_Prefab = null;
            }
            else
            {
                Debug.Log("돈이없습니다.");
                TurnManager.instance.DoNotHaveMoney();
                Destroy(go_Preview);
                isActivated = false;
                isPreviewActivated = false;
                go_Preview = null;
                go_Prefab = null;
            }
            BuildingTab.SetActive(true);
            BuildingList.SetActive(true);
            ReturnButton.SetActive(true);
            if(IsDownButtonActive)
                GetDownCamera.SetActive(true);
            else
                GetUpCamera.SetActive(true);
            
        }
    }
    public void Cancel()
    {
        if(isPreviewActivated)
            Destroy(go_Preview);
        isActivated = false;
        isPreviewActivated = false;
        go_Preview = null;

    }
    private void Window()
    {
        if (!isActivated && (firstCamera.enabled||secondCamera.enabled))
            OpenWindow();
        else
            CloseWindow();
    }
    public void OpenWindow()
    {
        isActivated = true;
        go_BaseUI.SetActive(true);
    }
    public void CloseWindow()
    {
        isActivated = false;
        go_BaseUI.SetActive(false);
        clickGetUpButton();
        TouchDownBuildingTabButton();
    }
}

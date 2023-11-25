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


    //camera에 맞춰서 빌딩 하면 될것같다/건물 격자 -> 건물에서 나오는 병사 스폰에다가 넣기 -> 격전의 평야 & 외곽지역 fog -> 자원량 관리 == 건물 병력 (결국에 이걸 할려면 turn 관리 manager 가 있어함)
    //시작화면 인물 선택 화면 -> 1턴 명세서 화면 -> 승리 패배화면  -> AI 넣어주기
    // Start is called before the first frame update
    private bool isActivated = false;
    private bool isPreviewActivated = false;

    private int selectedSlotNumber;
    [SerializeField]
    private GameObject go_BaseUI;


    [SerializeField]
    private Craft[] craft_building;

    private GameObject go_Preview;//미리보기 담을 변수
    private GameObject go_Prefab; // 실제 생성될 프리팹을 담을 변수 
    //RayCast 필요 변수 선언
    private RaycastHit hitInfo;

     [SerializeField]
     private LayerMask layerMask;
    //private int layerMask;

    void Start()
    {
        
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
        
        go_Prefab = craft_building[_slotNumber].go_Prefab;
        isPreviewActivated = true;
        go_BaseUI.SetActive(false);
    }
    void Update()
    {
      
        //GetMouseCursorpos();
        if (movingCamera.enabled||TurnManager.instance.StartWar)
        {
            Cancel();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab)&&!isPreviewActivated)
            {
                Window();
            }
        }
        if (Input.GetButtonDown("Fire1"))
            Build();

        if (Input.GetKeyDown(KeyCode.Escape))
            Cancel();
        if (isPreviewActivated)
            PreviewPositionUpdate();
       
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
        }
    }
    public void Cancel()
    {
        if(isPreviewActivated)
            Destroy(go_Preview);
        isActivated = false;
        isPreviewActivated = false;
        go_Preview = null;

        go_BaseUI.SetActive(false);
    }
    private void Window()
    {
        if (!isActivated && (firstCamera.enabled||secondCamera.enabled))
            OpenWindow();
        else
            CloseWindow();
    }
    private void OpenWindow()
    {
        isActivated = true;
        go_BaseUI.SetActive(true);
    }
    private void CloseWindow()
    {
        isActivated = false;
        go_BaseUI.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraSwitch : MonoBehaviour
{
    public Camera mainCamera;
    public Camera firstCamera;
    public Camera secondCamera;
    public Camera movingCamera;
    public Camera BlooshedCamera;
    public Camera EnemyAttack1Camera;
    public Camera EnemyAttack2Camera;
    public Camera PlayerAttack1Camera;
    public Camera PlayerAttack2Camera;
    private float cnt = 0;
    private bool isTransitioning = false;
    private float transitionStartTime;
    private float dragSpeed = 150.0f;
    private float attackdragSpeed = 2500f;
    void Start()
    {
        // 초기 화면 설정
        mainCamera.enabled = true;
        firstCamera.enabled = false;
        secondCamera.enabled = false;
        movingCamera.enabled = false;
        BlooshedCamera.enabled = false;
        EnemyAttack1Camera.enabled = false;
        EnemyAttack2Camera.enabled = false;
        PlayerAttack1Camera.enabled = false;
        PlayerAttack2Camera.enabled = false;
        movingCamera.transform.position = mainCamera.transform.position;
        movingCamera.transform.rotation = mainCamera.transform.rotation;
    }
    void Update()
    {
        Vector3 toMain = mainCamera.transform.position;
        Vector3 toFirst = firstCamera.transform.position;
        Vector3 toSecond = secondCamera.transform.position;
        Vector3 toBlooshed = BlooshedCamera.transform.position;
        Vector3 toEnemyAttack1 = EnemyAttack1Camera.transform.position;
        Vector3 toEnemyAttack2 = EnemyAttack2Camera.transform.position;
        Vector3 toPlayerAttack1 = PlayerAttack1Camera.transform.position;
        Vector3 toPlayerAttack2 = PlayerAttack2Camera.transform.position;
        Quaternion toMainRotate = mainCamera.transform.rotation;
        Quaternion toFirstRotate = firstCamera.transform.rotation;
        Quaternion toSecondRotate = secondCamera.transform.rotation;
        Quaternion toBlooshedRotate = BlooshedCamera.transform.rotation;
        Quaternion toEnemyAttack1Rotate = EnemyAttack1Camera.transform.rotation;
        Quaternion toEnemyAttack2Rotate = EnemyAttack2Camera.transform.rotation;
        Quaternion toPlayerAttack1Rotate = PlayerAttack1Camera.transform.rotation;
        Quaternion toPlayerAttack2Rotate = PlayerAttack2Camera.transform.rotation;
        if (mainCamera.enabled)
        {
            movingCamera.transform.position = mainCamera.transform.position;
        }
        else if (secondCamera.enabled)
        {
            movingCamera.transform.position = secondCamera.transform.position;
        }
        else if (firstCamera.enabled)
        {
            movingCamera.transform.position = firstCamera.transform.position;
        }
        else if (BlooshedCamera.enabled)
        {
            movingCamera.transform.position = BlooshedCamera.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Q) && !isTransitioning && cnt == 0)
        {
            if (firstCamera.enabled)//1 짓기 화면이 켜있다면
            {
                mainCamera.enabled = true;
                secondCamera.enabled = false;
                firstCamera.enabled = false;
                StartCoroutine(TransitionCameras(toMain, toMainRotate));
            }
            else
            {
                secondCamera.enabled = false;
                mainCamera.enabled = false;
                firstCamera.enabled = true;
                StartCoroutine(TransitionCameras(toFirst, toFirstRotate));

            }
        }
        if (Input.GetKeyDown(KeyCode.W) && !isTransitioning && cnt == 0&&PUnitManager.instance.fortress)
        {
            if (secondCamera.enabled)
            {
                mainCamera.enabled = true;
                secondCamera.enabled = false;
                firstCamera.enabled = false;
                StartCoroutine(TransitionCameras(toMain, toMainRotate));

            }
            else
            {
                mainCamera.enabled = false;
                firstCamera.enabled = false;
                secondCamera.enabled = true;
                StartCoroutine(TransitionCameras(toSecond, toSecondRotate));
            }

        }
        if (!TurnManager.instance.StartWar && cnt == 1)
        {
            BlooshedCamera.enabled = false;
            EnemyAttack1Camera.enabled = false;
            EnemyAttack2Camera.enabled = false;
            PlayerAttack1Camera.enabled = false;
            PlayerAttack2Camera.enabled = false;
            mainCamera.enabled = true;
            StartCoroutine(FastTransitionCameras(toMain, toMainRotate));
            cnt = 0;
        }
        if (TurnManager.instance.StartWar && cnt == 0)

        {
            cnt = 1;
            Debug.Log("change");
            mainCamera.enabled = false;
            firstCamera.enabled = false;
            secondCamera.enabled = false;
            BlooshedCamera.enabled = false;
            EnemyAttack1Camera.enabled = false;
            EnemyAttack2Camera.enabled = false;
            PlayerAttack1Camera.enabled = false;
            PlayerAttack2Camera.enabled = false;
            Debug.Log(BattleManager.instance.cameranum);

            switch (BattleManager.instance.cameranum)
            {
                case 0:
                    BlooshedCamera.enabled = true;
                    //StartCoroutine(FastTransitionCameras(toBlooshed, toBlooshedRotate));
                    break;
                case 1:
                    PlayerAttack1Camera.enabled = true;
                    StartCoroutine(TransitionCameras(toPlayerAttack1, toPlayerAttack1Rotate));
                    break;
                case 2:
                    PlayerAttack2Camera.enabled = true;
                    StartCoroutine(TransitionCameras(toPlayerAttack2, toPlayerAttack2Rotate));
                    break;
                case 3:
                    EnemyAttack1Camera.enabled = true;
                    StartCoroutine(TransitionCameras(toEnemyAttack1, toEnemyAttack1Rotate));
                    break;
                case 4:
                    EnemyAttack2Camera.enabled = true;
                    StartCoroutine(TransitionCameras(toEnemyAttack2, toEnemyAttack2Rotate));
                    break;
                default:
                    break;
            }
        }
    }
    IEnumerator TransitionCameras(Vector3 targerPosition, Quaternion targetRotation)
    {
        isTransitioning = true;
        movingCamera.enabled = true;
        Vector3 initialPosition = movingCamera.transform.position;
        Quaternion initialRotation = movingCamera.transform.rotation;

        float startTime = Time.time;
        float journeyLength = Vector3.Distance(initialPosition, targerPosition);
        while (isTransitioning)
        {
            float distanceCovered;
            distanceCovered = (Time.time - startTime) * dragSpeed;
            
            float journeyFraction = distanceCovered / journeyLength;
            movingCamera.transform.position = Vector3.Lerp(initialPosition, targerPosition, journeyFraction);
            movingCamera.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, journeyFraction);
            if (journeyFraction >= 1.0f)
            {
                movingCamera.enabled = false;
                isTransitioning = false;
            }
            yield return null;
        }

    }
    IEnumerator FastTransitionCameras(Vector3 targerPosition, Quaternion targetRotation)
    {
        isTransitioning = true;
        movingCamera.enabled = true;
        Vector3 initialPosition = movingCamera.transform.position;
        Quaternion initialRotation = movingCamera.transform.rotation;

        float startTime = Time.time;
        float journeyLength = Vector3.Distance(initialPosition, targerPosition);
        while (isTransitioning)
        {
            float distanceCovered;
            
            distanceCovered = (Time.time - startTime) * attackdragSpeed;
            float journeyFraction = distanceCovered / journeyLength;
            movingCamera.transform.position = Vector3.Lerp(initialPosition, targerPosition, journeyFraction);
            movingCamera.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, journeyFraction);
            if (journeyFraction >= 1.0f)
            {
                movingCamera.enabled = false;
                isTransitioning = false;
            }
            yield return null;
        }

    }
}
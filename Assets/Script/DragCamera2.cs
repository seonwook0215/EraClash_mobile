using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DragCamera2 : MonoBehaviour
{
    public Camera first;
    Vector2 clickPoint;
    float dragSpeed = 100.0f;
    Vector3 lineStart = new Vector3(-71f, 80f, -36.5f);
    Vector3 lineEnd = new Vector3(-70.5f, 71f, 16.5f);
    

    // 추가 변수: 카메라 이동 중인지 여부를 나타내는 플래그
    bool isCameraMoving = false;

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            clickPoint = Input.mousePosition;

            // 마우스 버튼을 누를 때 카메라 이동 플래그 초기화
            isCameraMoving = false;
        }

        if (Input.GetMouseButton(0)&&first.enabled)
        {
            Vector3 position = first.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint);
            position.z = position.y;
            position.y = 0.0f;

            Vector3 move = position * (Time.deltaTime * dragSpeed);

            float y = transform.position.y;

            transform.Translate(move);
            Vector3 newPosition = new Vector3(transform.position.x, y, transform.position.z);

            // 드래그 방향 계산
            Vector2 currentMousePosition = Input.mousePosition;
            Vector2 dragDirection = currentMousePosition - clickPoint;

            clickPoint = currentMousePosition;

            // 왼쪽에서 오른쪽으로 드래그하는 경우
            if (dragDirection.x > 0)
            {
                // 카메라 이동 중인 경우에만 lineStart로 이동
                if (!isCameraMoving)
                {
                    StartCoroutine(MoveCameraToPosition(lineStart));
                }
            }
            // 오른쪽에서 왼쪽으로 드래그하는 경우
            else if (dragDirection.x < 0)
            {
                if (!isCameraMoving)
                {
                    StartCoroutine(MoveCameraToPosition(lineEnd));
                }
            }
        }
    }

    // 카메라를 목표 위치로 부드럽게 이동시키는 코루틴
    IEnumerator MoveCameraToPosition(Vector3 targetPosition)
    {
        isCameraMoving = true;
        Vector3 initialPosition = transform.position;
        float journeyLength = Vector3.Distance(initialPosition, targetPosition);
        float startTime = Time.time;

        while (isCameraMoving)
        {
            float distanceCovered = (Time.time - startTime) * dragSpeed;
            float journeyFraction = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, journeyFraction);

            if (journeyFraction >= 1.0f)
            {
                // 이동이 완료되면 이동 플래그 종료
                isCameraMoving = false;
            }

            yield return null;
        }
    }
}

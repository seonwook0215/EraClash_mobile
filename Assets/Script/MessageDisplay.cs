    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public Text messageText;
    
    private bool isDisplayingMessage = false;

    private void Update()
    {
        // 메시지를 표시 중이라면 타이머를 시작
        if (isDisplayingMessage)
        {
            StartCoroutine(DisplayMessageForSeconds(3f)); // 3초 동안 메시지를 표시합니다.
        }
    }

    public void ShowMessage()
    {
        messageText.text = "ㅎㅇㅎㅇ"; // 표시할 메시지를 여기에 입력
        isDisplayingMessage = true;
    }

    IEnumerator DisplayMessageForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        messageText.text = ""; // 메시지를 숨깁니다.
        isDisplayingMessage = false;
    }   
}

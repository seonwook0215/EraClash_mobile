using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PersonController : MonoBehaviour
{
    public static PersonController instance;
    [Space(50)] public Animator main_animator;
    [Space(10)] [SerializeField] string sceneToLoad;
    public CanvasGroup homePanel;

    [SerializeField] private GameObject[] person;
    public int cur = 0;
    private bool change = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Duplicated PersonController, ignoring this one", gameObject);
        }
    }
    void Update()
    {
        if (change)
        {
            ActivePerson();
            change = false;
        }
    }

    public void LeftClick()
    {
        Debug.Log(cur);
        if (cur - 1 >= 0)
        {
            cur--;
            change = true;
        }
    }

    public void RightClick()
    {
        if (cur + 1 < person.Length)
        {
            cur++;
            change = true;
        }
    }

    public void ChoosePerson()
    {

            main_animator.enabled = true;
            main_animator.SetTrigger("LoadScene");
            StartCoroutine(WaitToLoadLevel());
        }

        IEnumerator WaitToLoadLevel()
        {
            yield return new WaitForSeconds(1.5f);

            // Scene Load
            SceneManager.LoadScene(sceneToLoad);
            //DontDestroyOnLoad(personobj);
        }

    private void ActivePerson()
    {
        for (int i = 0; i < person.Length; i++)
        {
            person[i].SetActive(i == cur);
        }
    }


}

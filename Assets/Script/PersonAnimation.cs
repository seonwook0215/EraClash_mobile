using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PersonAnimation : MonoBehaviour
    {
        [SerializeField] PersonController personManager;

        public void _fadingAnimationIsDone()
        {
            personManager.main_animator.enabled = false;
            personManager.homePanel.blocksRaycasts = true;
        }
    }
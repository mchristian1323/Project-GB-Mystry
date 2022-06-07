using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenu;
        [SerializeField] GameObject theoryList;
        [SerializeField] GameObject evidenceList;

        bool isPaused;
        bool isTheory;
        bool isEvidence;

        private void Start()
        {
            pauseMenu.SetActive(false);
            theoryList.SetActive(false);
            evidenceList.SetActive(false);
        }

        public void PauseGame()
        {
            if(isPaused)
            {
                isPaused = !isPaused;
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                isPaused = !isPaused;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        public void QuitGame()
        {
            //save game first
            Application.Quit();
        }

        public void TheoryList()
        {
            if (isTheory)
            {
                isTheory = !isTheory;
                theoryList.SetActive(false);
            }
            else
            {
                isTheory = !isTheory;
                theoryList.SetActive(true);
            }
        }

        public void EvidenceList()
        {
            if (isEvidence)
            {
                isEvidence = !isEvidence;
                evidenceList.SetActive(false);
            }
            else
            {
                isEvidence = !isEvidence;
                evidenceList.SetActive(true);
            }
        }
    }
}

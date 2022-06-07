using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheoryList;
using Saving;

namespace UI.Theory
{
    public class TheoryListManager : MonoBehaviour, ISaveable
    {
        List<TheoryList.Theory> theoriesList = new List<TheoryList.Theory>();
        [SerializeField] TheoryUIObject theoryPrefab;
        [SerializeField] Transform theoryContainer;

        // Start is called before the first frame update
        void Awake()
        {
            RefreshTheories();
        }

        private void RefreshTheories()
        {
            foreach (Transform child in theoryContainer)
            {
                Destroy(child.gameObject);
            }

            for(int i = 0; i < theoriesList.Count; i++)
            {
                var thisTheory = Instantiate(theoryPrefab, theoryContainer);

                thisTheory.SetThisThoery(theoriesList[i]);
            }
        }

        public void AddTheory(TheoryList.Theory newTheory)
        {
            theoriesList.Add(newTheory);
            RefreshTheories();
        }

        public object CaptureState()
        {
            var slotStrings = new string[theoriesList.Count];
            for (int i = 0; i < theoriesList.Count; i++)
            {
                if (theoriesList[i] != null)
                {
                    slotStrings[i] = theoriesList[i].GetTheoryID();
                }
            }

            return slotStrings;
        }

        public void RestoreState(object state)
        {
            var slotStrings = (string[])state;
            for (int i = 0; i < theoriesList.Count; i++)
            {
                theoriesList[i] = TheoryList.Theory.GetFromID(slotStrings[i]);
            }
            RefreshTheories();
        }
    }
}
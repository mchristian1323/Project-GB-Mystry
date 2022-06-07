using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheoryList;
using TMPro;

namespace UI.Theory
{
    public class TheoryUIObject : MonoBehaviour
    {
        TheoryList.Theory thisTheory;

        TextMeshProUGUI thisTextFeild;

        private void Start()
        {
            thisTextFeild = GetComponent<TextMeshProUGUI>();
        }

        public void SetThisThoery(TheoryList.Theory theory)
        {
            thisTheory = theory;

            SetTheorySettings();
        }

        private void SetTheorySettings()
        {
            thisTextFeild.text = thisTheory.GetTheory();
        }
    }
}

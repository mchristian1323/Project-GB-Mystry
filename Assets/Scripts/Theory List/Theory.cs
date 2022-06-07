using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheoryList
{
    public class Theory : ScriptableObject
    {
        [SerializeField] string theory;
        [SerializeField] string description;

        public enum Ending
        {
            Worst,
            Bad,
            Good,
            Just,
        }

        [SerializeField] Ending ending;

        //getters
        public string GetTheory()
        {
            return theory;
        }    

        public string GetDescription()
        {
            return description;
        }

        public Ending GetEnding()
        {
            return ending;
        }
    }
}
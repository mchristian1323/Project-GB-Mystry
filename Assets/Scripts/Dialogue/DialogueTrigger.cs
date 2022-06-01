using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        //add this to an object that will respond to an exit or entry action (dialogue unlock, theory, ect)
        //add an event, type in the string, find the function/object you want to trigger, set it to what you need to set it
        //example: Udemy course => add this to an ai, add attack to the action string, add the fighter enable script to the trigger
            //set its enemy script, then set it to enabled.

        [SerializeField] string action;
        [SerializeField] UnityEvent onTrigger;

        public void Trigger(string actionToTrigger)
        {
            if(actionToTrigger == action)
            {
                onTrigger.Invoke();
            }
        }
    }
}

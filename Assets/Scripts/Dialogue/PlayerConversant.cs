using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

namespace Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        //This Resides on the player, make sure the tag matches in the dialogueUI script

        //for testing purposes
        //[SerializeField] Dialogue testDialogue;
        //[SerializeField] AIConversant testConversant;

        [SerializeField] string playerName;
        Dialogue currentDialogue;
        DialogueNode currentNode = null;
        AIConversant currentConversant = null;
        bool isChoosing = false;

        public event Action onConversationUpdated;

        private void Start()
        {
            //StartDialogue(testConversant, testDialogue);
        }

        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            GetComponent<Control.PlayerControl>().SetAct(false); //unique to GB Express

            currentConversant = newConversant;
            currentDialogue = newDialogue;
            currentNode = currentDialogue.GetRootNode();
            TriggerEnterActions();
            onConversationUpdated();
        }

        public void Quit()
        {
            GetComponent<Control.PlayerControl>().SetAct(true); //unique to GB Express

            currentDialogue = null;
            TriggerExitActions();
            currentNode = null;
            isChoosing = false;
            currentConversant = null;
            onConversationUpdated();
        }

        public bool IsActive()
        {
            return currentDialogue != null;
        }

        public string GetCurrentConversantName()
        {
            if(isChoosing)
            {
                return playerName;
            }
            else
            {
                return currentConversant.GetName();
            }
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }
        
        public IEnumerable<DialogueNode> GetChoices()
        {
            return currentDialogue.GetPlayerChildren(currentNode);
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            TriggerEnterActions();
            isChoosing = false;
            Next(); //This skips the step of putting the dialogue of the selection on screen and goes to the next entry
        }

        public string GetText()
        {
            if(currentNode == null)
            {
                return "";
            }

            return currentNode.GetText();
        }

        public void Next()
        {
            int numPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();

            if(numPlayerResponses > 0)
            {
                isChoosing = true;
                TriggerExitActions();
                onConversationUpdated();
                return; 
            }

            DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
            int randomIndex = UnityEngine.Random.Range(0, children.Count());
            TriggerExitActions();
            currentNode =  children[randomIndex];
            TriggerEnterActions();
            onConversationUpdated();
        }

        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }

        private void TriggerEnterActions()
        {
            if(currentNode != null)
            {
                TriggerAction(currentNode.GetOnEnterActions());
            }
        }

        private void TriggerExitActions()
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnExitActions());
            }
        }

        private void TriggerAction(string action)
        {
            if(action == "")
            {
                return;
            }

            foreach(DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action);
            }
        }
    }
}

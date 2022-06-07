using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactions;
using Dialogue;

namespace GBExpress
{
    public class ConversantNPC : Interactables
    {
        public override void Interact()
        {
            GetComponent<AIConversant>().HandleDialogue();
        }
    }
}

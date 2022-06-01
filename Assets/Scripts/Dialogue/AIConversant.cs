using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class AIConversant : MonoBehaviour
    {
        /*
         the way they handle this script: They bring in an interface, which handles the cursor type and the raycast
        i want the target to be clickable and activated via controller so i want to handle it through my interactable
        scripts

        if want to add the raycastable detials, go to lecture 46 in the unity dialogue and quests C# course

        have the handle dialoge script be activated by the interaction system
         * */

        [SerializeField] Dialogue dialogue = null;
        [SerializeField] string characterName;

        public void HandleDialogue()
        {
            if (dialogue == null)
            {
                return;
            }

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
        }

        public string GetName()
        {
            return characterName;
        }
    }
}

using UnityEngine;

namespace Interactions
{
    //make a script for an interactable object, then make it derive from Interactables instead of Monobehaviour
    //then make a public override void Interact in that script and fill it with desired function
    //the on your player/controller script, when you hit an object with the scanner, have the lines
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, tempTargetSpot.position, 1f, interactionLayer);
        //if(hit)
        //Interactables interactable = hit.collider.GetComponent<Interactables>();
        //if (interactable != null) { interactable.Interact(); }
        //this will give you the desired effect within the interactable script
    public abstract class Interactables : MonoBehaviour
    {
        public abstract void Interact();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Interactions;
using UI;
using InventorySystem;

namespace Control
{
    public class PlayerControl : MonoBehaviour
    {
        //public
        public bool canAct = true;

        public Vector2 mousePos;

        //Serialized
        [Header("Player Stats")]
        [SerializeField] float playerCurrentSpeed;
        [Header("Menus")]
        [SerializeField] GameObject pauseMenu;
        [SerializeField] GameObject theoryMenu;
        [SerializeField] GameObject evidenceMenu;
        [Header("Layers")]
        [SerializeField] LayerMask interactionLayer;
        [Header("Menu")]
        [SerializeField] MenuManager myMenuManager;
        [Header("Test")]
        [SerializeField] ItemData testItem;

        //private
        Vector2 controllerMovement;

        Collider2D currentCollision;

        Rigidbody2D myRigidbody;
        PlayerInput myPlayerInput;
        UltraInventory myUltraInventory;

        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
            myPlayerInput = GetComponent<PlayerInput>();
            myUltraInventory = GetComponent<UltraInventory>();

            myPlayerInput.actions["Select"].started += OnSelect;
            myPlayerInput.actions["Back"].started += OnBack;
            myPlayerInput.actions["Evidence"].performed += OnEvidence;
            myPlayerInput.actions["Pause"].performed += OnPause;
            myPlayerInput.actions["Theory"].performed += OnTheory;
        }

        private void Start()
        {
            FindObjectOfType<UI_UltraInventory>().SetInventory(myUltraInventory);
        }

        private void FixedUpdate()
        {
            if(canAct)
            {
                Movement();
            }
        }

        //gets this from the controller
        public void OnMove(InputAction.CallbackContext context)
        {
            controllerMovement = context.ReadValue<Vector2>();
        }

        //for the fixed update function
        private void Movement()
        {
            float xThrow = controllerMovement.x;
            float yThrow = controllerMovement.y;

            Vector2 playerVelocity = new Vector2(xThrow * playerCurrentSpeed, yThrow * playerCurrentSpeed);
            myRigidbody.velocity = playerVelocity;
        }

        private void OnSelect(InputAction.CallbackContext context)
        {
            if(canAct)
            {
                if (currentCollision != null && currentCollision.tag == "NPC")
                {
                    Interactables interactable = currentCollision.GetComponent<Interactables>();

                    if (interactable != null)
                    {
                        interactable.Interact();
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            currentCollision = collision;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            currentCollision = null;
        }

        private void OnBack(InputAction.CallbackContext context)
        {
            //close menu
            Debug.Log("bakc");
        }

        private void OnPause(InputAction.CallbackContext context)
        {
            myMenuManager.PauseGame();
        }

        private void OnTheory(InputAction.CallbackContext context)
        {
            myMenuManager.TheoryList();
        }

        private void OnEvidence(InputAction.CallbackContext context)
        {
            myMenuManager.EvidenceList();;
        }

        public void SetAct(bool act)
        {
            canAct = act;
        }

        public void OnMousePos(InputAction.CallbackContext context)
        {
            mousePos = context.ReadValue<Vector2>();
        }
    }
}
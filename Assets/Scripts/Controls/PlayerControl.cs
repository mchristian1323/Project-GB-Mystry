using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Control
{
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] float playerCurrentSpeed;

        Vector2 controllerMovement;

        PlayerControls myPlayerControls;
        Rigidbody2D myRigidbody;

        private void Awake()
        {
            myPlayerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            myPlayerControls.Enable();
        }

        private void OnDisable()
        {
            myPlayerControls.Disable();
        }

        private void Start()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            float xThrow = controllerMovement.x;
            float yThrow = controllerMovement.y;

            Vector2 playerVelocity = new Vector2(xThrow * playerCurrentSpeed, yThrow * playerCurrentSpeed);
            myRigidbody.velocity = playerVelocity;
        }

        private void OnMove(InputValue value)
        {
            controllerMovement = value.Get<Vector2>();
        }

        private void OnSelect()
        {

        }

        private void OnBack()
        {

        }

        private void OnPause()
        {

        }

        private void OnTheory()
        {

        }

        private void OnEvidence()
        {

        }
    }
}

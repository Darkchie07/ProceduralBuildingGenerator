using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the movement speed.
    private Transform mainCamera;
    public GameObject _input;
    public GameObject _save;

    private void Start()
    {
        mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        // Get input for movement.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the camera's forward and right vectors.
        Vector3 cameraForward = mainCamera.forward;
        Vector3 cameraRight = mainCamera.right;

        // Ignore the y-component of vectors to ensure movement stays on the ground.
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        // Normalize vectors to ensure consistent speed regardless of camera direction.
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate movement direction based on camera rotation.
        Vector3 moveDirection = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;

        // Apply movement.
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            _input.SetActive(true);
            this.GetComponent<CharacterMovement>().enabled = false;
        }else if (Input.GetKeyDown(KeyCode.B))
        {
            _save.SetActive(true);
            this.GetComponent<CharacterMovement>().enabled = false;
        }

    }
}

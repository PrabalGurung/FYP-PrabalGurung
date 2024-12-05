/* 
Controls player movement and animation

Works Remaining:        - NA     
Problems:               - NA
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;  
    private Animator animator;
    private MenuController menuController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        menuController = FindObjectOfType<MenuController>();
    }

    // Update is called once per frame
    void Update()   
    {
        rb.velocity = moveInput * playerSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!menuController.isInDialouge)
        {
            animator.SetBool("isWalking", true);

            if (context.canceled)
            {
                animator.SetBool("isWalking", false);
                animator.SetFloat("InputX", moveInput.x);
                animator.SetFloat("InputY", moveInput.y);
            }

            moveInput = context.ReadValue<Vector2>();
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
        }

    }
}

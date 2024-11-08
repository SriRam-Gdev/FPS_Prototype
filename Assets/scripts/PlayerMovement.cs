using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;

    [SerializeField] private float moveSpeed = 5f;

    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.deltaTime;
    }
}

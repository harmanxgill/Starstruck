using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private CharacterController controller;
    private float verticalSpeed;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        float x = 0f;
        float z = 0f;

        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            x -= 1f;

        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            x += 1f;

        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
            z += 1f;

        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
            z -= 1f;

        Vector3 direction = new Vector3(x, 0f, z).normalized;

        if (controller.isGrounded && verticalSpeed < 0f)
            verticalSpeed = -2f;

        verticalSpeed += Physics.gravity.y * Time.deltaTime;

        Vector3 velocity = direction * speed;
        velocity.y = verticalSpeed;

        controller.Move(velocity * Time.deltaTime);
    }
}
using System;
using UnityEngine.InputSystem;

public class PlayerInputListener
{
    private InputActionReference moveInput;

    private InputActionReference jumpInput;

    public event Action JumpedButtonPressed;

    public float GetMoveInputValue
    {
        get
        {
            return moveInput.action.ReadValue<float>();
        }
    }

    public void Initialize(InputActionReference moveInput, InputActionReference jumpInput)
    {
        this.moveInput = moveInput;
        this.jumpInput = jumpInput;

        jumpInput.action.started += OnJumpedButtonPressed;
    }

    public void Deinitialize()
    {
        jumpInput.action.started -= OnJumpedButtonPressed;
    }

    private void OnJumpedButtonPressed(InputAction.CallbackContext callbackContext)
    {
        JumpedButtonPressed?.Invoke();
    }
}
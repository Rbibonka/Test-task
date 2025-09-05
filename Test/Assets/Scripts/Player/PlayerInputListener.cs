using System;
using UnityEngine.InputSystem;

public class PlayerInputListener
{
    private InputActionReference moveInput;
    private InputActionReference shotInput;
    private InputActionReference changeWeaponInput;

    public event Action JumpButtonPressed;
    public event Action ShootButtonPressed;
    public event Action ChangeWeaponButtonPressed;

    public UnityEngine.Vector2 GetMoveInputValue
    {
        get
        {
            return moveInput.action.ReadValue<UnityEngine.Vector2>();
        }
    }

    public void Initialize(InputActionReference moveInput, InputActionReference shotInput, InputActionReference changeWeaponInput)
    {
        this.moveInput = moveInput;
        this.shotInput = shotInput;
        this.changeWeaponInput = changeWeaponInput;

        this.shotInput.action.started += OnShootButtonPressed;
        this.changeWeaponInput.action.started += OnChangeButtonButtonPressed;
    }

    public void Deinitialize()
    {
        shotInput.action.started -= OnShootButtonPressed;
        changeWeaponInput.action.started -= OnChangeButtonButtonPressed;
    }

    private void OnShootButtonPressed(InputAction.CallbackContext callbackContext)
    {
        ShootButtonPressed?.Invoke();
    }

    private void OnChangeButtonButtonPressed(InputAction.CallbackContext callbackContext)
    {
        ChangeWeaponButtonPressed?.Invoke();
    }
}
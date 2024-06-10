using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu (menuName = "SO/Input/InputReader", fileName = "InputData")]
public class InputReaderSO : ScriptableObject, Controls.IPlayerActions
{
    public float DirectionX;
    public Action Jump;
    public Action SmashStart;
    public Action SmashEnd;
    public Action Esc;

    private Controls _controls;

    private void OnEnable() 
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
        }
        _controls.Enable();
    }

    public void OnPlayerX(InputAction.CallbackContext context)
    {
        DirectionX = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (GameManager.Instance)
        {
            if (!GameManager.Instance.CanMove)
                return;
        }
        
        if (context.started)
            Jump?.Invoke();
    }

    public void OnSmash(InputAction.CallbackContext context)
    {
        int value = (int)context.ReadValue<float>();

        if (value == 1)
        {
            if (context.started)
                SmashStart?.Invoke();
        }
        else 
            SmashEnd?.Invoke();
    }

    public void OnEsc(InputAction.CallbackContext context)
    {
        Esc?.Invoke();
    }
}

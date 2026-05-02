using UnityEngine;
using UnityEngine.InputSystem;

public class TapGame : MonoBehaviour
{
    [SerializeField]
    private InputAction _pointerClickAction;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Transform _tapEffect;

    void OnStarted(InputAction.CallbackContext context)
    {
        var tapEffectInstance = Instantiate(_tapEffect, transform);
        if(context.control.device is Pointer pointer)
        {
            var screenPos = pointer.position.ReadValue();
            var worldPos = _camera.ScreenToWorldPoint(screenPos);
            tapEffectInstance.position = new Vector3(worldPos.x, worldPos.y, 0);
        }
    }

    void OnEnable()
    {
        if(_pointerClickAction == null) return;

        _pointerClickAction.Enable();
        _pointerClickAction.started += OnStarted;
    }
    
    void OnDisable()
    {
        if(_pointerClickAction == null) return;

        _pointerClickAction.Disable();
        _pointerClickAction.started -= OnStarted;
    }
}

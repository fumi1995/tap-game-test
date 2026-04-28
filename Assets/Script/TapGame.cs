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

    void OnPreformed(InputAction.CallbackContext context)
    {
        var tapEffectInstance = Instantiate(_tapEffect, transform);
        var screenPos = context.ReadValue<Vector2>();
        var worldPos = _camera.ScreenToWorldPoint(screenPos);
        tapEffectInstance.position = new Vector3(worldPos.x, worldPos.y, 0);
    }

    void OnEnable()
    {
        if(_pointerClickAction == null) return;

        _pointerClickAction.Enable();
        _pointerClickAction.performed += OnPreformed;
    }
    
    void OnDisable()
    {
        if(_pointerClickAction == null) return;

        _pointerClickAction.Disable();
        _pointerClickAction.performed -= OnPreformed;
    }
}

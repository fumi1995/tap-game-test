using UnityEngine;
using UnityEngine.InputSystem;

public class TapGame : MonoBehaviour
{
    [SerializeField]
    private InputAction _pointerClickAction;
    [SerializeField]
    private InputAction _pointerPressAction;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Transform _tapEffect;
    [SerializeField]
    private Transform _pressEffect;

    void OnTap(InputAction.CallbackContext context)
    {
        if(Pointer.current == null) return;

        var tapEffectInstance = Instantiate(_tapEffect, transform);

        var screenPos = Pointer.current.position.ReadValue();
        var worldPos = _camera.ScreenToWorldPoint(screenPos);
        tapEffectInstance.position = new Vector3(worldPos.x, worldPos.y, 0);

        Debug.Log(screenPos);
    }

    void OnPressPerformed(InputAction.CallbackContext context)
    {
        if(Pointer.current == null) return;

        _pressEffect.gameObject.SetActive(true);
    }

    void OnPressCanceled(InputAction.CallbackContext context)
    {
        _pressEffect.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        _pointerClickAction.started += OnTap;
        _pointerClickAction.Enable();

        _pointerPressAction.performed += OnPressPerformed;
        _pointerPressAction.canceled += OnPressCanceled;
        _pointerPressAction.Enable();
    }
    
    void OnDisable()
    {
        _pointerClickAction.Disable();
        _pointerClickAction.started -= OnTap;

        _pointerPressAction.Disable();
        _pointerPressAction.performed += OnPressPerformed;
        _pointerPressAction.canceled += OnPressCanceled;
    }

    void Update()
    {
        if(Pointer.current == null || _pressEffect.gameObject.activeInHierarchy is false) return;

        var screenPos = Pointer.current.position.ReadValue();
        var worldPos = _camera.ScreenToWorldPoint(screenPos);
        _pressEffect.position = new Vector3(worldPos.x, worldPos.y, 0);
    }
}

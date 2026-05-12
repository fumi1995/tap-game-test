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
    private ParticleSystem _tapEffect;
    [SerializeField]
    private AudioSource _tapAudioSource;
    [SerializeField]
    private ParticleSystem _pressEffect;
    [SerializeField]
    private CharacterManager _characterManager;
    [SerializeField]
    private ScoreText _scoreText;

    private int _score;

    void OnTap(InputAction.CallbackContext context)
    {
        if(Pointer.current == null) return;

        var tapEffectInstance = Instantiate(_tapEffect, transform);

        var screenPos = Pointer.current.position.ReadValue();
        var worldPos = _camera.ScreenToWorldPoint(screenPos);
        tapEffectInstance.transform.position = new Vector3(worldPos.x, worldPos.y, 0);
    }

    void OnPressPerformed(InputAction.CallbackContext context)
    {
        if(Pointer.current == null) return;

        _pressEffect.Play();
        _tapAudioSource.Play();
    }

    void OnPressCanceled(InputAction.CallbackContext context)
    {
        _pressEffect.Stop();
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

    void Start()
    {
        _scoreText.UpdateScore(_score);
        _characterManager.Initialize(point => {
                _score+=point;
                _scoreText.UpdateScore(_score);
            });
    }

    void Update()
    {
        if(Pointer.current == null || _pressEffect.gameObject.activeInHierarchy is false) return;

        var screenPos = Pointer.current.position.ReadValue();
        var worldPos = _camera.ScreenToWorldPoint(screenPos);
        _pressEffect.transform.position = new Vector3(worldPos.x, worldPos.y, 0);
    }
}

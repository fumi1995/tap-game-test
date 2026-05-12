using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterBehaviour : MonoBehaviour
{
    public enum SpawnArea
    {
        InsideScreen,
        OutsideScreen
    }

    [SerializeField]
    private int _point;
    [SerializeField, Range(0f, 30f)]
    private float _lifeTime = 10f;
    [SerializeField]
    private SpawnArea _spawnArea;
    [SerializeField]
    private Vector4 _padding;

    private Animator _animator;
    private float _lifeTimeTimer;
    private bool _isDied;
    private Action<int> _defeatAction;

    public void Initialize(Action<int> defeatAction)
    {
        _defeatAction = defeatAction;
    }

    public Vector2 CalculateSpawnPos()
    {
        float randomX = Random.Range(0f, 1f);
        float randomY = Random.Range(0f, 1f);

        return new Vector2(randomX, randomY);
    }

    public void Act()
    {
        StartCoroutine("ActAsync");
        _isDied = true;
    }


    void Awake()
    {
        _animator = GetComponent<Animator>();
        _lifeTimeTimer = _lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDied) return;

        _lifeTimeTimer -= Time.deltaTime;

        if(_lifeTimeTimer <= 0)
        {
            StartCoroutine("DestoryAsync");
            _isDied = true;
        }
    }

    IEnumerator DestoryAsync()
    {
        _animator.Play("out");

        yield return null;
        yield return new WaitForAnimation(_animator, "out");

        Destroy(gameObject);
    }

    IEnumerator ActAsync()
    {
        _animator.Play("act");

        yield return null;
        yield return new WaitForAnimation(_animator, "act");

        Destroy(gameObject);
        _defeatAction?.Invoke(_point);
    }
}

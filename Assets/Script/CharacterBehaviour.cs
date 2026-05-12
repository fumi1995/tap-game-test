using System.Collections;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public enum SpawnArea
    {
        InsideScreen,
        OutsideScreen
    }

    [SerializeField, Range(0f, 30f)]
    private float _lifeTime = 10f;
    [SerializeField]
    private SpawnArea _spawnArea;
    [SerializeField]
    private Vector4 _padding;

    private Animator _animator;
    private float _lifeTimeTimer;
    private bool _isDied;

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

        var layerNum = 0;
		var currentAnimatorState = _animator.GetCurrentAnimatorStateInfo(layerNum);

		while(currentAnimatorState.normalizedTime < 1)
        {
            yield return null;
        }

        Destroy(gameObject);
    }

    IEnumerator ActAsync()
    {
        _animator.Play("act");

        yield return null;

        var layerNum = 0;
		var currentAnimatorState = _animator.GetCurrentAnimatorStateInfo(layerNum);

		while(currentAnimatorState.IsName("act") && currentAnimatorState.normalizedTime < 1)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}

using System.Collections;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField, Range(0f, 30f)]
    private float _lifeTime = 10f;

    private Animator _animator;
    private float _lifeTimeTimer;
    private bool _isDied;


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
}

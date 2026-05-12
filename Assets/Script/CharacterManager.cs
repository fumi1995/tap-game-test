using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private CharacterBehaviour[] _characters;
    [SerializeField, Range(0f, 30f)]
    private float _spawnInterval = 10f;
    [SerializeField, Range(0f, 30f)]
    private float _spawnIntervalRandamize = 5f;

    private float _spawnTimer;
    private Action<int> _defeatAction;

    public void Initialize(Action<int> defeatAction)
    {
        _defeatAction = defeatAction;
    }

    void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if(_spawnTimer <= 0)
        {
            var choicedCharacterIdx = Random.Range(0, _characters.Length - 1);
            var choicedCharacter = _characters[choicedCharacterIdx];

            float randomX = Random.Range(0f, 1f);
            float randomY = Random.Range(0f, 1f);

            float zDistance = 10f;

            Vector3 screenPos = new Vector3(randomX, randomY, zDistance);

            Vector3 worldPos = Camera.main.ViewportToWorldPoint(screenPos);

            var instance = Instantiate(choicedCharacter, worldPos, Quaternion.identity);
            instance.Initialize(_defeatAction);
            
            _spawnTimer = _spawnInterval + Random.Range(0f, _spawnIntervalRandamize);
        }
    }
}

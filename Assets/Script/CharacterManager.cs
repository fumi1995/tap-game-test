using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private CharacterBehaviour[] _characters;
    [SerializeField, Range(0f, 30f)]
    private float _spawnInterval = 10f;
    [SerializeField, Range(0f, 30f)]
    private float _spawnIntervalRandamize = 5f;

    private float _spawnTimer;

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

            Instantiate(choicedCharacter, worldPos, Quaternion.identity);
            
            _spawnTimer = _spawnInterval + Random.Range(0f, _spawnIntervalRandamize);
        }
    }
}

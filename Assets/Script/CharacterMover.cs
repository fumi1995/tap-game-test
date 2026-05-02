using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)]
    private float _speed;

    void Awake()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * _speed * Time.deltaTime;
    }
}

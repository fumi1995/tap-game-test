using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    public void UpdateScore(int score)
    {
        _text.text = score.ToString();
    }
}

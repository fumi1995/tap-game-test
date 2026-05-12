using UnityEngine;

// なるべく基準解像度の見た目を維持する
[ExecuteInEditMode]
public class CameraSizeScaler : MonoBehaviour
{
    [SerializeField]
    private Vector2 _baseResolution = new Vector2(1920,1080);

    private Camera _camera;
    private Camera Camera => _camera != null ? _camera : _camera = GetComponent<Camera>();

    // Update is called once per frame
    void Update()
    {
        var screenRatio = (float)Screen.height / Screen.width;
        var baseRatio = _baseResolution.y / _baseResolution.x;
        
        // 縦長なら横に画角を合わせる
        if(screenRatio < baseRatio)
        {
            // 横合わせでスケーリングする
            var scaleFactor = _baseResolution.x / Screen.width;
            var scaledScreenHeight = Screen.height * scaleFactor;
            Camera.orthographicSize = scaledScreenHeight * 0.5f / 100f;
        }
        // 横長なら縦に画角を合わせる.デフォルトの挙動なので何もしない
        else
        {
            Camera.orthographicSize = _baseResolution.y * 0.5f / 100f;
        }
    }
}

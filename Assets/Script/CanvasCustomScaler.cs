using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CanvasCustomScaler : MonoBehaviour
{
    private CanvasScaler _canvasScaler;
    private CanvasScaler CanvasScaler => _canvasScaler != null ? _canvasScaler : _canvasScaler = GetComponent<CanvasScaler>();

    // Update is called once per frame
    void Update()
    {
        var screenRatio = (float)Screen.height / Screen.width;
        var baseRatio = CanvasScaler.referenceResolution.y / CanvasScaler.referenceResolution.x;
        
        // 縦長なら横に画角を合わせる
        if(screenRatio < baseRatio)
        {
            CanvasScaler.matchWidthOrHeight = 0f;
        }
        // 横長なら縦に画角を合わせる
        else
        {
            CanvasScaler.matchWidthOrHeight = 1f;
        }
    }
}

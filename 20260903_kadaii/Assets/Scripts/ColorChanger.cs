using System.Runtime.InteropServices;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // JavaScript側の関数を呼び出す
    // unityでjsを使う準備
    #if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void SendMessageToJS(string message);
    #endif

    private Renderer sphereRenderer;

    void Start()
    {
        // Renderer取得
        sphereRenderer = GetComponent<Renderer>();

        // WebGLのみ実行
        #if UNITY_WEBGL && !UNITY_EDITOR
        SendMessageToJS("Unity準備完了");
        #endif
    }

    // JavaScriptから呼び出される関数
    public void ChangeColor(string colorName)
    {
        if (sphereRenderer == null)
        {
            Debug.LogError("Rendererが見つかりません");
            return;
        }

        switch (colorName.ToLower())
        {
            case "red":
                sphereRenderer.material.color = Color.red;
                break;

            case "green":
                sphereRenderer.material.color = Color.green;
                break;

            case "blue":
                sphereRenderer.material.color = Color.blue;
                break;

            default:
                Debug.LogWarning($"未対応の色: {colorName}");
                break;
        }

        Debug.Log($"色変更: {colorName}");

        // JSへ通知
        #if UNITY_WEBGL && !UNITY_EDITOR
        SendMessageToJS($"Color Changed : {colorName}");
        #endif
    }

    // Sphereがクリックされたとき
    void OnMouseDown()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        SendMessageToJS("Sphereがクリックされた");
        #endif
    }

    public void ChangeSize(string sizeValue)
    {
        float size = float.Parse(sizeValue);

        transform.localScale = new Vector3(size, size, size);
    }

    public void ChangeColorByPicker(string hexColor)
    {
        // カラーコードを成形
        // #を削除
        hexColor = hexColor.Replace("#", "");

        // 16進数の文字列を数値に変換
        int r = int.Parse(hexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        int g = int.Parse(hexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        int b = int.Parse(hexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        // 0-255, 0-1の範囲に変換
        Color newColor = new Color(r / 255f, g / 255f, b / 255f);

        // 色をSphereにあてる
        sphereRenderer.material.color = newColor;
    }
}
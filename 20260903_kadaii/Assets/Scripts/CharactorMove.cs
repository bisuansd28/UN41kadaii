using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorMove : MonoBehaviour
{
    // 移動速度
    public float moveSpeed = 5f;
    private Vector2 touchDirection = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Wが押されている間は前方に移動
        if (Input.GetKey(KeyCode.W))
        {
            // Vector3.forwardで前方に移動
            // deltaTime -> フレームに依存しなくなりマシンスペックに合わせた速度になる
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        Vector3 touchMove = new Vector3(
            touchDirection.x,
            0,
            touchDirection.y
        );

        // XとZが-4～4を超えないようにする
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -4f, 4f);
        pos.z = Mathf.Clamp(pos.z, -4f, 4f);

        transform.position = pos;
    }

    // JavaScriptから呼び出す
    public void SetTouchDirection(string direction)
    {
        string[] values = direction.Split(',');

        float x = float.Parse(values[0]);
        float y = float.Parse(values[1]);

        touchDirection = new Vector2(x, y);
    }

    // タッチ終了
    public void StopTouch()
    {
        touchDirection = Vector2.zero;
    }
}

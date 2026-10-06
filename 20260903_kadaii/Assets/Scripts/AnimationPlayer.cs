using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    // アニメーションファイルを格納する変数
    // 待機モーション
   public AnimationClip idleAnimationClip;
   // 走りモーション
   public AnimationClip runAnimation;
   // ジャンプモーション
   public AnimationClip jumpAnimation;

   // ジャンプ中か管理するフラグ
    public bool isJumping = false;

   // アニメーションコンポーネントを格納する変数
   private Animation animationComponent;

   public Vector2 touchDirection = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        animationComponent = GetComponent<Animation>();
        if(animationComponent == null)
        {
            // アニメーションコンポーネントがなければ新しくつける
            animationComponent = gameObject.AddComponent<Animation>();
        }

        // アニメーションを登録
        animationComponent.AddClip(idleAnimationClip, "Idle");
        animationComponent.AddClip(runAnimation, "Run");
        animationComponent.AddClip(jumpAnimation, "Jump");
    }

    // Update is called once per frame
    void Update()
    {
        // ジャンプキーが押されたとき&ジャンプ状態でないとき
        if(Input.GetKey(KeyCode.Space) && !isJumping)
        {
            // ジャンプ状態
            isJumping = true;
            // ジャンプモーション再生
            animationComponent.Play("Jump");
        }

        // ジャンプ終了した場合
        if(isJumping && !animationComponent.IsPlaying("Jump"))
        {
            isJumping = false;
        }

        if (!isJumping)
        {
            // WASDで走る
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || touchDirection != Vector2.zero)
            {
                animationComponent.Play("Run");
            } else
            {
                // アニメーションを再生
                animationComponent.Play("Idle");
            }
        }
    }
}

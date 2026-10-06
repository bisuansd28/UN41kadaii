using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    // 待機モーション
    public AnimationClip idleAnimationClip;

    // 走りモーション
    public AnimationClip runAnimation;

    // ジャンプモーション
    public AnimationClip jumpAnimation;

    // ジャンプ中か
    public bool isJumping = false;

    // Animationコンポーネント
    private Animation animationComponent;

    // 移動スクリプト
    private CharactorMove charactorMove;

    void Start()
    {
        animationComponent = GetComponent<Animation>();

        if (animationComponent == null)
        {
            animationComponent = gameObject.AddComponent<Animation>();
        }

        charactorMove = GetComponent<CharactorMove>();

        // アニメーション登録
        animationComponent.AddClip(idleAnimationClip, "Idle");
        animationComponent.AddClip(runAnimation, "Run");
        animationComponent.AddClip(jumpAnimation, "Jump");

        animationComponent.Play("Idle");
    }

    void Update()
    {
        // ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            animationComponent.Play("Jump");
        }

        // ジャンプ終了
        if (isJumping && !animationComponent.IsPlaying("Jump"))
        {
            isJumping = false;
        }

        if (!isJumping)
        {
            bool isMoving =
                Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D);

            // タッチ移動判定
            if (charactorMove != null)
            {
                isMoving = isMoving || charactorMove.touchDirection != Vector2.zero;
            }

            if (isMoving)
            {
                if (!animationComponent.IsPlaying("Run"))
                {
                    animationComponent.Play("Run");
                }
            }
            else
            {
                if (!animationComponent.IsPlaying("Idle"))
                {
                    animationComponent.Play("Idle");
                }
            }
        }
    }
}
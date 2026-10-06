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

    // 前フレームの位置
    private Vector3 lastPosition;

    void Start()
    {
        animationComponent = GetComponent<Animation>();

        if (animationComponent == null)
        {
            animationComponent = gameObject.AddComponent<Animation>();
        }

        // アニメーション登録
        animationComponent.AddClip(idleAnimationClip, "Idle");
        animationComponent.AddClip(runAnimation, "Run");
        animationComponent.AddClip(jumpAnimation, "Jump");

        // 初期位置保存
        lastPosition = transform.position;

        // 待機モーション再生
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

        // ジャンプ中でなければ移動判定
        if (!isJumping)
        {
            float moveDistance =
                Vector3.Distance(transform.position, lastPosition);

            bool isMoving = moveDistance > 0.001f;

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

        // 現在位置を保存
        lastPosition = transform.position;
    }
}
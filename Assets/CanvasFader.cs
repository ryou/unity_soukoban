using System;
using UnityEngine;

public class CanvasFader : MonoBehaviour
{

    private enum State
    {
        Idle,
        Fading,
    }

    public float duration = 1.0f;

    private CanvasGroup canvasGroup;
    private event Action onFadeComplete;
    private State currentState = State.Idle;

    private float startAlpha;
    private float targetAlpha;

    private void Awake()
    {
        var tmpCanvasGroup = this.GetComponent<CanvasGroup>();
        if (tmpCanvasGroup == null)
        {
            tmpCanvasGroup = this.gameObject.AddComponent<CanvasGroup>();
        }
        this.canvasGroup = tmpCanvasGroup;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsFading()) return;

        var fadeProgressPerSecond = (this.targetAlpha - this.startAlpha) / this.duration;
        var fadeProgress = fadeProgressPerSecond * Time.deltaTime;

        this.canvasGroup.alpha += fadeProgress;

        // フェード処理完了
        if (this.canvasGroup.alpha >= 1.0f || this.canvasGroup.alpha <= 0)
        {
            if (this.canvasGroup.alpha > 1.0f)
            {
                this.canvasGroup.alpha = 1.0f;
            }
            else if (this.canvasGroup.alpha < 0)
            {
                this.canvasGroup.alpha = 0;
            }

            this.currentState = State.Idle;
            this.onFadeComplete();
        }
    }

    void Fade(float inStartAlpha, float inTargetAlpha, Action inOnFadeComplete = null)
    {
        if (IsFading()) return;

        this.canvasGroup.alpha = this.startAlpha = inStartAlpha;
        this.targetAlpha = inTargetAlpha;
        this.onFadeComplete = inOnFadeComplete;
        this.currentState = State.Fading;
    }

    public void FadeIn(Action inOnFadeComplete = null)
    {
        Fade(0, 1.0f, inOnFadeComplete);
    }

    public void FadeOut(Action inOnFadeComplete = null)
    {
        Fade(1.0f, 0, inOnFadeComplete);
    }

    public bool IsFading()
    {
        return this.currentState == State.Fading;
    }
}
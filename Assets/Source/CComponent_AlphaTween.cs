using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public enum EAlphaTweenType
{
    FADEIN,
    FADEOUT
};

[RequireComponent(typeof(CanvasGroup))]
public class CComponent_AlphaTween : MonoBehaviour
{
    // private:
    [SerializeField] private bool m_TweenOnEnable;
    [SerializeField] private float m_TweenWaitTimeInSeconds;
    [SerializeField] private EAlphaTweenType m_TweenType;
    [SerializeField] private float m_StartValue = 0;
    [SerializeField] private float m_EndValue = 1;
    [SerializeField] private float m_FadeInSpeedInSeconds;
    [SerializeField] private float m_FadeOutSpeedInSeconds;
    private CanvasGroup m_CanvasGroup;
    private Action m_TweenAction;

    private float m_CurrentWaitTime;
    // public:
    public event Action OnFadeInComplete = delegate { };
    public event Action OnFadeOutComplete = delegate { };

    // private:
    private void OnEnable()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();

        OnFadeInComplete += () => { this.enabled = false; };
        OnFadeOutComplete += () => { this.enabled = false; };

        if (m_TweenOnEnable != true) return;
        m_TweenAction = (m_TweenType == EAlphaTweenType.FADEIN) ? TweenIn : TweenOut;
    }
    private void Update()
    {
        if (m_TweenOnEnable != true) return;

        if (m_CurrentWaitTime < m_TweenWaitTimeInSeconds) m_CurrentWaitTime += Time.deltaTime;
        else
        {
            m_TweenAction();
            m_TweenOnEnable = false;
        }


    }
    private void TweenUpdate(float _Value) => m_CanvasGroup.alpha = _Value;
    // public:
    public void TweenIn() => LeanTween.value(gameObject, m_StartValue, m_EndValue, m_FadeInSpeedInSeconds).setOnUpdate(TweenUpdate).setOnComplete(OnFadeInComplete);
    public void TweenOut() => LeanTween.value(gameObject, m_EndValue, m_StartValue, m_FadeOutSpeedInSeconds).setOnUpdate(TweenUpdate).setOnComplete(OnFadeOutComplete);


}
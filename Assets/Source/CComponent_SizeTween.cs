using UnityEngine;
using System;

public class CComponent_SizeTween : MonoBehaviour
{
    // private:
    [SerializeField] private bool m_TweenOnEnable = false;
    [SerializeField] private float FirstSize;
    [SerializeField] private float LastSize;
    [SerializeField] private float TransitionTimeInSeconds;
    
    // public:
    public event Action OnComplete = delegate { }; 
    // private:
    private void OnEnable()
    {
        if(m_TweenOnEnable) Tween();
    }
    private void TweenUpdate(float _Value)
    {
        transform.localScale = new Vector3(_Value, _Value, _Value);
    }
    // public:
    public void Tween()
    {
        LeanTween.value(gameObject, FirstSize, LastSize, TransitionTimeInSeconds).setEaseLinear().setOnComplete(OnComplete).setOnUpdate(TweenUpdate);
    }

    public void UndoTween()
    {
        LeanTween.value(gameObject, LastSize, FirstSize, TransitionTimeInSeconds).setEaseLinear().setOnComplete(OnComplete).setOnUpdate(TweenUpdate);
    }

}
using System;
using UnityEngine;
public class CComponent_PositionTween : MonoBehaviour
{
    // private:
    [SerializeField] private bool TweenOnEnable = false; 
    [SerializeField] private Transform Destination;
    [SerializeField] private Vector3 Direction;
    [SerializeField] private float Distance;
    [SerializeField] private float TransitionTimeInSeconds;

    // public:
    public event Action OnComplete = delegate { };

    // private:
    private void OnEnable()
    {
        if(TweenOnEnable) Tween();
    }
    private void TweenUpdate(Vector3 _Value)
    {
        transform.position = _Value;
    }
    // public:
    public void Tween()
    {
        Vector3 target = (Destination != null) ? Destination.position : Direction * Distance;
        LeanTween.value(gameObject, transform.position, target, TransitionTimeInSeconds).setEaseLinear().setOnComplete(OnComplete).setOnUpdateVector3(TweenUpdate);
    }
}

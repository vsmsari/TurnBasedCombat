using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CController_Animator : MonoBehaviour
{
    private Animator m_Animator;
    
    // These are for other scripts to listen.
    public event Action OnAttackAnimation_InflictDamage;
    public event Action DamageAnimation_PeakFrame;


    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }


    public void PlayAttackAnimation() => m_Animator.SetTrigger("OnAttack");
    public void PlayDamageAnimation() => m_Animator.SetTrigger("OnDamage");


    // ANIMATION KEY FRAME EVENTS
    public void AnimEvent_AttackAnimation_InflictDamage() => OnAttackAnimation_InflictDamage?.Invoke();
    public void AnimEvent_DamageAnimation_PeakFrame() => DamageAnimation_PeakFrame?.Invoke();
}
using System;
using UnityEngine;
using UnityEngine.UI;

public class CComponent_ActorHealth : MonoBehaviour
{
    // private:
    [SerializeField] private int m_MaximumHealth;
    [SerializeField] private float m_DestructionSpeedInSeconds = 0.5f;
    [Header("Healthbar")]
    [SerializeField] private Slider m_HealthBar;
    [SerializeField] private float m_HealthTransitionSpeedInSeconds;
    [SerializeField] private float m_HealthFadeInSpeedInSeconds;
    [SerializeField] private float m_HealthFadeOutSpeedInSeconds;
    
    private int m_LastHealth;
    private int m_CurrentHealth;
    
    // public:
    public event Action OnTakeDamage = delegate{};
    public event Action OnDie = delegate{};
    private void OnEnable()
    {
        OnDie += () => { Destroy(this.gameObject, m_DestructionSpeedInSeconds); };
    }
    public void TakeDamage(int _Damage)
    {
        OnTakeDamage?.Invoke();

        CManager_FloatingTexts.Instance.Spawn(transform.position, _Damage.ToString());
        
        m_LastHealth = m_CurrentHealth;
        m_CurrentHealth -= _Damage;
        
        LeanTween.value(m_HealthBar.gameObject, m_LastHealth, m_CurrentHealth, m_HealthTransitionSpeedInSeconds)
        .setOnUpdate( (_Value) => { m_HealthBar.value = _Value; });
        
        if (m_CurrentHealth <= 0) OnDie?.Invoke();
    }
    public void SetMaximumHealth(int _Health)
    {
        m_MaximumHealth = _Health;
        m_CurrentHealth = _Health;
        m_HealthBar.maxValue = _Health;
        m_HealthBar.value = _Health;
    }
    public void ShowHealthbar()
    {
        if(m_HealthBar != null)  m_HealthBar.gameObject.SetActive(true);
    }

    public void HideHealthBar()
    {
        if(m_HealthBar != null)  m_HealthBar.gameObject.SetActive(false);
    }
    
}

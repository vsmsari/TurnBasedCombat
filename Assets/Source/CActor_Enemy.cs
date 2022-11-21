using UnityEngine;
using Random = UnityEngine.Random;

public class CActor_Enemy : CCombatActor
{
    // public:
    private int m_MinimumAttackPower;
    private int m_MaximumAttackPower;

    // private: --
    private void Awake()
    {
        m_HealthComponent = GetComponent<CComponent_ActorHealth>();
    }
    private void OnEnable()
    {
        AnimatorController.OnAttackAnimation_InflictDamage += () => 
        {
            m_TargetActor.HealthComponent.TakeDamage(AttackPower());
        } ;
        m_HealthComponent.OnTakeDamage += () => { AnimatorController.PlayDamageAnimation(); };
        
        m_HealthComponent.OnDie += () => { CManager_CombatStates.Instance.ToResultState(); } ;
    }
    // public: 
    public void SetAttackPowerRange(int _MinAttackPower, int _MaxAttackPower) 
    {
        m_MinimumAttackPower = _MinAttackPower;
        m_MaximumAttackPower = _MaxAttackPower;
    } 
    public int AttackPower() => Random.Range(m_MinimumAttackPower, m_MaximumAttackPower);

}

using UnityEngine;

public class CCombatState_Cooldown : CSMState_Base
{
    [SerializeField] private float m_CooldownTime;
    private float m_CooldownCurrent;
    public override void BeginState()
    {
        m_CooldownCurrent = 0;
        m_Manager.HeroManager.ShowHealthbars();
    }
    public override void UpdateState()
    {
        if (m_CooldownCurrent < m_CooldownTime) m_CooldownCurrent += Time.deltaTime;
        else  m_Manager.SetState(m_TargetState);
    }
}
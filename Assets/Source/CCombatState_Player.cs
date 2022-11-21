using System;
using UnityEngine;


[Serializable]
// public class CCommandSet_HeroAttack : CCommandSet_Base { }

public class CCombatState_Player : CSMState_Base
{
    // private:
    [SerializeField] private Transform PlayerAttackLocation;
    [SerializeField] private CCommandSet_Base m_AttackCommandSet;     


    private CActor_Hero m_SelectedHero;
    private CComponent_PlayerInput m_PlayerInput;
    private bool m_DetailsOn;
    private void Awake()
    {
        m_PlayerInput = GetComponent<CComponent_PlayerInput>();
        m_PlayerInput.OnTouchPressed += InputCallback_SelectHero;
        m_PlayerInput.OnHoldRelease += InputCallback_ShowHeroDetails;
        m_PlayerInput.OnTouchReleased += InpuCallback_StartHeroAttack;
    }
    private void InputCallback_SelectHero() // OnTouchPressed Callback
    {
        if (m_DetailsOn)
        {
            m_DetailsOn = false;
            m_SelectedHero.Shrink();
            m_SelectedHero.HideDetailsPanel();  
        }

        GameObject result = m_PlayerInput.RaycastResult();
        if (result != null)
        {
            m_SelectedHero = result.GetComponent<CActor_Hero>();
            m_SelectedHero.Grow();
        }
        else m_SelectedHero = null;
    }
    private void InputCallback_ShowHeroDetails() // OnTouchHold Callback
    {
        if (m_SelectedHero == null || m_DetailsOn) return;
        m_SelectedHero.ShowDetailsPanel();
        m_DetailsOn = true;
    }
    private void InpuCallback_StartHeroAttack() // OnTouchReleased Callback
    {
        if (m_SelectedHero == null) return;
        m_SelectedHero.SetTargetActor(m_Manager.EnemyManager.GetEnemy());
        m_SelectedHero.Shrink();
        SendActionCommands();
        m_Manager.Deactivate();
    }
    // public: 
    public override void BeginState()
    {
        m_SelectedHero = null;
        m_Manager.EnemyManager.ShowHealthbar();
    }

    public override void UpdateState() => m_PlayerInput.UpdateInput();
    
    // Finalizing the premade scriptable object data with their class types.  
    private void SendActionCommands()
    {
        for (int index = 0; index < m_AttackCommandSet.CommandList.Count; index++)
        {
            CCommand_Base command = m_AttackCommandSet.CommandList[index];
            command.SetOwner(m_SelectedHero); // Each command has to have some sort of "Owner" as CActor.

            if (command is CActorCommand_Move moveCommand)
            {
                moveCommand.SetDestination(PlayerAttackLocation.position);
                moveCommand.OnCompleteUndo += () =>
                {
                    m_Manager.SetState(m_TargetState); 
                };
            }
        }
        m_SelectedHero.AddCommandSet(m_AttackCommandSet);
        m_SelectedHero.HealthComponent.HideHealthBar();
    }
}

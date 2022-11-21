using UnityEngine;

public class CCombatState_Enemy : CSMState_Base
{
    public override void BeginState()
    {
        CActor_Hero targetHero = m_Manager.HeroManager.GetRandomSpawnedHero();
        CActor_Enemy enemy = m_Manager.EnemyManager.GetEnemy();

        enemy.SetTargetActor(targetHero);
        enemy.HealthComponent.HideHealthBar();
        enemy.AnimatorController.PlayAttackAnimation();
        
        m_Manager.SetState(m_TargetState);
    }
}

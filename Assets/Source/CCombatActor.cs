public abstract class CCombatActor : CActor 
{
    // protected:
    protected CComponent_ActorHealth m_HealthComponent;
    protected CCombatActor m_TargetActor { get; private set; }

    // public:
    public CCombatActor TargetActor => m_TargetActor;
    public CComponent_ActorHealth HealthComponent => m_HealthComponent;
    public void SetTargetActor(CCombatActor _TargetActor) => m_TargetActor = _TargetActor;
}
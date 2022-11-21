using UnityEngine;
using TMPro;

public class CActor_Hero : CCombatActor, ICommandActor
{
    // private:
    [SerializeField] private CHeroData m_HeroData;
    private CManager_Hero m_Manager;
    private CComponent_ShakeTween m_CameraShakeComponent;
    private CComponent_SizeTween m_SizeTweenComponent;
    private CComponent_CommandBrain m_CommandBrain;
    private CComponent_Hero_InGameDetails m_InGameDetailsComponent;
    
    // public:
    public CHeroData HeroData => m_HeroData;
    
    // private:
    private void Awake()
    {
        m_Manager = transform.GetComponentInParent<CManager_Hero>();
        m_CameraShakeComponent = Camera.main.GetComponent<CComponent_ShakeTween>();
        m_HealthComponent = GetComponent<CComponent_ActorHealth>();
        m_CommandBrain = GetComponent<CComponent_CommandBrain>();
        m_InGameDetailsComponent = GetComponent<CComponent_Hero_InGameDetails>();
        m_SizeTweenComponent = GetComponent<CComponent_SizeTween>();
    }
    private void OnEnable()
    {
        AnimatorController.OnAttackAnimation_InflictDamage += () =>
        {
            m_TargetActor.HealthComponent.TakeDamage(m_HeroData.AttackPower);
        } ;
        m_HealthComponent.OnTakeDamage += () =>
        {
            AnimatorController.PlayDamageAnimation();
            m_CameraShakeComponent.Tween();
        };
        m_HealthComponent.OnDie += () =>
        {
            m_Manager.RemoveAliveHero(this);
        };
    }
    // public:
    
    // TODO(vsmsari): Decide which one is easier to read.
    // public void AddCommands(bool _UseUndo, params CCommand_Base[] _NewCommands)
    // {
    //     m_CommandBrain.SetUndo(_UseUndo);
    //     m_CommandBrain.ReceiveCommands(_NewCommands);
    // }
    public void AddCommandSet(CCommandSet_Base _CommandSet)
    {
        m_CommandBrain.SetUndo(_CommandSet.UseUndo);
        m_CommandBrain.ReceiveCommands(_CommandSet.CommandList.ToArray());
    }
    public void ShowDetailsPanel() => m_InGameDetailsComponent.ShowHeroDetailsPanel();
    public void HideDetailsPanel() => m_InGameDetailsComponent.HideHeroDetailsPanel();
    public void SetHeroData(CHeroData _Hero)
    {
        m_HeroData = _Hero;
        m_HealthComponent.SetMaximumHealth(m_HeroData.Health);
        m_SpriteRendererComponent.sprite = CManager_Data.Instance.GetSprite(m_HeroData.DataIndex);        
        
        string heroName = m_HeroData.Name;
        string heroAP = m_HeroData.AttackPower.ToString();
        string heroLevel = CManager_Data.Instance.GetHeroLevel(m_HeroData).ToString();
        string heroExp = m_HeroData.Experience.ToString();
        m_InGameDetailsComponent.SetDetails(heroName, heroAP, heroLevel, heroExp);        
    }

    public void Grow() => m_SizeTweenComponent.Tween();
    public void Shrink() => m_SizeTweenComponent.UndoTween();

}

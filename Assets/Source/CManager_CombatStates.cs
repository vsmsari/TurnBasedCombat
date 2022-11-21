using UnityEngine;
 
public class CManager_CombatStates : MonoBehaviour
{
    // private:
    [SerializeField] protected bool m_IsActive = false; // Debug
    [SerializeField] protected CSMState_Base m_EntryState;
    [SerializeField] protected CSMState_Base m_CurrentState;
    [SerializeField] private CManager_Hero m_HeroManager;
    [SerializeField] private CManager_Enemy m_EnemyManager;
    [SerializeField] private CCombatState_Result m_ResultState;

    // public:    
    public CManager_Hero HeroManager => m_HeroManager;
    public CManager_Enemy EnemyManager => m_EnemyManager;

    public static CManager_CombatStates Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    private void Start()
    {
        if (m_IsActive != true) return;
        SetState(m_EntryState);
    }
    private void Update()
    {
        if (m_IsActive != true) return;
        m_CurrentState.UpdateState();
    }
    public void SetState(CSMState_Base _TargetState)
    {
        if(m_CurrentState != null) m_CurrentState.gameObject.SetActive(false);
        if (m_CurrentState != _TargetState)
        {
            m_CurrentState = _TargetState;
            m_CurrentState.SetManager(this);
            m_CurrentState.gameObject.SetActive(true);
            m_CurrentState.BeginState();
            m_IsActive = true;;
        }
    }
    public void Deactivate() => m_IsActive = false;
    public void ToResultState() => SetState(m_ResultState);
}

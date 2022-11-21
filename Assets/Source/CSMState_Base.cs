using UnityEngine;


// NOTE(vsmsari): Even though I know that enabling and disabling objects is not free, the reason that I use these states as MonoBehaviour is readability.
// For bigger projects I use custom types but I also write a custom inspector so that I can easily check runtime state activation. 
[System.Serializable]
public abstract class CSMState_Base : MonoBehaviour
{
    [SerializeField] protected CSMState_Base m_TargetState;
    protected CManager_CombatStates m_Manager { get; private set; }
    public virtual void BeginState() { }
    public virtual void UpdateState() { }
    public void SetManager(CManager_CombatStates _Manager) => m_Manager = _Manager;
}

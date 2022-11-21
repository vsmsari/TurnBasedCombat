using UnityEngine;

[CreateAssetMenu(fileName = "New Move Command", menuName = "Command System/Commands/Move", order = 0)]
public class CActorCommand_Move : CCommand_Base
{
    // private:
    [Header("Command Specific")]
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_StopDistance;
    private Vector3 m_LastPosition;
    private Vector3 m_FirstPosition;
    // public:
    public override void Execute()
    {
        if (Vector3.Distance(Owner.transform.position, m_LastPosition) > m_StopDistance)
        {
            Vector3 direction = (m_LastPosition - Owner.transform.position).normalized;
            Owner.transform.position += direction * m_Speed * Time.deltaTime;
        }
        else Brain.EndCurrentExecution();
    }
    public override void Undo()
    {
        if (Vector3.Distance(Owner.transform.position, m_FirstPosition) > 0.1f)
        {
            Vector3 direction = (m_FirstPosition - Owner.transform.position).normalized;
            Owner.transform.position += direction * m_Speed * Time.deltaTime;
        }
        else Brain.EndCurrentExecution();
    }
    // Methods for the variables that are not easy? to assign via scriptable object inspector. 
    public void SetDestination(Vector3 _Destination)
    {
        m_FirstPosition = Owner.transform.position;        
        m_LastPosition = _Destination;
    }

}
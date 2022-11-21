using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Command", menuName = "Command System/Commands/Attack", order = 1)]
public class CActorCommand_Attack : CCommand_Base
{
    // public:
    public override void Execute()
    {
        if(Brain.m_IsCommandExecuted != true) Owner.AnimatorController.PlayAttackAnimation();
        Brain.EndCurrentExecution();
    }
}
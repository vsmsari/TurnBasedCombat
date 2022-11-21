using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CComponent_CommandBrain : MonoBehaviour
{
    // private:
    [SerializeField] public bool m_IsCommandExecuted;
    [SerializeField] private List<CCommand_Base> m_CommandList = new List<CCommand_Base>();
    [SerializeField] private List<CCommand_Base> m_UndoList = new List<CCommand_Base>();
    [SerializeField] private int m_CurrentCommandIndex;
    [SerializeField] private bool m_UseUndo;
    private bool m_AllCommandsReceived;
    private float m_CooldownTime = 0;
    private float m_CurrentCooldown = 0;


    private void Update()
    {
        if(m_AllCommandsReceived != true) return;
        if (m_CommandList.Count > 0)
        {
            if (m_IsCommandExecuted != true) m_CommandList[m_CurrentCommandIndex].Execute();
            else UpdateCooldown();
        }
        else if (m_UseUndo && m_UndoList.Count > 0)
        {
            if (m_IsCommandExecuted != true) m_UndoList[m_CurrentCommandIndex].Undo();
            else UpdateCooldown();
        }
    }
    
    private void UpdateCooldown()
    {
        if (m_CurrentCooldown < m_CooldownTime) m_CurrentCooldown += Time.deltaTime;
        else
        {
            if (m_CommandList.Count > 0)
            {
                m_CommandList[m_CurrentCommandIndex].EndCommand(); // Invokes current commands OnComplete action.
                ToNextCommand();
            }
            else if (m_UndoList.Count > 0)
            {
                m_UndoList[m_CurrentCommandIndex].EndUndo(); // Invokes current undos OnComplete action.
                ToNextUndo();
            }
        }
    }
    private void HardReset()
    {
        m_CommandList = new List<CCommand_Base>();
        m_UndoList = new List<CCommand_Base>();
        m_AllCommandsReceived = false;
        m_CurrentCommandIndex = 0;
        m_CurrentCooldown = 0;
        m_CooldownTime = 0;
    }
    // public:
    public void ReceiveCommands(params CCommand_Base[] _NewCommmands)
    {
        HardReset();
        m_IsCommandExecuted = false;
        for (int index = 0; index < _NewCommmands.Length; index++)
        {
            CCommand_Base command = _NewCommmands[index];
            command.SetBrain(this);
            // command.OnCompleteCommand += ToNextCommand; 
            m_CommandList.Add(command);
            if (command.IsUndoable) AddToUndoList(command);
        }
        m_AllCommandsReceived = true;
        m_CooldownTime = m_CommandList[m_CurrentCommandIndex].CooldownTime;
    }
    public void ToNextCommand()
    {
        m_CurrentCooldown = 0;
        m_IsCommandExecuted = false; // Start new execution.
        
        if (m_CurrentCommandIndex < m_CommandList.Count - 1) // Until we are at the last command.
        {
            m_CurrentCommandIndex++; // Get the next index
            m_CooldownTime = m_CommandList[m_CurrentCommandIndex].CooldownTime; // Reassign cooldown time
        }
        else
        {
            if (m_UndoList.Count > 0)
            {
                m_CommandList = new List<CCommand_Base>(); // Reset command list
                m_CurrentCommandIndex = 0; // Reset index for possible undos
                m_CooldownTime = m_UndoList[m_CurrentCommandIndex].CooldownTime; // Reassign cooldown time
            }
        }
    }
    public void ToNextUndo()
    {
        m_CurrentCooldown = 0;
        if (m_CurrentCommandIndex < m_UndoList.Count - 1) // Until we are at the last undo.
        {
            m_CurrentCommandIndex++; // Get the next index
            m_CooldownTime = m_UndoList[m_CurrentCommandIndex].CooldownTime; // Reassign cooldown time
            m_IsCommandExecuted = false; // Start new execution.
        }
        else  HardReset();
    }
    public void AddToUndoList(CCommand_Base _Command)
    {
        _Command.OnCompleteUndo += ToNextUndo;
        if(m_UndoList.Count > 0) m_UndoList.Insert(0, _Command);
        else m_UndoList.Add(_Command);
    }
    public void SetUndo(bool _UseUndo) => m_UseUndo = _UseUndo;
    public void EndCurrentExecution() => m_IsCommandExecuted = true;
}
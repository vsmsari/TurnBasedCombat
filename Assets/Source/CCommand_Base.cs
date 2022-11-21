using System;
using UnityEngine;


// NOTE(vsmsari): I usually prefer pre instantiated static objects(as I did for the finite state machine) over scriptable objects for these kind of command pattern based in game actions.    
// But since this is a small turn based game, the commands are super simple and the "owners" are not getting conflicted as easy as... let's say an rts game?, anyway that's why I'm using scriptable object.
[Serializable]
public class CCommand_Base: ScriptableObject
{
    // private:
    [Header("Essential")]
    [SerializeField] private bool m_Undoable;
    [SerializeField] private float m_CooldownTime;
    
    private CActor m_Owner = null;
    private CComponent_CommandBrain m_Brain;
    
    // protected:
    protected CComponent_CommandBrain Brain => m_Brain;

    // public:
    public CActor Owner => m_Owner;
    public bool IsUndoable => m_Undoable;
    public float CooldownTime => m_CooldownTime;
    
    public event Action OnCompleteCommand = delegate {  };
    public event Action OnCompleteUndo = delegate {  };
    
    // public:
    public virtual void Execute() { }
    public virtual void Undo() { }

    public void EndCommand() => OnCompleteCommand?.Invoke(); 
    public void EndUndo() => OnCompleteUndo?.Invoke();
    public void SetOwner(CActor _Owner) => m_Owner = _Owner;
    public void SetBrain(CComponent_CommandBrain _Brain) => m_Brain = _Brain;
}

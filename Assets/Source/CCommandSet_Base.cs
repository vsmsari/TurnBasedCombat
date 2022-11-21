using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Command Set", menuName = "Command System/Command Set", order = 0)]
public class CCommandSet_Base : ScriptableObject
{
    public bool UseUndo = false;
    public List<CCommand_Base> CommandList;
}
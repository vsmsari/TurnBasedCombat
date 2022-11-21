using UnityEngine;

// TODO(vsmsari): Remake this using only lean tween.
public class CUIPanel_BattleResult : MonoBehaviour
{
    public void LoadSelectionScene() => CManager_Scene.Instance.LoadSelectionMenu();
}
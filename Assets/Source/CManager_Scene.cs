using UnityEngine;
using UnityEngine.SceneManagement;


public class CManager_Scene : MonoBehaviour
{
    public static CManager_Scene Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    public void LoadSelectionMenu() => SceneManager.LoadScene("Scene_SelectionMenu");
    public void LoadBattle() => SceneManager.LoadScene("Scene_Battle");       
}
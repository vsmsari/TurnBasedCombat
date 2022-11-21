using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CManager_BattleUI: MonoBehaviour
{
    // private:
    [SerializeField] private TMP_Text ResultText;
    [SerializeField] private Animator ResultAnimator;
    [SerializeField] private Button ReturnToSelectionButton;
    // public:
    public static CManager_BattleUI Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    // private:
    private void OnEnable()
    {
        ReturnToSelectionButton.onClick.AddListener(HideBattleResult);
    }
    // public:
    public void ShowBattleResult(bool _ResultState)
    {
        ResultText.text = (_ResultState) ? "YOU WON!" : "YOU LOSE..";
        ResultAnimator.gameObject.SetActive(true); // ?
    }

    public void HideBattleResult() => ResultAnimator.SetTrigger("OnHideBattleResult");
}


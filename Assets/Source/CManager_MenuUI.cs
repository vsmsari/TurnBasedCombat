using UnityEngine;
using TMPro;

public class CManager_MenuUI: MonoBehaviour
{
    // private:
    [SerializeField] private CComponent_AlphaTween m_SceneTransitionComponent;
    [SerializeField] private CUIButton_StartBattle m_StartBattleButton;
    [SerializeField] private Transform m_HeroSelectionButtonContainer;
    [SerializeField] private GameObject m_HeroDetailsPanel;
    [SerializeField] private Transform m_HeroDetailsPivot;
    [SerializeField] private TMP_Text m_Text_HeroDetails_Name;
    [SerializeField] private TMP_Text m_Text_HeroDetails_Level;
    [SerializeField] private TMP_Text m_Text_HeroDetails_AttackPower;
    [SerializeField] private TMP_Text m_Text_HeroDetails_Experience;
    // public:
    public CComponent_AlphaTween SceneTransitionComponent => m_SceneTransitionComponent;
    public static CManager_MenuUI Instance { get; private set; }
    // private:
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    private void OnEnable()
    {
        for (int index = 0; index < m_HeroSelectionButtonContainer.childCount; index++)
        {
            CUIButton_HeroSelection button = m_HeroSelectionButtonContainer.GetChild(index).GetComponent<CUIButton_HeroSelection>();
            button.OnSelectHero += () =>
            {
                CManager_Data.Instance.SelectHeroIndex(button.DataIndex);
                m_StartBattleButton.SetSliderValue(CManager_Data.Instance.GetSelectedHeroCount());
            };
            button.OnUnselectHero += () =>
            {
                CManager_Data.Instance.UnselectHeroIndex(button.DataIndex);
                m_StartBattleButton.SetSliderValue(CManager_Data.Instance.GetSelectedHeroCount());
            };
            button.OnOpenHeroDetails += (_DataIndex, _Location) =>
            {
                m_HeroDetailsPanel.SetActive(true);
                m_HeroDetailsPivot.position = _Location;
                SetHeroDetails(_DataIndex);
            };
        }
    }
    private void SetHeroDetails(int _Index)
    {
        CHeroData heroData = CManager_Data.Instance.GetCollectedHeroData(_Index);
        if (heroData == null) return;
        m_Text_HeroDetails_Name.text = heroData.Name;
        m_Text_HeroDetails_Level.text = CManager_Data.Instance.GetHeroLevel(heroData).ToString();
        m_Text_HeroDetails_AttackPower.text = heroData.AttackPower.ToString();
        m_Text_HeroDetails_Experience.text = heroData.Experience.ToString();
    }
}


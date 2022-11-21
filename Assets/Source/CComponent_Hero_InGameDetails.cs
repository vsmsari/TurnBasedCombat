using UnityEngine;
using TMPro;

public class CComponent_Hero_InGameDetails : MonoBehaviour
{
    // private:
    [SerializeField] private GameObject m_DetailsPanelGO;
    [SerializeField] private TMP_Text m_DetailsNameText;
    [SerializeField] private TMP_Text m_DetailsLevelText;
    [SerializeField] private TMP_Text m_DetailsAPText;
    [SerializeField] private TMP_Text m_DetailsExperienceText;
    
    // public:
    public void ShowHeroDetailsPanel() => m_DetailsPanelGO.SetActive(true);
    public void HideHeroDetailsPanel() => m_DetailsPanelGO.SetActive(false);
    public void SetDetails(string _Name, string _AttackPower, string _Level, string _Experience)
    {
        m_DetailsNameText.text = _Name;
        m_DetailsAPText.text = _AttackPower;
        m_DetailsLevelText.text = _Level;
        m_DetailsExperienceText.text = _Experience;         
    }
}
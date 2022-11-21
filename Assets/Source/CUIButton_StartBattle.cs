using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CUIButton_StartBattle: MonoBehaviour
{
    // private:
    [SerializeField] private Slider m_BackgroundSlider;
    [SerializeField] private float m_SliderSpeedInSeconds = 0.2f;
    [SerializeField] private int m_SliderMultiplier = 10;
    private Button m_ButtonComponent;
    
    private void Awake() => m_ButtonComponent = GetComponent<Button>();
    private void OnEnable()
    {
        m_BackgroundSlider.maxValue = CManager_Data.Instance.BattleHeroCount * m_SliderMultiplier;
        CManager_Data.Instance.OnHeroSetIsFull += EnableButton;
        CManager_Data.Instance.OnHeroSetIsShort += DisableButton;
        m_ButtonComponent.onClick.AddListener(() =>
        {
            CManager_Data.Instance.SaveTemp(); 
            CManager_MenuUI.Instance.SceneTransitionComponent.OnFadeInComplete += CManager_Scene.Instance.LoadBattle;
            CManager_MenuUI.Instance.SceneTransitionComponent.enabled = true;
            CManager_MenuUI.Instance.SceneTransitionComponent.TweenIn();
        });
    }
    private void EnableButton() => m_ButtonComponent.interactable = true;
    private void DisableButton() => m_ButtonComponent.interactable = false;

    public void SetSliderValue(int _SliderValue)
    {
        int firstValue = (int)m_BackgroundSlider.value;
        int lastValue = _SliderValue * m_SliderMultiplier;
       LeanTween.value(m_BackgroundSlider.gameObject, firstValue, lastValue, m_SliderSpeedInSeconds).setOnUpdate( (_Value) => { m_BackgroundSlider.value = _Value ; });
    }

}


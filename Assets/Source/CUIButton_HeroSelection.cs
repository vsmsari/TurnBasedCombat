using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CUIButton_HeroSelection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Private:
    [SerializeField] private GameObject m_Icon;
    [SerializeField] private GameObject m_Background;
    [SerializeField] private float m_PointerHoldTime = 3.0f;
    [SerializeField] private bool m_IsInteractable;
    [SerializeField] private bool m_IsSelected;

    private Image m_SelectionBorder;
    private CComponent_SizeTween m_SizeTweenComponent;
    private bool m_IsPointerDown;
    private float m_PointerTimer;
    private Vector2 m_PointerScreenLocation;

    // Public:
    public int DataIndex {get; private set;}
    public event Action OnUnselectHero = delegate {  };
    public event Action OnSelectHero = delegate {  };
    public event Action<int, Vector3> OnOpenHeroDetails = delegate {  };

    // Private:    
    private void Awake()
    {
        m_SelectionBorder = GetComponent<Image>();
        m_SizeTweenComponent = GetComponent<CComponent_SizeTween>();
    }
    private void OnEnable()
    {
        m_SelectionBorder.enabled = false;
        m_Icon.SetActive(m_IsInteractable);
        OnUnselectHero += HideSelectionBorder;
        OnSelectHero += ShowSelectionBorder;
    }
    private void Update()
    {
        if(m_IsPointerDown)
        {
            m_PointerTimer += Time.deltaTime;
            if (m_PointerTimer >= m_PointerHoldTime) OnOpenHeroDetails?.Invoke(DataIndex, transform.position);
        }
    }

    private void HideSelectionBorder() => m_SelectionBorder.enabled = false;
    private void ShowSelectionBorder() => m_SelectionBorder.enabled = true;
    
    // Public:
    public void OnPointerDown(PointerEventData _EventData)
    {
        if(m_IsInteractable != true) return;
        m_SizeTweenComponent.Tween();
        m_IsPointerDown = true;
    }
    public void OnPointerUp(PointerEventData _EventData)
    {
        if(m_IsInteractable != true) return;
        m_SizeTweenComponent.UndoTween();
        if (m_PointerTimer < m_PointerHoldTime)
        {
            if (m_IsSelected) OnUnselectHero?.Invoke();
            else OnSelectHero?.Invoke();
        }
        m_IsSelected = !m_IsSelected;
        
        m_PointerTimer = 0;
        m_IsPointerDown = false;
    }
    public void SetDataIndex(int _Index) => DataIndex = _Index;
    public void SetInteractable(bool _State) => m_IsInteractable = _State;
    public void SetSprite(Sprite _Sprite) => m_Icon.GetComponent<Image>().sprite = _Sprite;
    public void SetBackgroundVisibility(bool _State) => m_Background.SetActive(_State);

}

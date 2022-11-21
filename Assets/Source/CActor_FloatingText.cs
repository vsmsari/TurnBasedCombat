using System;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class CActor_FloatingText : CActor
{
    [Header("Text")]
    [SerializeField] private TMP_Text m_TextComponent;
    [Header("Tween")]
    [SerializeField] private Vector3 m_MovementDirection;
    [SerializeField] private float m_MovementDirectionXMinimum;
    [SerializeField] private float m_MovementDirectionXMaximum;
    [SerializeField] private float m_MovementDistance;
    [SerializeField] private float m_MovementSpeedInSeconds;
    [SerializeField] private float m_AlphaStart;
    [SerializeField] private float m_AlphaEnd;
    [SerializeField] private float m_AlphaTransitionSpeedInSeconds;
    
    
    private CanvasGroup m_CanvasGroup;
    private Vector3 m_TargetPosition;
    
    public event Action OnComplete = delegate { };
    private void Awake()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
        OnComplete += () => { gameObject.SetActive(false); };
    }
    private void OnEnable()
    {
        // Randomizing the direction x for variaty.
        m_TargetPosition = transform.position + new Vector3(Random.Range(m_MovementDirectionXMinimum, m_MovementDirectionXMaximum), 0, 0); 
        Tween();
    }
    private void AlphaUpdate(float _Value) => m_CanvasGroup.alpha = _Value;
    private void PositionUpdate(Vector3 _NewPos) => transform.position = _NewPos;
    
    public void SetText(string _Text) => m_TextComponent.text = _Text;
    public void Tween()
    {
        LeanTween.value(gameObject, transform.position, m_TargetPosition + (m_MovementDirection * m_MovementDistance), m_MovementSpeedInSeconds).setEaseLinear().setOnUpdateVector3(PositionUpdate);
        LeanTween.value(gameObject, m_AlphaStart, m_AlphaEnd, m_AlphaTransitionSpeedInSeconds).setOnUpdate(AlphaUpdate).setOnComplete(OnComplete);
    }

}
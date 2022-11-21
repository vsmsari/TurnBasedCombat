using UnityEngine;
public class CComponent_ShakeTween : MonoBehaviour
{
    // private:
    [SerializeField] private float m_HorizontalDistance = 0.1f;
    [SerializeField] private float m_VerticalDistance = 0.2f;
    [SerializeField] private float m_HorizontalSpeedInSeconds = 0.3f;
    [SerializeField] private float m_VerticalSpeedInSeconds = 0.1f;
    
    private Vector3 m_DefaultPosition;
    private void OnEnable()
    {
        m_DefaultPosition = transform.position;
    }
    // public:
    public void Tween()
    {
        LeanTween.moveX(gameObject, m_DefaultPosition.x + m_HorizontalDistance, m_HorizontalSpeedInSeconds).setEaseLinear().setOnComplete(()=> {
        LeanTween.moveX(gameObject, m_DefaultPosition.x - m_HorizontalDistance, m_HorizontalSpeedInSeconds * 2).setEaseLinear().setOnComplete(() => { 
        LeanTween.moveX(gameObject, m_DefaultPosition.x, m_HorizontalSpeedInSeconds).setEaseLinear(); }); });
        
        LeanTween.moveY(gameObject, m_DefaultPosition.y + m_VerticalDistance, m_VerticalSpeedInSeconds).setEaseLinear().setOnComplete(()=> {
        LeanTween.moveY(gameObject, m_DefaultPosition.y - m_VerticalDistance, m_VerticalSpeedInSeconds * 2).setEaseLinear().setOnComplete(() => { 
        LeanTween.moveY(gameObject, m_DefaultPosition.y, m_VerticalSpeedInSeconds).setEaseLinear(); }); });
    }

}
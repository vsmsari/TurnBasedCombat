using System;
using UnityEngine;

// NOTE(vsmsari) For me, not using new input system is the biggest gamble for this project. But I though it would be overkill for three simple events..
public class CComponent_PlayerInput : MonoBehaviour
{
    // private:
    [SerializeField] private LayerMask m_TargetLayerMask;
    [SerializeField] private float m_DetailsHoldThreshold = 3f;
    private float m_CurrentHoldTime = 0;
    
    // public:
    public event Action OnTouchPressed = delegate{};
    public event Action OnTouchReleased = delegate{};
    public event Action OnHoldRelease = delegate{};
    
    public void UpdateInput()
    {
        if(Input.GetMouseButtonDown(0)) OnTouchPressed();
        else if(Input.GetMouseButton(0)) 
        {
            m_CurrentHoldTime += Time.deltaTime;
            if(m_CurrentHoldTime >= m_DetailsHoldThreshold) OnHoldRelease();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(m_CurrentHoldTime < m_DetailsHoldThreshold) OnTouchReleased();
            m_CurrentHoldTime = 0;
        } 
    }

    public GameObject RaycastResult()
    {
        Vector2 raycastLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(raycastLocation, Vector2.zero, m_TargetLayerMask);
        return (hit.collider) ? hit.collider.gameObject : null;
    }
}

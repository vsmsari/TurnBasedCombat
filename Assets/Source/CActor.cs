using UnityEngine;

public abstract class CActor : MonoBehaviour
{
    // private:
    [Header("Components")]
    [SerializeField] protected CController_Animator m_AnimatorController;
    [SerializeField] protected SpriteRenderer m_SpriteRendererComponent;
    
    // public:
    public CController_Animator AnimatorController => m_AnimatorController;
}
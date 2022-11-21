using System.Collections.Generic;
using UnityEngine;

public class CManager_FloatingTexts : MonoBehaviour
{
    [Header("Initialize")]
    [SerializeField] private GameObject m_FloatingTextPF;
    [SerializeField] private int m_Count;
    
    [Header("Update")]
    [SerializeField] private List<CActor_FloatingText> m_FloatingTextlist = new List<CActor_FloatingText>();
    
    public static CManager_FloatingTexts Instance { get; private set; }
    private void Awake()
    {
        Instance = this;

        for (int index = 0; index < m_Count; index++)
        {
            CActor_FloatingText newFloatingText = GameObject.Instantiate(m_FloatingTextPF, transform).GetComponent<CActor_FloatingText>();
            newFloatingText.gameObject.SetActive(false);
            if (newFloatingText == null) return;
            
            newFloatingText.OnComplete += RecycleLast;
            m_FloatingTextlist.Add(newFloatingText);
        }
    }

    private void RecycleLast()
    {
        CActor_FloatingText last = m_FloatingTextlist[0];
        m_FloatingTextlist.RemoveAt(0);
        m_FloatingTextlist.Insert(m_Count - 1, last);
    }
    public void Spawn(Vector3 _SpawnPosition, string _Text)
    {
        m_FloatingTextlist[0].transform.position = _SpawnPosition;
        m_FloatingTextlist[0].SetText(_Text);
        m_FloatingTextlist[0].gameObject.SetActive(true);
    }


}

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CManager_Data))]
public class CComponent_GenerateHeroData : MonoBehaviour
{
    // private:
    [SerializeField] private int m_MinimumHealth = 80;
    [SerializeField] private int m_MaximumHealth = 100;
    [SerializeField] private int m_MinimumAttackPower = 20;
    [SerializeField] private int m_MaximumAttackPower = 50;
    [SerializeField] private List<string> m_HeroNameList = new List<string>(){"Adam", "Jonathan", "Weasley", "Peter", "Lisa", "Ashley", "Sarah", "John", "Jack", "Tony", "Harry", "Gwen", "Gwyndolin", "Gwynevere", "Gwyn", "John", "Jack", "Arthorias", "Malenia", "Isshin"};
    [SerializeField] private List<Sprite> m_HeroSpriteList = new List<Sprite>();
    private CManager_Data m_DataManager;
    
    // public:
    public List<Sprite> HeroSpriteList => m_HeroSpriteList;
    public void GenerateHero(int _Count = 1)
    {
        if(_Count > 1)
        {
            for(int index = 0; index < _Count; index++) GenerateHero();
        }
        else // As default add single hero.
        {
            string heroName = m_HeroNameList[Random.Range(0, m_HeroNameList.Count)];
            m_HeroNameList.Remove(heroName);
            
            int heroHealth = Random.Range(m_MinimumHealth, m_MaximumHealth);
            int heroAttackPower = Random.Range(m_MinimumAttackPower, m_MaximumAttackPower);
            int experience = 0;
            CHeroData heroBase = new CHeroData(heroName, heroHealth, heroAttackPower, experience);
            CManager_Data.Instance.AddHeroData(heroBase);
        }
    }
    public Sprite GetSpriteByIndex(int _Index) => (HeroSpriteList.Count > _Index) ? HeroSpriteList[_Index] : null;

}

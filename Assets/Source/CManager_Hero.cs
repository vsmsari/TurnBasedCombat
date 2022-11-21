using System.Collections.Generic;
using UnityEngine;

public class CManager_Hero : MonoBehaviour
{
    // private:
    [SerializeField] private List<CActor_Hero> m_AliveHeroList = new List<CActor_Hero>();
    [SerializeField] private List<CActor_Hero> m_DeadHeroList = new List<CActor_Hero>();
    
    // public:
    public List<CActor_Hero> AliveHeroList => m_AliveHeroList;
    public List<CActor_Hero> DeadHeroList => m_DeadHeroList;

    // Private: 
    private void Awake()
    {
        m_AliveHeroList = new List<CActor_Hero>();
        for(int index = 0; index < transform.childCount; index++)
        {
            CActor_Hero hero = transform.GetChild(index).GetComponent<CActor_Hero>();
            CHeroData heroData = CManager_Data.Instance.GetSelectedHeroData(index);
            hero.SetHeroData(heroData);
            m_AliveHeroList.Add(hero);
        }
    }

    // Public:
    public CActor_Hero GetRandomSpawnedHero() => (AliveHeroList.Count > 0) ? AliveHeroList[Random.Range(0, AliveHeroList.Count)] : null;
    public void RemoveAliveHero(CActor_Hero _HeroToRemove)
    {
        AliveHeroList.Remove(_HeroToRemove);
        DeadHeroList.Add(_HeroToRemove);
        if(AliveHeroList.Count == 0) CManager_CombatStates.Instance.ToResultState();
    }

    public void ShowHealthbars()
    {
        for (int index = 0; index < AliveHeroList.Count; index++)
        {
            CComponent_ActorHealth healthComponent = AliveHeroList[index].HealthComponent;
            healthComponent.ShowHealthbar();
        }
    }
}

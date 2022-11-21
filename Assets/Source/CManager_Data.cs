using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CGameData { }

[System.Serializable]
public class CHeroData
{
    public bool IsActive;
    public int DataIndex;
    public string Name;
    public int Health;
    public int AttackPower;
    public int Experience;

    public CHeroData(string _Name, int _Health, int _AttackPower, int _Experience)
    {
        this.DataIndex = -1;
        this.Name = _Name;
        this.Health = _Health;
        this.AttackPower = _AttackPower;
        this.Experience = _Experience;
        this.IsActive = true;
    }
    // public void SetDataIndex(int _DataIndex) => this.DataIndex = _DataIndex;
}

[System.Serializable]
public class CTempData : CGameData
{
    public List<int> HeroSelectionIndexList;
    public CTempData() => HeroSelectionIndexList = new List<int>();
    // public CTempData(List<int> _HeroSelectionIndexList) => HeroSelectionIndexList = _HeroSelectionIndexList;
}

[System.Serializable]
public class CSaveData : CGameData
{
    public bool IsPlayersFirstTime;
    public int BattleIndex;
    public List<CHeroData> CollectedHeroList;

    public CSaveData()
    {
        IsPlayersFirstTime = true;
        BattleIndex = 0;
        CollectedHeroList = new List<CHeroData>();
    }
}


public class CManager_Data : MonoBehaviour
{
    // private:
    private const string DATA_FILE_NAME = "Save.json";
    private const string TEMP_FILE_NAME = "Temp.json";
    
    [SerializeField] private CSaveData m_SaveData = new CSaveData();
    [SerializeField] private CTempData m_TempData = new CTempData();

    [SerializeField] private int m_BattleHeroCount = 3; // Maximum hero count to initiate battle.
    [SerializeField] private int m_RequiredExperiencePerLevel = 5; // Heroes will level up once in 5 experience point.    
    [SerializeField] private int m_RequiredBattleCountPerNewHero = 5; //      

    private int m_StartingHeroCount = 3;
    
    private CComponent_GenerateHeroData m_GenerateHeroDataComponent;
    private CComponent_FileIO m_FileIOComponent;
    
    // public: 
    public event Action OnHeroSetIsFull = delegate {};
    public event Action OnHeroSetIsShort = delegate {};
    public int BattleHeroCount => m_BattleHeroCount;
    public int RequiredExperiencePerLevel => m_RequiredExperiencePerLevel;
    public static CManager_Data Instance { get; private set; }
    
    // private:
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        
        m_FileIOComponent = GetComponent<CComponent_FileIO>();
        m_GenerateHeroDataComponent = GetComponent<CComponent_GenerateHeroData>();

        Load();
        LoadTemp();

        if (m_SaveData.IsPlayersFirstTime)
        {
            m_SaveData.IsPlayersFirstTime = false;
            m_GenerateHeroDataComponent.GenerateHero(m_StartingHeroCount);
            Save();
            return;
        }
        if (m_SaveData.BattleIndex % m_RequiredBattleCountPerNewHero == 0)
        {
            int maxHeroCount = m_StartingHeroCount + (m_SaveData.BattleIndex / m_RequiredBattleCountPerNewHero);
            if (GetCollectedHeroCount() >= maxHeroCount || GetCollectedHeroCount() == 10) return;
            
            m_GenerateHeroDataComponent.GenerateHero();
            Save();
        }
    }

    // public:
    public void Save() => m_FileIOComponent.WriteJsonToFile(DATA_FILE_NAME, m_SaveData);
    public void SaveTemp() => m_FileIOComponent.WriteJsonToFile(TEMP_FILE_NAME, m_TempData);
    public void Load()
    {
        string json = m_FileIOComponent.LoadJsonFromFile(DATA_FILE_NAME);
        m_SaveData = JsonUtility.FromJson<CSaveData>(json);
        if (m_SaveData == null) m_SaveData = new CSaveData();
    }
    public void LoadTemp()
    {
        string json = m_FileIOComponent.LoadJsonFromFile(TEMP_FILE_NAME);
        m_TempData = JsonUtility.FromJson<CTempData>(json);
        if (m_TempData == null) m_TempData = new CTempData();
    }
    public void ResetTemp() => m_TempData = new CTempData();
    public void AddHeroData(CHeroData _NewHero)
    {
        _NewHero.DataIndex = m_SaveData.CollectedHeroList.Count;
        m_SaveData.CollectedHeroList.Add(_NewHero);
    }
    public void SelectHeroIndex(int _Index)
    {
        if (m_TempData.HeroSelectionIndexList.Count >= m_BattleHeroCount) return;
        
        m_TempData.HeroSelectionIndexList.Add(_Index);
        if(m_TempData.HeroSelectionIndexList.Count == m_BattleHeroCount) OnHeroSetIsFull();
    }
    public void UnselectHeroIndex(int _Index)
    {
        m_TempData.HeroSelectionIndexList.Remove(_Index);
        if(m_TempData.HeroSelectionIndexList.Count < m_BattleHeroCount) OnHeroSetIsShort();
    }
    public CHeroData GetCollectedHeroData(int _Index) => (_Index < m_SaveData.CollectedHeroList.Count) ? m_SaveData.CollectedHeroList[_Index] : null;

    public CHeroData GetSelectedHeroData(int _Index)
    {
        int dataIndex = m_TempData.HeroSelectionIndexList[_Index];
        return m_SaveData.CollectedHeroList[dataIndex];
    }

    public int GetSelectedHeroCount() => m_TempData.HeroSelectionIndexList.Count;
    public bool IsDataIndexSelected(int _Index)
    {
        return m_TempData.HeroSelectionIndexList.Contains(_Index);
    }

    public int GetCollectedHeroCount() => m_SaveData.CollectedHeroList.Count;
    public int GetHeroLevel(CHeroData _Data) => (_Data.Experience < m_RequiredExperiencePerLevel) ? 1 : (_Data.Experience / m_RequiredExperiencePerLevel) + 1;
    public void IncreaseBattleIndex() => m_SaveData.BattleIndex++;
    public Sprite GetSprite(int _Index) => m_GenerateHeroDataComponent.GetSpriteByIndex(_Index);

}

using System.Collections.Generic;

public class CCombatState_Result : CSMState_Base
{
    public override void BeginState()
    {
        m_Manager.enabled = false; // Since the combat is finished I'm basically cutting off the manager right here. 
        List<CActor_Hero> aliveHeroList = m_Manager.HeroManager.AliveHeroList;
        
        CManager_Data.Instance.IncreaseBattleIndex(); 
        CManager_BattleUI.Instance.ShowBattleResult(aliveHeroList.Count > 0); // Win or lose based on the alive hero count.
        
        if (aliveHeroList.Count == 0) return; 
        
        for (int index = 0; index < aliveHeroList.Count; index++)
        {
            CHeroData currentHeroData = aliveHeroList[index].HeroData;
            
            currentHeroData.Experience++;
            
            if (currentHeroData.Experience % CManager_Data.Instance.RequiredExperiencePerLevel == 0)
            {
                currentHeroData.Health += (currentHeroData.Health * 10) / 100;
                currentHeroData.AttackPower += (currentHeroData.AttackPower * 10) / 100;
            }
        }
        CManager_Data.Instance.Save();
        CManager_Data.Instance.ResetTemp(); 
        CManager_Data.Instance.SaveTemp(); // TODO(vsmsari): DO NOT FORGET TO UNCOMMENT THIS. 
    }

}
using UnityEngine;
public class CUIPanel_HeroContainer : MonoBehaviour
{
    private void Awake()
    {
        // Matching the hero indices to button indices so that we can get hero informations by using their indices later. 
        for (int index = 0; index < transform.childCount; index++)
        {
            CUIButton_HeroSelection button = transform.GetChild(index).GetComponent<CUIButton_HeroSelection>();
            bool isInteractable = index < CManager_Data.Instance.GetCollectedHeroCount();
            button.SetInteractable(isInteractable);
            button.SetDataIndex(index);
            button.SetSprite(CManager_Data.Instance.GetSprite(index));

            if (index < CManager_Data.Instance.GetCollectedHeroCount()) button.SetBackgroundVisibility(true);
            if (button.DataIndex >= CManager_Data.Instance.GetCollectedHeroCount()) continue;
            
            // If all the three heroes are selected, others should be uninteractable.  
            CManager_Data.Instance.OnHeroSetIsFull += () => { if(CManager_Data.Instance.IsDataIndexSelected(button.DataIndex) != true) button.SetInteractable(false); };
            CManager_Data.Instance.OnHeroSetIsShort += () => { if (CManager_Data.Instance.IsDataIndexSelected(button.DataIndex) != true) button.SetInteractable(true); };
        }
    }
}

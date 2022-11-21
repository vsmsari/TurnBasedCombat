using System.Collections.Generic;
using UnityEngine;

public class CManager_Enemy : MonoBehaviour
{
    // private:
    [SerializeField] private int m_MinimumAttackPower = 10;
    [SerializeField] private int m_MaximumAttackPower = 30;
    [SerializeField] private int m_MinimumHealth = 60;
    [SerializeField] private int m_MaximumHealth = 80;
    private List<CActor_Enemy> m_EnemyList = new List<CActor_Enemy>();
    private void Awake()
    {
        // Randomizing the enemy.
        for (int index = 0; index < transform.childCount; index++)
        {
            CActor_Enemy enemy = transform.GetChild(index).GetComponent<CActor_Enemy>();
            int randomMinAttackPower = Random.Range(m_MinimumAttackPower, m_MaximumAttackPower);
            int randomMaxAttackPower = Random.Range(randomMinAttackPower, m_MaximumAttackPower);
            int randomHealth = Random.Range(m_MinimumHealth, m_MaximumHealth);

            enemy.SetAttackPowerRange(randomMinAttackPower, randomMaxAttackPower);
            enemy.HealthComponent.SetMaximumHealth(randomHealth);
            m_EnemyList.Add(enemy);
        }
    }
    // public:
    public CActor_Enemy GetEnemy(int _Index = 0) => (m_EnemyList.Count > 0) ? m_EnemyList[_Index] : null;

    public void ShowHealthbar()
    {
        GetEnemy().HealthComponent.ShowHealthbar();
    }
}
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EnemySkill
{
    public string skillName;
    public int damage;
}

public class EnemyBattle : MonoBehaviour
{
    [Header("기본 정보")]
    public string enemyName;

    [Header("체력")]
    public int maxHP = 100;
    public int currentHP;

    public Slider hpBar;

    [Header("스킬")]
    public EnemySkill[] skills;

    private int nextSkill = 0;

    private BattleVisual visual;

    private void Awake()
    {
        currentHP = maxHP;

        visual = GetComponent<BattleVisual>();

        if (hpBar != null)
        {
            hpBar.maxValue = maxHP;
            hpBar.value = currentHP;
        }
    }

    // 다음 사용할 스킬 반환
    public EnemySkill GetNextSkill()
    {
        if (skills == null || skills.Length == 0)
            return null;

        EnemySkill skill = skills[nextSkill];

        nextSkill++;

        if (nextSkill >= skills.Length)
            nextSkill = 0;

        return skill;
    }

    // 데미지 처리
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP < 0)
            currentHP = 0;

        if (hpBar != null)
            hpBar.value = currentHP;

        if (visual != null)
            visual.FlashRed();

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayDamage();
    }
}
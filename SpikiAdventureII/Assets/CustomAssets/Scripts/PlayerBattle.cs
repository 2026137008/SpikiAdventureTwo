using UnityEngine;
using UnityEngine.UI;

public class PlayerBattle : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;

    public int attackPower = 15;

    public Slider hpBar;

    public bool isDefending = false;

    private BattleVisual visual;

    private void Awake()
    {
        currentHP = maxHP;

        visual = GetComponent<BattleVisual>();

        hpBar.maxValue = maxHP;
        hpBar.value = currentHP;
    }

    public void TakeDamage(int damage)
    {
        if (isDefending)
            damage /= 2;

        currentHP -= damage;

        if (currentHP < 0)
            currentHP = 0;

        hpBar.value = currentHP;

        visual.FlashRed();

        AudioManager.Instance.PlayDamage();
    }
}
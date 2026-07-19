using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public PlayerBattle player;
    public EnemyBattle enemy;

    public BattleUI ui;

    public string nextSceneName;

    BattleState state;

    EnemySkill nextEnemySkill;

    IEnumerator Start()
    {
        state = BattleState.Start;

        ui.UpdateHP(player.currentHP, enemy.currentHP);

        ui.SetButtons(false);

        ui.SetMessage(enemy.enemyName + "이(가) 앞을 막아선 것이에요!");

        yield return new WaitForSeconds(3f);

        nextEnemySkill = enemy.GetNextSkill();

        StartPlayerTurn();
    }

    void StartPlayerTurn()
    {
        state = BattleState.PlayerTurn;

        player.isDefending = false;

        ui.SetButtons(true);

        ui.SetMessage(enemy.enemyName + "은(는) " +
                      nextEnemySkill.skillName +
                      "을(를) 준비 중인 거에요!");
    }

    public void Attack()
    {
        if (state != BattleState.PlayerTurn)
            return;

        AudioManager.Instance.PlayButton();

        state = BattleState.Busy;
        ui.SetButtons(false); 

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        state = BattleState.Busy;

        ui.SetButtons(false);

        enemy.TakeDamage(player.attackPower);

        ui.UpdateHP(player.currentHP, enemy.currentHP);

        ui.SetMessage("진심을 담아 공격한 것이에요!");

        yield return new WaitForSeconds(2f);

        if (enemy.currentHP <= 0)
        {
            BattleVisual visual = enemy.GetComponent<BattleVisual>();

            if (visual != null)
                yield return StartCoroutine(visual.FadeOut());

            StartCoroutine(WinBattle());

            yield break;
        }

        StartCoroutine(EnemyTurn());
    }

    public void Skill()
    {
        if (state != BattleState.PlayerTurn)
            return;

        AudioManager.Instance.PlayButton();

        state = BattleState.Busy;
        ui.SetButtons(false); 

        StartCoroutine(PlayerSkill());
    }

    IEnumerator PlayerSkill()
    {
        state = BattleState.Busy;

        ui.SetButtons(false);

        enemy.TakeDamage(player.attackPower * 3);

        ui.UpdateHP(player.currentHP, enemy.currentHP);

        ui.SetMessage("매우 강력한 스킬인 것이에요!");

        yield return new WaitForSeconds(2f);

        if (enemy.currentHP <= 0)
        {
            StartCoroutine(WinBattle());

            yield break;
        }

        StartCoroutine(EnemyTurn());
    }

    public void Item()
    {
        if (state != BattleState.PlayerTurn)
            return;

        AudioManager.Instance.PlayButton();

        state = BattleState.Busy;
        ui.SetButtons(false); 

        StartCoroutine(PlayerItem());
    }

    IEnumerator PlayerItem()
    {
        state = BattleState.Busy;

        ui.SetButtons(false);

        player.currentHP += 20;

        if (player.currentHP > player.maxHP)
            player.currentHP = player.maxHP;

        ui.UpdateHP(player.currentHP, enemy.currentHP);

        ui.SetMessage("체력을 (겨우) 20 회복한 거에요!");

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemyTurn());
    }

    public void Defend()
    {
        if (state != BattleState.PlayerTurn)
            return;

        AudioManager.Instance.PlayButton();

        state = BattleState.Busy;
        ui.SetButtons(false); 

        StartCoroutine(PlayerDefend());
    }

    IEnumerator PlayerDefend()
    {
        state = BattleState.Busy;

        ui.SetButtons(false);

        player.isDefending = true;

        ui.SetMessage("방어 스킬을 사용한 것이에요!");

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        state = BattleState.EnemyTurn;

        ui.SetMessage(enemy.enemyName + "의 " + nextEnemySkill.skillName + "을(를) 얻어맞았어요! 흐에엥!");

        yield return new WaitForSeconds(1.5f);

        player.TakeDamage(nextEnemySkill.damage);

        ui.UpdateHP(player.currentHP, enemy.currentHP);

        yield return new WaitForSeconds(2f);

        if (player.currentHP <= 0)
        {
            state = BattleState.Lose;

            ui.SetMessage("스핔이 열심히 했는데....");

            AudioManager.Instance.PlayGameOver();

            BattleVisual visual = player.GetComponent<BattleVisual>();

            if (visual != null)
                yield return StartCoroutine(visual.FadeOut());

            yield break;
        }

        nextEnemySkill = enemy.GetNextSkill();

        StartPlayerTurn();
    }

    IEnumerator WinBattle()
    {
        state = BattleState.Win;

        ui.SetMessage("쪼아요! " + enemy.enemyName + "을(를) 처치한 것이에요!");

        AudioManager.Instance.PlayVictory();

        float waitTime = 3.5f;

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(nextSceneName);
    }
}
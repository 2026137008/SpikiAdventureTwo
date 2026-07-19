using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [Header("메시지")]
    public Text messageText;

    [Header("HP")]
    public Text playerHPText;
    public Text enemyHPText;

    [Header("버튼")]
    public Button attackButton;
    public Button skillButton;
    public Button itemButton;
    public Button defendButton;

    public void UpdateHP(int playerHP, int enemyHP)
    {
        playerHPText.text = "" + playerHP;
        enemyHPText.text = "" + enemyHP;
    }

    public void SetMessage(string text)
    {
        messageText.text = text;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayText();
    }

    public void SetButtons(bool active)
    {
        attackButton.interactable = active;
        skillButton.interactable = active;
        itemButton.interactable = active;
        defendButton.interactable = active;
    }
}
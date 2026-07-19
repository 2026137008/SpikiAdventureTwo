using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Stage1DialoguePart2 : MonoBehaviour, IPointerDownHandler
{
    [Header("Character")]
    public GameObject chuchu;
    public GameObject tori;

    [Header("UI")]
    public Text ScriptText_dialogue;
    public Text ScriptText_name;

    [Header("Dialogue")]
    public string[] dialogue;
    public string[] speaker;

    private int dialogueCount = 0;

    [Header("Typing")]
    public float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private string currentDialogue = "";

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip textSound;

    void Start()
    {
        ShowDialogue();
    }

    public void OnPointerDown(PointerEventData data)
    {
        // 타이핑 중이면 끝까지 출력
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            ScriptText_dialogue.text = currentDialogue;
            isTyping = false;
            return;
        }

        // 다음 대사
        dialogueCount++;

        if (dialogueCount >= dialogue.Length)
        {
            SceneManager.LoadScene("Stage1Phase2");
            return;
        }

        ShowDialogue();
    }

    void ShowDialogue()
    {
        currentDialogue = dialogue[dialogueCount];

        ScriptText_name.text = speaker[dialogueCount];

        if (speaker[dialogueCount] == "0" || speaker[dialogueCount] == "1")
        {
            Spiki1();
        }
        else if (speaker[dialogueCount] == "2")
        {
            Spiki2();
        }
        else if (speaker[dialogueCount] == "3" || speaker[dialogueCount] == "4")
        {
            Spiki1();
        }

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(currentDialogue));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        ScriptText_dialogue.text = "";

        foreach (char c in text)
        {
            ScriptText_dialogue.text += c;

            // 띄어쓰기는 효과음 X
            if (c != ' ' && c != '\n' && textSound != null)
            {
                audioSource.PlayOneShot(textSound);
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void Spiki1()
    {
        chuchu.SetActive(true);
        tori.SetActive(false);
    }

    void Spiki2()
    {
        chuchu.SetActive(false);
        tori.SetActive(true);
    }
}
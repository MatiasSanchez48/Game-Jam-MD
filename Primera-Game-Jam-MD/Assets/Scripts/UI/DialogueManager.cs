using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("References")]
    public GameObject dialogue;
    public GameObject flameBackground;

    [Header("Dialogue")]
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueTextButton;

    public string[] lines;
    public float typingSpeed = 0.04f;

    private int index;
    private bool isTyping;
    private Coroutine typingCoroutine;
    private PlayerController playerController;

    public System.Action OnDialogueFinished;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        dialogueText.text = "";
        dialogue.SetActive(false);

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!dialogue.activeSelf) return;
        flameBackground.SetActive(false);
        playerController.canMove = false;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Next();
        }
    }
    public void Next()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = lines[index];
            isTyping = false;
        }
        else
        {
            index++;

            if (index < lines.Length)
            {
                StartTyping();
                dialogueTextButton.text = "Continuar";
            }
            else
            {
                dialogueTextButton.text = "Cerrar";
                EndDialogue();
            }
        }
    }
    public void StartDialogue(DialogueData data)
    {
        lines = data.lines;
        index = 0;
        dialogueText.text = "";
        dialogue.SetActive(true);
        StartTyping();
    }

    public void EndDialogue()
    {
        dialogueText.text = "";

        flameBackground.SetActive(true);
        playerController.canMove = true;

        OnDialogueFinished?.Invoke();
        OnDialogueFinished = null;
        // devolver control al player, etc
    }

    void StartTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeLine(lines[index]));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

}

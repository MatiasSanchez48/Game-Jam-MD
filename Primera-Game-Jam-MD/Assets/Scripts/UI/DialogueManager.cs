using Cinemachine;
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
    [Header("Teleport")]
    public Transform teleportTarget;
    public Transform teleportTargetCamera;
    public CinemachineVirtualCamera vcam;

    [Header("Dialogue")]
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueTextButton;

    public string[] lines;
    public float typingSpeed = 0.04f;

    private int index;
    private bool isTyping;
    private Coroutine typingCoroutine;
    private GameObject player;


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

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!dialogue.activeSelf) return;
        flameBackground.SetActive(false);
        player.GetComponent<PlayerController>().canMove = false;

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
        player.GetComponent<PlayerController>().canMove = true;

        if (dialogue.activeSelf)
        {
            TeleportPlayer();
        }
        dialogue.SetActive(false);

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
    void TeleportPlayer()
    {
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        player.transform.position = teleportTarget.position;
        player.transform.rotation = teleportTarget.rotation;

        if (cc != null) cc.enabled = true;

        CameraFollowZOnly cam = vcam.GetComponent<CameraFollowZOnly>();
        if (cam != null && teleportTargetCamera != null)
            cam.SnapToPosition(teleportTargetCamera);
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

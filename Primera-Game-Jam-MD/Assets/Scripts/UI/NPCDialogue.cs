using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueData[] dialogueStages;

    private int currentStage = 0;
    private bool playerInRange;
    private bool isTalking;
    private bool dialogueFinished;
    void Update()
    {
        if (!playerInRange || isTalking ) return;
        // esperar a que suelte la tecla
        if (dialogueFinished)
        {
            if (Input.GetKeyUp(KeyCode.E))
                dialogueFinished = false;

            return;
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            isTalking = true;
            PlayerInteraction.Instance.isInteracting = false;

            DialogueManager.Instance.OnDialogueFinished = OnDialogueFinished;
            DialogueManager.Instance.StartDialogue(dialogueStages[currentStage]);
        }
    }
    void OnDialogueFinished()
    {
        isTalking = false;

        // si es la ultima etapa del dialogo
        if (currentStage >= dialogueStages.Length - 1)
        {
            PlayerInteraction.Instance.isInteracting = true;
            dialogueFinished = true;
            return;
        }

        currentStage++;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            DialogueManager.Instance.EndDialogue();
        }
    }
}

[System.Serializable]
public class DialogueData
{
    public string[] lines;
}
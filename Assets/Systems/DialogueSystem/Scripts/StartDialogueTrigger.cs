using UnityEngine;

public class StartDialogueTrigger : MonoBehaviour
{
    [SerializeField] private int m_DialogueIndex;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            var dialogueManager = DialogueManager.Instance;
            if (dialogueManager != null)
            {
                dialogueManager.StartDialogue(m_DialogueIndex);
            }
            else
            {
                Debug.LogWarning("DialogueManager instance is null.");
            }
        }
    }
}

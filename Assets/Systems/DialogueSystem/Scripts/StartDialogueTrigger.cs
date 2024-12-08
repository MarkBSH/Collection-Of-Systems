using UnityEngine;

public class StartDialogueTrigger : MonoBehaviour
{
    [SerializeField] private int m_DialogueIndex;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            DialogueManager.Instance.StartDialogue(m_DialogueIndex);
        }
    }
}

using UnityEngine;

public class StartDialogueTrigger : MonoBehaviour
{
    #region Unity Methods

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            StartDialogue();
        }
    }

    #endregion

    #region Components



    #endregion

    #region DialogueStarter

    public int m_DialogueIndex;

    public void StartDialogue()
    {
        DialogueManager _DialogueManager = DialogueManager.Instance;

        _DialogueManager.StartDialogue(m_DialogueIndex);
    }

    #endregion
}

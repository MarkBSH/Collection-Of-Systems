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
        var _DialogueManager = DialogueManager.Instance;

        if (_DialogueManager == null)
        {
            DebugWarning("DialogueManager instance is null.");
            return;
        }

        _DialogueManager.StartDialogue(m_DialogueIndex);
    }

    #endregion

    #region Debugging

    private void DebugWarning(string _warning)
    {
        Debug.LogWarning("Warning: " + _warning);
    }

    #endregion
}

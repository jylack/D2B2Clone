using UnityEngine;
using UnityEngine.Playables;

public class TestHsy : MonoBehaviour, INotificationReceiver
{
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is ScDialogueSignalAsset signal)
        {
            ScHandUpGuide.tetst.ShowDialogue(signal.dialogueKey);
        }
    }
}

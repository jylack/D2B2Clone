using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class DialoguesSignalReceiver : MonoBehaviour, INotificationReceiver
{
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is ScDialogueSignalAsset signal)
        {
            Debug.Log("signal.dialogueIndex : " + signal.dialogueIndex);
            ScHandUpGuide.Instance.ShowDialogue(signal.dialogueIndex);
        }
    }
}

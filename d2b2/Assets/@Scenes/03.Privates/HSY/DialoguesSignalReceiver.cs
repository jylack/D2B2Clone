using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class DialoguesSignalReceiver : MonoBehaviour, INotificationReceiver
{
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        Debug.Log("Received Signal Type: " + notification.GetType().Name);
        if (notification is DialogueMarker signal)
        {
            Debug.Log("signal.dialogueIndex : " + signal.dialogueIndex);
            ScHandUpGuide.tetst.ShowDialogue(signal.dialogueIndex);
        }
    }
}

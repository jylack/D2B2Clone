using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogueMarker : Marker,INotification
{
    public int dialogueIndex;
    public PropertyName id => new PropertyName();
}

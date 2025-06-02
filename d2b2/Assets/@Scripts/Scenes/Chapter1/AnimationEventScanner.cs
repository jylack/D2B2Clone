using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR

public class AnimationEventScanner
{
    [MenuItem("Tools/Scan for Hidden AnimationEvents")]
    public static void Scan()
    {
        var clips = Resources.FindObjectsOfTypeAll<AnimationClip>();
        int count = 0;

        foreach (var clip in clips)
        {
            var events = AnimationUtility.GetAnimationEvents(clip);
            foreach (var e in events)
            {
                if (string.IsNullOrEmpty(e.functionName))
                {
                    Debug.LogWarning($"{clip.name} contains empty AnimationEvent at time {e.time}!");
                    count++;
                }
                else
                {
                    Debug.Log($"{clip.name} has AnimationEvent: {e.functionName} at {e.time}s");
                }
            }
        }

        if (count == 0)
        {
            Debug.Log("No empty AnimationEvents found.");
        }
    }
}
#endif

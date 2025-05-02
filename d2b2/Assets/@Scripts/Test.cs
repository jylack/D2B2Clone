using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;

public class Test : MonoBehaviour
{
    private FirebaseAuth auth;

    private async void Start()
    {
        await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(t =>
        {
            if (t.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                print(auth.App.Name);
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + t.Result);
            }
        });
    }
}

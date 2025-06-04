using UnityEngine;
using UnityEditor;
using UnityEngine.Timeline;

public class FindSignalReceiversEditor : EditorWindow
{
    [MenuItem("Tools/Find SignalReceivers")]
    static void ShowWindow()
    {
        GetWindow<FindSignalReceiversEditor>("SignalReceiver Finder");
    }

    void OnGUI()
    {
        if (GUILayout.Button("씬 & 프리팹의 모든 SignalReceiver 출력"))
        {
            // 씬에 존재하는 SignalReceiver와 에셋(프리팹) 상의 SignalReceiver까지 모두 찾는다.
            var allReceivers = Resources.FindObjectsOfTypeAll<SignalReceiver>();

            Debug.Log($" 발견된 SignalReceiver 개수: {allReceivers.Length}");
            foreach (var r in allReceivers)
            {
                string path;
                // r이 씬 오브젝트라면 transform이 null이 아니므로 경로를 잡아주고,
                // 에셋(프리팹)이라면 assetPath로 정보 표시
                if (r.gameObject != null && r.gameObject.scene.isLoaded)
                {
                    path = GetGameObjectPath(r.gameObject) + "  (씬 오브젝트)";
                }
                else
                {
                    // 프리팹 에셋의 경우 AssetDatabase에서 경로를 꺼낸다.
                    string assetPath = AssetDatabase.GetAssetPath(r);
                    path = $"{r.name} (프리팹 에셋) - {assetPath}";
                }
                Debug.Log($" 이름: {r.name} | 경로: {path}");
            }
        }
    }

    static string GetGameObjectPath(GameObject go)
    {
        string path = go.name;
        Transform t = go.transform;
        while (t.parent != null)
        {
            t = t.parent;
            path = t.name + "/" + path;
        }
        return path;
    }
}

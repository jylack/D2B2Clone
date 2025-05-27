using UnityEngine;

public class PathFindingCollTest : MonoBehaviour
{
    [SerializeField] private bool isOn;




    private void OnTriggerEnter(Collider other)
    {
        if (!isOn)
        {
            other.GetComponent<ScCh3Npc>().StopPathFinding();
        }
    }
}

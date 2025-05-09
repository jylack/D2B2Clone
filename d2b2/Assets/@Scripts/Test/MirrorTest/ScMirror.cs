using UnityEngine;

public class ScMirror : MonoBehaviour
{
    public Transform playerTransform;
    public Transform mirrorCamTransform;



    private void LateUpdate()
    {
        // Player -> Mirror
        Vector3 targetDir = mirrorCamTransform.position - playerTransform.position;

        // Mirror -> Target
        Vector3 reflected = Vector3.Reflect(targetDir, transform.forward);

        mirrorCamTransform.rotation = Quaternion.LookRotation(reflected, Vector3.up);

        // Player -> Mirror
        //Vector3 targetDir = playerTransform.position - mirrorCamTransform.position;
        //mirrorCamTransform.rotation = Quaternion.LookRotation(targetDir, Vector3.up);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerTransform.position, mirrorCamTransform.position);

        Gizmos.color = Color.yellow;
        Vector3 targetDir = mirrorCamTransform.position - playerTransform.position;
        Vector3 reflected = Vector3.Reflect(targetDir, transform.forward);
        Gizmos.DrawLine(mirrorCamTransform.position, mirrorCamTransform.position + reflected.normalized * 8f);
    }
}

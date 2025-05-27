using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBlindRegionMiniGameScene : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] Transform cameraOffset;
    [SerializeField] Transform camera;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(10);
        playerPos.transform.position = new Vector3 (-1.5f, 2.7f, 0);
        cameraOffset.transform.rotation = Quaternion.Euler(0, -camera.transform.eulerAngles.y, 0);

    }

}

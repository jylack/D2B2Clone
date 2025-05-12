using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScRespawn : MonoBehaviour
{
    [Header("스폰 위치")]
    [SerializeField] Transform[] spawnPoint;
    private int _currentStep = 0;

    TeleportationProvider tel;
    TeleportRequest telPos;
    private void Start()
    {
        _currentStep = Manager.Instance.GameMgr.CurrentStep;

        //Find 할때 현재 오브젝트의 자식들 중에서부터 찾아야함
        tel = transform.Find("XR Interaction Setup/XR Origin (XR Rig)/Locomotion System/Teleportation").GetComponent<TeleportationProvider>();

        Init(_currentStep);
    }

    public void Init(int CurrentStep)
    {
        if (_currentStep < spawnPoint.Length)
        {
            SetPos(spawnPoint[CurrentStep]);
        }
        Respawn();
    }
    public void SetPos(Transform spawn)
    {
        telPos = new TeleportRequest()
        {
            destinationPosition = spawn.position,
            destinationRotation = spawn.rotation
        };

    }

    public void NextPos(int index)
    {               
        if (index < spawnPoint.Length)
        {
            SetPos(spawnPoint[index]);
        }
    }
    public void Respawn()
    {
        tel.QueueTeleportRequest(telPos);
    }

}

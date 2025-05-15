using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScRespawn : MonoBehaviour
{
    [Header("스폰 위치")]
    [SerializeField] private Transform[] spawnPoint;
    private Transform playerTrans;
    public Transform PlayerTrans => playerTrans;
    private int _currentStep = 0;


    //TeleportationProvider tel;
    TeleportRequest telPos;
    private CharacterController characterController;
    public CharacterController CharacterController => characterController;

    public int SpawnCount => spawnPoint.Length;

    private void Start()
    {
        _currentStep = ScChapter1.Instance.CurrentSetp;
        playerTrans = GameObject.Find("Player").transform;
        ////XR Interaction Setup/XR Origin (XR Rig)/
        ////Find 할때 현재 오브젝트의 자식들 중에서부터 찾아야함
        //tel = transform.Find("Locomotion System/Teleportation").GetComponent<TeleportationProvider>();

        characterController = GetComponent<CharacterController>();

        Init(_currentStep);
        Respawn(false);
    }



    public void Init(int CurrentStep)
    {
        if (CurrentStep < spawnPoint.Length)
        {
            SetPos(spawnPoint[CurrentStep]);
        }
    }
    public void SetPos(Transform spawn)
    {
        telPos = new TeleportRequest()
        {
            destinationPosition = spawn.position,
            destinationRotation = spawn.rotation
        };
    }

    public void Respawn(bool moveScene)
    {
        characterController.enabled = false;

        playerTrans.SetPositionAndRotation(telPos.destinationPosition, telPos.destinationRotation);
        transform.SetPositionAndRotation(telPos.destinationPosition, telPos.destinationRotation);
        //tel.QueueTeleportRequest(telPos);
        
        ScChapter1.Instance.lookAroundMissionClear = false;

        characterController.enabled = true;

        Manager.Instance.GameMgr.canMove = true;

        //if (moveScene)
        //{
        //    Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Ch1Login);
        //}
    }

}

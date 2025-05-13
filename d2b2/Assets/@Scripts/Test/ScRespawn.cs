using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScRespawn : MonoBehaviour
{
    [Header("스폰 위치")]
    [SerializeField] private Transform[] spawnPoint;
    private Transform PlayerTrans;
    private int _currentStep = 0;
    
    TeleportationProvider tel;
    TeleportRequest telPos;
    //Vector3 telPos = new Vector3();

    public int SpawnCount => spawnPoint.Length;

    private void Start()
    {
        _currentStep = ScChapter1.Instance.CurrentSetp = 0;
        //PlayerTrans = GetComponent<ScPlayer>().XrOriginTrans;
        //PlayerTrans = GameObject.Find("Player").transform;
        ////XR Interaction Setup/XR Origin (XR Rig)/
        ////Find 할때 현재 오브젝트의 자식들 중에서부터 찾아야함
        tel = transform.Find("Locomotion System/Teleportation").GetComponent<TeleportationProvider>();

        
        Init(_currentStep);
        Respawn();
    }


     
    public void Init(int CurrentStep)
    {
        //Debug.Log(spawnPoint.Length);
        if (CurrentStep < spawnPoint.Length)
        {
            //Debug.Log("pos index : " + CurrentStep);
            SetPos(spawnPoint[CurrentStep]);
        }
    }
    public void SetPos(Transform spawn)
    {
        //telPos = spawn.position;
        telPos = new TeleportRequest()
        {
            destinationPosition = spawn.position,
            destinationRotation = spawn.rotation
        };

    }

    public void Respawn()
    {
        //Debug.Log("2-1");
        //tel.QueueTeleportRequest(telPos);
        transform.SetPositionAndRotation(telPos.destinationPosition, telPos.destinationRotation);

        Manager.Instance.GameMgr.canMove = true;

        //transform.position = PlayerTrans.position;
        //transform.position = telPos.destinationPosition;
        //PlayerTrans.position = telPos.destinationPosition;
        //PlayerTrans.rotation = telPos.destinationRotation;
    }

}

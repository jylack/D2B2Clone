using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScRespawn : MonoBehaviour
{
    private static ScRespawn instance;

    [Header("스폰 위치")]
    [SerializeField] private Transform[] spawnPoint;
    //[SerializeField] private GameObject Npc;

    private ScPlayer Player;

    private int _currentStep = 0;

    public static ScRespawn Instance => instance;


    TeleportRequest telPos;


    public int SpawnCount => spawnPoint.Length;

    private void Awake()
    {
        if(Instance == null)
            instance = this;
    }

    private void Start()
    {
        Player = GameObject.Find("Player").GetComponent<ScPlayer>();
        _currentStep = Ch1_Step.CurrentSetp;

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
        Player.CharacterController.enabled = false;
        //player
        Player.transform.SetPositionAndRotation(telPos.destinationPosition, telPos.destinationRotation);
        //xrOrigin
        Player.CharacterController.transform.SetPositionAndRotation(telPos.destinationPosition, telPos.destinationRotation);

        ScChapter1.Instance.lookAroundMissionClear = false;

        Player.CharacterController.enabled = true;

        Manager.Instance.GameMgr.canMove = true;

    }

}

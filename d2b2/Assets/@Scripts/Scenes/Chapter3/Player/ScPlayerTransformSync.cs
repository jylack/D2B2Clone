using Photon.Pun;
using UnityEngine;

// Assets/Resources/Prefabs/Ch3Player.prefab
public class ScPlayerTransformSync : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private GameObject mainCamera;
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerLController;
    [SerializeField] private Transform playerRController;
    [Header("Other Player")]
    [SerializeField] private GameObject otherPlayer;
    [SerializeField] private Transform otherLController;
    [SerializeField] private Transform otherRController;

    private Vector3 masterHeadPosition;
    private Quaternion masterHeadRotation;
    private GameObject head;
    private Vector3 headPosition;
    private Quaternion headRotation;
    private Vector3 playerLPosition;
    private Quaternion playerLRotation;
    private Vector3 playerRPosition;
    private Quaternion playerRRotation;
    
    public float lerpSpeed = 10f;
    
    
    
    private void Start()
    {
        // GameObject가 자신인 경우
        if (photonView.IsMine)
        {
            player.SetActive(true);
            Destroy(otherPlayer);

            Manager.Instance.GameMgr.SetCurrentPlayerMoveSpeed(0);
        }
        // GameObject가 다른 유저인 경우
        else
        {
            object[] data = photonView.InstantiationData;
            ScDefine.ScGuideCharacter characterType = (ScDefine.ScGuideCharacter)data[0];
            GameObject prefab = Manager.Instance.ResourceMgr.GetCharacterHeadPrefab(characterType);
            
            head = Instantiate(prefab, otherPlayer.transform);
            head.transform.localPosition = Vector3.zero;
            head.transform.localRotation = Quaternion.identity;

            otherLController.localScale = 1.5f * Vector3.one;
            otherRController.localScale = 1.5f * Vector3.one;
            
            Destroy(player);
            otherPlayer.SetActive(true);
        }
        
        GameObject playerParent = GameObject.Find("Players");
        if (playerParent != null)
            transform.SetParent(playerParent.transform);
    }

    private void Update()
    {
        // GameObject가 자신인 경우, 전송할 정보 저장
        if (photonView.IsMine)
        {
            masterHeadPosition = mainCamera.transform.position;
            masterHeadRotation = mainCamera.transform.rotation;
        }
        // GameObject가 다른 유저인 경우, 컨트롤러 위치 적용
        else
        {
            if (head != null)
            {
                head.transform.position = Vector3.Lerp(head.transform.position, headPosition, Time.deltaTime * lerpSpeed);
                head.transform.rotation = Quaternion.Lerp(head.transform.rotation, headRotation, Time.deltaTime * lerpSpeed);
            }
            
            if (otherLController != null)
            {
                otherLController.position = Vector3.Lerp(otherLController.position, playerLPosition, Time.deltaTime * lerpSpeed);
                otherLController.rotation = Quaternion.Lerp(otherLController.rotation, playerLRotation, Time.deltaTime * lerpSpeed);    
            }

            if (otherRController != null)
            {
                otherRController.position = Vector3.Lerp(otherRController.position, playerRPosition, Time.deltaTime * lerpSpeed);
                otherRController.rotation = Quaternion.Lerp(otherRController.rotation, playerRRotation, Time.deltaTime * lerpSpeed);    
            }
        }
    }
    
    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(masterHeadPosition);
            stream.SendNext(masterHeadRotation);
            stream.SendNext(playerLController.position);
            stream.SendNext(playerLController.rotation);
            stream.SendNext(playerRController.position);
            stream.SendNext(playerRController.rotation);
        }
        else
        {
            headPosition = (Vector3)stream.ReceiveNext();
            headRotation = (Quaternion)stream.ReceiveNext();
            playerLPosition = (Vector3)stream.ReceiveNext();
            playerLRotation = (Quaternion)stream.ReceiveNext();
            playerRPosition = (Vector3)stream.ReceiveNext();
            playerRRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}

using Photon.Pun;
using UnityEngine;

public class ScPlayerTransformSync : MonoBehaviourPun, IPunObservable
{
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerLController;
    [SerializeField] private Transform playerRController;
    [Header("Other Player")]
    [SerializeField] private GameObject otherPlayer;
    [SerializeField] private Transform otherLController;
    [SerializeField] private Transform otherRController;

    private Vector3 playerLPosition;
    private Quaternion playerLRotation;
    private Vector3 playerRPosition;
    private Quaternion playerRRotation;
    
    public float lerpSpeed = 10f;
    
    
    
    private void Start()
    {
        if (photonView.IsMine)
        {
            player.SetActive(true);
            Destroy(otherPlayer);
        }
        else
        {
            Destroy(player);
            otherPlayer.SetActive(true);
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
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
            stream.SendNext(playerLController.position);
            stream.SendNext(playerLController.rotation);
            stream.SendNext(playerRController.position);
            stream.SendNext(playerRController.rotation);
        }
        else
        {
            playerLPosition = (Vector3)stream.ReceiveNext();
            playerLRotation = (Quaternion)stream.ReceiveNext();
            playerRPosition = (Vector3)stream.ReceiveNext();
            playerRRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}

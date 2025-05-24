using Photon.Pun;
using UnityEngine;

public class ScPlayerTransformSync : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject otherPlayer;
    [SerializeField] private Transform playerLController;
    [SerializeField] private Transform playerRController;
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
            otherPlayer.SetActive(false);
        }
        else
        {
            player.SetActive(false);
            otherPlayer.SetActive(true);
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            otherLController.position = Vector3.Lerp(otherLController.position, playerLPosition, Time.deltaTime * lerpSpeed);
            otherLController.rotation = Quaternion.Lerp(otherLController.rotation, playerLRotation, Time.deltaTime * lerpSpeed);
            otherRController.position = Vector3.Lerp(otherRController.position, playerRPosition, Time.deltaTime * lerpSpeed);
            otherRController.rotation = Quaternion.Lerp(otherRController.rotation, playerRRotation, Time.deltaTime * lerpSpeed);
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

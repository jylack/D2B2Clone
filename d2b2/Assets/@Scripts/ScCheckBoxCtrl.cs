using UnityEngine;

//행동권환 활성화 해줄 클래스
public class ScCheckBoxCtrl : MonoBehaviour
{
    [SerializeField] ScTrafficCtrl trafficCtrl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (trafficCtrl != null)
            {

            }
        }
    }



}

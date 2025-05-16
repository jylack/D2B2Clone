using System.Collections;
using UnityEngine;

//행동권환 활성화 해줄 클래스
public class ScCheckBoxCtrl : MonoBehaviour
{
    [SerializeField] private ScLookAroundRegion Look;

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            if(Look.lookAroundMissionClear == false)
            {
                Debug.Log("checkbox");
                other.gameObject.GetComponent<ScRespawn>().Respawn(false);
            }
            else
            {
                ScChapter1.Instance.lookAroundMissionClear = Look.lookAroundMissionClear;
            }
        }
    }

}



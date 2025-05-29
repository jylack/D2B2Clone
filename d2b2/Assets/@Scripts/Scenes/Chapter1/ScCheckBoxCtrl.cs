using UnityEngine;

//행동권환 활성화 해줄 클래스
public class ScCheckBoxCtrl : MonoBehaviour
{
    [SerializeField] private ScLookAroundRegion Look;

    private bool isMove = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            Manager.Instance.GameMgr.OnPlayerMoving += OnMoving;

            if (ScMissionListPanel.Instance != null)
            {
                ScMissionListPanel.Instance.CheckMission(Chapter.Ch1, 1, false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            if (isMove == false)
            {
                if (ScMissionListPanel.Instance != null)
                {
                    ScMissionListPanel.Instance.CheckMission(Chapter.Ch1, 1, true);
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            ScChapter1.Instance.lookAroundMissionClear = Look.lookAroundMissionClear;
            Manager.Instance.GameMgr.OnPlayerMoving -= OnMoving;
        }
    }

    private void OnMoving(bool isMoving)
    {
        isMove = isMoving;
        Debug.Log("이동중");
    }

}



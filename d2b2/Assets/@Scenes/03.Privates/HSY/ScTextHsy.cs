using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScTextHsy : MonoBehaviour
{
    public static Dictionary<string, string> DialogueMap = new Dictionary<string, string>()
    {
        { "GsStart1", "괜찮아. 누구나 실수할 수 있는 거야." },
        { "GsStart2", "방금 있었던 일이 왜 위험했는지, 같이 살펴보자!" },
        { "GsHandUp1", "아까 너는 그냥 걸어가기만 했지? 손은 안 들었더라." },
        { "GsHandUp2", "손을 안 들면, 차에 있는 사람들이 널 못 보고 차가 그냥 지나갈 수도 있어!" },
        { "GsHandUp3", "그럼 아야하겠지? 그래서 조심해야 해!" },
        { "GsHandUp4", "차에 있는 사람들은, 앞이나 옆이 잘 안 보일 때도 있어." },
        { "GsHandUp5", "그래서 우리가 먼저 손을 들어서 알려주는 게 좋아." },
        { "GsHandUp6", "봐봐, 손도 안 들고 그냥 건너고 있지?" },
        { "GsHandUp7", "손을 안 들면, 운전자도 못 보고 멈추기 어려울 수 있어." },
        { "GsHandUp8", "그럼, 이제 어떻게 하면 되는지 같이 볼까?" },
        { "GsHandUp9", "이 친구는 건널 때 손을 들고 있지?" },
        { "GsHandUp10", "그럼 차에 있는 사람들도 금방 알아볼 수 있어서 안전하게 건널 수 있어!" },
        { "GsEnd1", "어때? 이제 어떻게 하면 좋은지 알겠지?" },
        { "GsEnd2", "다시 한번 도로로 돌아가서 다시 도전하자!" },
        { "GsLookAround1", "아까 너는 초록불이 켜지자마자, 아무 것도 안 보고 그냥 건넜어." },
        { "GsLookAround2", "가끔은 차가 신호를 놓치고 그냥 지나갈 수도 있어. 그래서 우리가 먼저 확인해야 해." },
        { "GsLookAround3", "신호가 바뀌었어도, 옆에서 오는 차가 있을 수 있어." },
        { "GsLookAround4", "그래서 꼭, 차가 멈췄는지 먼저 보고 건너야 해." },
        { "GsLookAround5", "봐봐, 이 친구는 초록불이 켜지자마자 그냥 건너고 있어." },
        { "GsLookAround6", "깜짝이야! 진짜 놀랐어! 이렇게 초록불인데도 오는 차가 있을 수 있어!" },
        { "GsLookAround7", "그럼, 이제 어떻게 하면 되는지 같이 볼까?”" },
        { "GsLookAround8", "먼저 좌우를 살펴보고...”" },
        { "GsLookAround9", "봐봐! 이렇게 하면 진짜 안전하게 건널 수 있어!" }
    };
}

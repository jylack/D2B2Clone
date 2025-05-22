using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScTextHsy : MonoBehaviour
{
    public static readonly Dictionary<int, string> DialogueMap = new()
    {
        { 0, "안녕하세요. 여기는 시작 지점입니다." },
        { 1, "왼쪽으로 이동하세요." },
        { 2, "장애물을 피해서 전진하세요." },
        { 3, "목적지에 도착했습니다. 수고하셨습니다." }
    };
}

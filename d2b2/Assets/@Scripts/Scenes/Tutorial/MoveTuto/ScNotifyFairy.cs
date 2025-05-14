using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScNotifyFairy : MonoBehaviour
{
    [SerializeField][Tooltip("Player속 MainCamera 넣어야함")] private GameObject player;
    [SerializeField] TMP_Text text;
    float startHeight;
    [SerializeField] float floatingRange;
    [SerializeField] float floatingSpeed;
    int direction = 1;

    private void Start()
    {
        startHeight = transform.position.y;
    }

    private void FixedUpdate()
    {
        transform.LookAt(player.transform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        if(startHeight + floatingRange >= transform.position.y)
        {
            direction = -1;
        }
        else if(startHeight - floatingRange <= transform.position.y)
        {
            direction = 1;
        }

        transform.Translate(0, floatingSpeed * direction * Time.deltaTime, 0);
    }

    public void ChangeText(string content)
    {
        text.text = content;
    }
}

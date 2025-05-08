using TMPro;
using UnityEngine;

public class RayDetectMan : MonoBehaviour
{
    [SerializeField] private TMP_Text countText;

    private Outline outline;
    private int count;



    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        countText.text = count.ToString();
    }



    public void SetOutlineVisible(bool isVisible)
    {
        print($"SetOutlineVisible: {isVisible}");
        outline.enabled = isVisible;
    }

    public void DoSomething()
    {
        countText.text = (++count).ToString();
    }
}

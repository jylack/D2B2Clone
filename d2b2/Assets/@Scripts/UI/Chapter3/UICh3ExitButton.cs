using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICh3ExitButton : ScObjectBase, IPointerDownHandler, IPointerUpHandler
{
    private const float HOLD_THRESHOLD = 3f;

    [SerializeField] private TMP_Text exitText;
    [SerializeField] private Image imgPressed;

    private bool isHolding;
    private float holdTime;



    private void Update()
    {
        if (isHolding)
        {
            holdTime += Time.deltaTime;
            imgPressed.fillAmount = holdTime / HOLD_THRESHOLD;

            if (holdTime >= HOLD_THRESHOLD)
            {
                isHolding = false;
                OnHoldComplete();
            }
        }
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        holdTime = 0f;
        imgPressed.fillAmount = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        holdTime = 0f;
        imgPressed.fillAmount = 0f;
    }



    private void OnHoldComplete()
    {

    }
}

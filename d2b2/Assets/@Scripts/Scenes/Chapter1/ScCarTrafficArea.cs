using UnityEngine;

public class ScCarTrafficArea : MonoBehaviour
{
    private ScTrafficCtrl scTrafficCtrl;

    private void Awake()
    {
        scTrafficCtrl = transform.parent.GetComponent<ScTrafficCtrl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.layer);

        if (other.gameObject.layer == ScDefine.Layer.CarIndex)
        {
            //  Debug.Log(scTrafficCtrl.CurrentColor.ToString());

            if (scTrafficCtrl.CurrentColor == TrafficLightColor.Green)
            {
                other.gameObject.GetComponent<ScCarController>().StopCar();
                //Debug.Log("IsBlink : " + scTrafficCtrl.IsBlink);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.CarIndex)
        {
            if (scTrafficCtrl.CurrentColor == TrafficLightColor.Red)
            {
                other.gameObject.GetComponent<ScCarController>().MoveCar();
            }

            if (scTrafficCtrl.IsBlink)
            {
                other.gameObject.GetComponent<ScCarController>().StopCar();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScCarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject headLightCar;
    private ScCarController carController;
    [SerializeField] private int createTime = 10;

    public void Start()
    {
        var tempCar = Instantiate(headLightCar, transform.position, transform.rotation);
        carController = tempCar.GetComponent<ScCarController>();
        carController.gameObject.SetActive(false);
        StartCoroutine(CreateHeadLightCar());
    }
    IEnumerator CreateHeadLightCar()
    {
        while (true)
        {
            carController.gameObject.transform.position = transform.position;
            carController.gameObject.SetActive(true);
            yield return new WaitUntil(() => carController.isActive == false);
            yield return new WaitForSeconds(createTime);
        }
    }
}

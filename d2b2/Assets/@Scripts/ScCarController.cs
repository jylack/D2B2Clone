using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScCarController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float carSpeed = 5;
    public bool isActive { get; private set; }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void MoveCar()
    {
        rb.velocity = Vector3.forward * carSpeed;
        Debug.Log("차량 속도 " + rb.velocity.z);
    }
    private void OnEnable()
    {
        Debug.Log("차량 이동 시작");
        isActive = true;
        MoveCar();
    }
    private void OnDisable()
    {
        isActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}

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

    public void MoveCar()
    {
        rb.velocity = transform.right * carSpeed;
        Debug.Log("차량 속도 " + rb.velocity.z);
    }
    public void StopCar()
    {
        rb.velocity = Vector3.zero;
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
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.WallIndex)
        {
            gameObject.SetActive(false);
        }
        
    }
}

using UnityEngine;

public class ScCar : ScObjectBase
{
    private static int numbering;

    [SerializeField] private Vector3 boxcastCenterDistance;
    [SerializeField] private Vector3 boxCastSize = Vector3.one;

    private Vector3 dir = Vector3.forward;
    private float moveSpeed;
    private Rigidbody rb;
    private Vector3 originPosition;
    private float destinationDistance;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.name = $"Car_{numbering++}";
        originPosition = transform.position;
    }

    private void Start()
    {
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs((transform.position - originPosition).magnitude) > destinationDistance)
        {
            Destroy(gameObject);
            return;
        }
        
        RaycastHit[] hitInfos = Physics.BoxCastAll(GetBoxcastPosition(), boxCastSize / 2, transform.forward, transform.rotation, 0f);

        if (hitInfos.Length > 0)
        {
            foreach (RaycastHit hitInfo in hitInfos)
            {
                if (hitInfo.collider.gameObject == gameObject)
                    continue;

                rb.velocity = Vector3.zero;
                return;
            }
        }

        rb.velocity = dir.normalized * moveSpeed;
    }

    // Boxcast 범위 확인용
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(GetBoxcastPosition(), boxCastSize);
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.gameObject.name);
    //}



    public void Init(float speed, Vector3 direction, float destDistance)
    {
        moveSpeed = speed;
        dir = direction;
        destinationDistance = destDistance;
    }



    private Vector3 GetBoxcastPosition()
    {
        return transform.position + transform.localRotation * boxcastCenterDistance;
    }
}

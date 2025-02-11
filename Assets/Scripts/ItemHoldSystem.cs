using UnityEngine;

public class ItemHoldSystem : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, throwForce = 20f, lerpSpeed = 10f;
    [SerializeField] Transform objectHolder;
    
    Rigidbody grabbedRb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbedRb)
        {
            grabbedRb.MovePosition(Vector3.Lerp(grabbedRb.position, objectHolder.transform.position, Time.deltaTime * lerpSpeed));

            if (Input.GetMouseButtonDown(1))
            {
                grabbedRb.isKinematic = false;
                grabbedRb.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
                grabbedRb = null;
            }
        }
        
        if (Input.GetKey(KeyCode.F))
        {
            if (grabbedRb)
            {
                grabbedRb.isKinematic = false;
                grabbedRb = null;
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if (Physics.Raycast(ray, out hit, maxGrabDistance))
                {
                    grabbedRb = hit.collider.gameObject.GetComponent<Rigidbody>();
                    if (grabbedRb)
                    {
                        grabbedRb.isKinematic = true;
                    }
                }
            }
        }
    }
}

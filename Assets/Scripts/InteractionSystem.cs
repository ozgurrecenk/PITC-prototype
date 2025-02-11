using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class InteractionSystem : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, InteractRange))
            {
                Transform target = hit.collider.transform;
                
                IInteractable interactObj = target.GetComponentInParent<IInteractable>();
                
                if (interactObj != null)
                {
                    interactObj.Interact();
                    Debug.Log(gameObject.name);
                }
            }
        }
    }
}
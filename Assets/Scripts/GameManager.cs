using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] prefabs;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && Input.GetKey(KeyCode.Alpha1))
        {
            Instantiate(prefabs[1], transform.position, Quaternion.identity);
        }
    }
}

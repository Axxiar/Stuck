using UnityEngine;
 
public class Follow : MonoBehaviour
{
    public Transform target;
 
     
    void Update()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
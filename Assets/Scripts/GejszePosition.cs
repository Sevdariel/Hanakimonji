using UnityEngine;

public class GejszePosition : MonoBehaviour
{
    public float width, height;

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }
}

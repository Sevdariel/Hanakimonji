using UnityEngine;

public class ActionPosition : MonoBehaviour
{
    public GameObject[] actionPrefab;
    public float width, height;
    // Start is called before the first frame update
    void Start()
    {
        SpawnActions();
    }

    private void SpawnActions()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition.name == "SecretAction")
            
        Debug.Log(freePosition.name);
    }

    private Transform NextFreePosition()
    {
        foreach(Transform childPositionGameObject in transform)
            if (childPositionGameObject.childCount == 0)
                return childPositionGameObject;
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }
}

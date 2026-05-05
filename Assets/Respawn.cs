using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float threshold;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y < threshold)
        {
            transform.position = new Vector3(-11.67f, -6.07f, 0f);
        }
    }
}

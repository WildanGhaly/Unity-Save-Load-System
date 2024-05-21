using UnityEngine;

public class PillarMovement : MonoBehaviour
{
    private readonly float destroyDelay = 10f;
    private readonly float speed = 3f;
    private Vector3 position;

    private void Start()
    {
        position = transform.position;
        Destroy(gameObject, destroyDelay);
    }

    void FixedUpdate()
    {
        position.x -= speed * Time.deltaTime;
        transform.localPosition = position;
    }
}

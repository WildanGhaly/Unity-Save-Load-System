using UnityEngine;

public class PillarMovement : MonoBehaviour
{
    private readonly float destroyDelay = 12f;
    private readonly float speed = 3f;
    private Vector3 position;

    private void Start()
    {
        position = transform.localPosition;
        Destroy(gameObject, destroyDelay);
    }

    void Update()
    {
        position.x -= speed * Time.deltaTime;
        transform.localPosition = position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayable : MonoBehaviour
{
    private new Transform transform;
    private new Rigidbody2D rigidbody;

    [SerializeField] private float jumpForce = 5.0f;

    void Awake()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnDisable()
    {
        transform.localScale = new Vector3(8.75f, 8.75f, 1);
        transform.localPosition = new Vector3(-4, 0, 0);
        transform.localRotation = Quaternion.identity;
    }

    public void Jump()
    {
        if (rigidbody != null && rigidbody.simulated)
        {
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}

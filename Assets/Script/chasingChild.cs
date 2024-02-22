using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasingChild : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5.0f;
    private Rigidbody rbChild;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        rbChild = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        movement = direction;
    }

    void moveCharacter(Vector3 direction)
    {
        rbChild.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
    }

   private void FixedUpdate()
    {
        moveCharacter(movement);
    }
}

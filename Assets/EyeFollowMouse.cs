using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollowMouse : MonoBehaviour
{
    Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    Rigidbody2D rb;
    Vector2 position = new Vector2 (0, 0);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        mousePosition=Input.mousePosition;
        mousePosition=Camera.main.ScreenToWorldPoint (mousePosition); 
        position=Vector2.Lerp(transform.position, mousePosition, moveSpeed);

    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }
}
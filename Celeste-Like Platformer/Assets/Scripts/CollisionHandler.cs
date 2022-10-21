using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    //public variables
    public bool isGrounded;
    
    [Space]

    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    [Header("Collider settings")]
    [SerializeField] private float collisionRadius = 0.25f;
    [SerializeField] private Vector2 bottomOffset;
    private Color debugCollisionColor = Color.red;
   // Start is called before the first frame update
   void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle((Vector2) transform.position + bottomOffset, collisionRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = debugCollisionColor;

        //grounded gizmo
        if (isGrounded) { Gizmos.color = Color.green; }
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);

    }
}

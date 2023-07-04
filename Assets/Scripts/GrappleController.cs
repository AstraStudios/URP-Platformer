using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// overhauled version of original code
public class GrappleController : MonoBehaviour
{
    [Header("Player Related:")]
    [SerializeField] GameObject player;
    Rigidbody2D rb;
    float facingAngle = 0f;

    [Header("Line Firing:")]
    [SerializeField] GameObject grapplePoint;
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject grapplePointParent;
    [SerializeField] float grappleSpeed = 10f;
    [SerializeField] float maxDistance = 20f;
    [SerializeField] float retractSpeed = 10f;
    RaycastHit2D hit;
    bool isGrappling = false;
    private Vector2 currentVelocity;
    private Vector2 origFirePoint;

    [Header("Line Rendering:")]
    [SerializeField] Material lineMat;
    LineRenderer lineRenderer;

    void Start()
    {
        rb = gameObject.GetComponentInParent<Rigidbody2D>();

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .1f;
        lineRenderer.material = lineMat;
    }

    void Update()
    {
        FacePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetMouseButtonDown(0))
            FireGrapple();

        if (isGrappling)
        {
            lineRenderer.enabled = true;

            lineRenderer.SetPosition(0, firePoint.transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }
    }

    void FireGrapple()
    {
        hit = Physics2D.Raycast(firePoint.transform.position, -firePoint.transform.right);

        if (!hit.collider.CompareTag("Ground")) return;

        isGrappling = true;

        // launch twords point
        currentVelocity = (hit.point - (Vector2)firePoint.transform.position).normalized * grappleSpeed; 
        rb.velocity = (currentVelocity);
    }

    /////// UTILS

    void FacePosition(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        facingAngle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, facingAngle + 180);
    }
}

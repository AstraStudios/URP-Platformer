using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// overhauled version of original code
public class GrappleController : MonoBehaviour
{
    [Header("Line Firing:")]
    [SerializeField] GameObject firePoint;
    [SerializeField] float grappleSpeed = 10f;
    [HideInInspector] public bool isGrappling = false;

    [Header("Line Rendering:")]
    [SerializeField] Material lineMat;
    [SerializeField] ParticleSystem fireParticles;

    Rigidbody2D rb;
    LineRenderer lineRenderer;
    RaycastHit2D hit;

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

        else if (!Input.GetMouseButton(0))
            isGrappling = false;


        if (isGrappling)
        {
            lineRenderer.enabled = true;

            lineRenderer.SetPosition(0, firePoint.transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
            lineRenderer.enabled = false;
    }

    void FireGrapple()
    {
        RaycastHit2D newhit = Physics2D.Raycast(firePoint.transform.position, -firePoint.transform.right);

        if (!newhit.collider.CompareTag("Ground")) return;

        hit = newhit;

        isGrappling = true;
        ParticleSystem firedParticles = Instantiate(fireParticles, firePoint.transform.position, firePoint.transform.rotation);
        firedParticles.Play();

        // launch twords point
        rb.velocity = ((hit.point - (Vector2)firePoint.transform.position).normalized * grappleSpeed);
    }

    /////// UTILS

    void FacePosition(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        float facingAngle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, facingAngle + 180);
    }
}

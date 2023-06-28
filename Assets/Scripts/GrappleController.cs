using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// overhauled version of original code
public class GrappleController : MonoBehaviour
{
    [Header("Player Related:")]
    [SerializeField] GameObject player;
    Rigidbody2D rb2D;
    float facingAngle = 0f;

    [Header("Line Firing:")]
    [SerializeField] GameObject grapplePoint;
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject grapplePointParent;
    [SerializeField] float grappleSpeed = 10f;
    [SerializeField] float maxDistance = 20f;
    [SerializeField] float retractSpeed = 10f;
    RaycastHit2D hit;
    bool grappleActive = false;
    bool isGrappling = false;
    private Vector2 currentVelocity;
    private Vector2 origFirePoint;

    [Header("Line Rendering:")]
    [SerializeField] Material lineMat;
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponentInParent<Rigidbody2D>();

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .1f;
        lineRenderer.material = lineMat;
    }

    // Update is called once per frame
    void Update()
    {
        FacePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (isGrappling)
            RetractHook();
        else
            FireGrapple();
    }

    void FireGrapple()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(firePoint.transform.position, -firePoint.transform.right);
            origFirePoint = firePoint.transform.position;

            if (hit.collider.CompareTag("Ground"))
            {
                GameObject lastSpawned = Instantiate(grapplePoint, hit.transform.position, Quaternion.identity);
                lastSpawned.transform.parent = grapplePointParent.transform;
                grappleActive = true;
                currentVelocity = (hit.point - origFirePoint).normalized * grappleSpeed;
                rb2D.velocity = currentVelocity;
            }
        }
        if (grappleActive && hit.collider.CompareTag("Ground"))
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, firePoint.transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }
    }

    void RetractHook()
    {
        float distance = Vector2.Distance(firePoint.transform.position, hit.transform.position);
        if (distance >= maxDistance)
        {
            isGrappling = false;
            rb2D.velocity = Vector2.zero;
            return;
        }
        //currentVelocity = (origFirePoint =- hit.transform.position).normalized * retractSpeed;
        //rb2D.velocity = currentVelocity;
        //rb2D.position += currentVelocity * Time.deltaTime;

        Vector2 direction = (origFirePoint =- hit.transform.position).normalized;
        Vector2 targetPosition = rb2D.position + direction * retractSpeed * Time.deltaTime;
        rb2D.MovePosition(targetPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrappling = true;
        rb2D.velocity = Vector2.zero;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrappling = false;
    }

    void FacePosition(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        facingAngle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, facingAngle + 180);
    }
}

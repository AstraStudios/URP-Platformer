using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleController : MonoBehaviour
{
    float facingAngle = 0f;

    [SerializeField] GameObject grapplePoint;
    [SerializeField] GameObject firePoint;
    LineRenderer lineRenderer;
    RaycastHit2D hit;
    bool grappleActive = false;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .1f;
    }

    // Update is called once per frame
    void Update()
    {
        FacePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(firePoint.transform.position, -firePoint.transform.right);

            if (hit.collider.CompareTag("Ground"))
            {
                Instantiate(grapplePoint, hit.transform.position, Quaternion.identity);
            }

            // draw a line
            grappleActive = true;
        }
        if (grappleActive)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, firePoint.transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        if (Input.GetMouseButtonDown(1))
        {
            lineRenderer.enabled = false;
            grappleActive = false;
        }
    }

    void FacePosition(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        facingAngle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, facingAngle + 180);
    }

    void FireGrapple()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, -firePoint.transform.right);

        if (hit.collider.CompareTag("Ground"))
        {
            Instantiate(grapplePoint, hit.transform.position, Quaternion.identity);
        }

        // draw a line
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePoint.transform.position);
        lineRenderer.SetPosition(1, hit.point);
    }
}

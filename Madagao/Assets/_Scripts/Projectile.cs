using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float maxDistance = 20f;
    [SerializeField] float projSpeed = 5f;

    Vector3 origin;
    Rigidbody rb;

    private Vector3 m_vertical;
    private Vector3 m_horizontal;

    private void Start()
    {
        origin = transform.position;
        rb = GetComponent<Rigidbody>();
       

        m_vertical = Camera.main.transform.forward;
        m_vertical.y = 0;
        m_vertical = Vector3.Normalize(m_vertical);

        //Gets the horizontal direction relative to the camera using the vertical vector
        m_horizontal = Quaternion.Euler(new Vector3(0, 90, 0)) * m_vertical;
        m_horizontal = Vector3.Normalize(m_horizontal);

        MoveProjectile();
    }

    // Update is called once per frame
    void Update ()
    {
        float distance = Vector3.Distance(origin, transform.position);
        if (distance >= maxDistance)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void MoveProjectile()
    {
        var heading = Camera.main.ScreenToViewportPoint(Input.mousePosition) - transform.position;
        heading.Normalize();
        Vector3 vMovement = m_vertical * Time.deltaTime * projSpeed;
        Vector3 hMovement = m_horizontal * Time.deltaTime * projSpeed;
        Vector3 movement = (hMovement + vMovement) + heading;
        rb.AddForce(movement * projSpeed);
    }


}

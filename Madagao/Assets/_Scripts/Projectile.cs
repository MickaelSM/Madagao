using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float maxDistance = 20f;
    [SerializeField] float projSpeed = 1f;
    
    Vector3 origin;
    private Vector3 m_vertical;
    private Vector3 m_horizontal;
    private Vector3 direction;

    private void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        float distance = Vector3.Distance(origin, transform.position);
        if (distance >= maxDistance)
        {
            GameObject.Destroy(this.gameObject);
        }
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        
        Vector3 movement = transform.forward * projSpeed * Time.deltaTime;
        transform.position += movement;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] float maxDistance = 20f;
    [SerializeField] float projSpeed = 5f;

    private Vector3 startPosition;
    Vector3 direction;

    private void Awake()
    {
        startPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        startPosition.y = 2;
        direction = Camera.main.ScreenToViewportPoint(Input.mousePosition) - startPosition;
        direction.y = 0;
        //direction.Normalize();
        transform.position = startPosition;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    // Update is called once per frame
    void Update ()
    {
        MoveProjectile();
	}

    private void MoveProjectile()
    {
        float distance = Vector3.Distance(startPosition, transform.position);
        Vector3 movement = direction * Time.deltaTime * projSpeed;
        if (distance <= maxDistance)
        {
            transform.position += movement;
        }
        else GameObject.Destroy(this.gameObject);
    }
}

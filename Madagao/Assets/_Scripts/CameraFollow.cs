using UnityEngine;

public class CameraFollow : MonoBehaviour {

    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update ()
    {
        transform.position = player.transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArrowHitbox : MonoBehaviour
{
    CameraArrow Arrow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            Arrow.gameObject.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Arrow = GetComponentInChildren<CameraArrow>();
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, transform.position.y, 0);
    }


}

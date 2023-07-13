using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField]
    private Vector2 slowAmount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("coll");
        if (other.gameObject.name.Contains("Player"))
        {
           // Debug.Log("coll");


            Rigidbody2D _rb = other.GetComponent<Rigidbody2D>();
            if (other.GetComponent<PlayerFreeMovement>().isInvincible == false)
            {
                _rb.velocity -= (_rb.velocity / 100 * slowAmount);
            }

            /*
           GameOver gameOver = other.GetComponent<GameOver>();

            gameOver.EndGame();
            Destroy(other.gameObject);
            */
        };
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

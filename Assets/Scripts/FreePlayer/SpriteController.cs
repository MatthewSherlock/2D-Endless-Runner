using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    private float Rotation;
    private float YPos;
    
    private void UpdateRotation()
    {
        
        if (YPos < transform.position.y)
            Rotation++;
        else if (YPos > transform.position.y)
            Rotation--;
        Rotation = Mathf.Clamp(Rotation, -45, 45);
        transform.eulerAngles = new Vector3(0, 0, Rotation);
        YPos = transform.position.y;
    }

    private void Start()
    {
        SpriteRenderer playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerSpriteRenderer.sprite = DataManager._Instance._PlayerSprite;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
    }
}

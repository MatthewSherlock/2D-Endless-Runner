using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCorrector : MonoBehaviour
{
    [SerializeField]
    private float _XOffset, _YOffset, ZoomTime = 0.1f, MoveTime = 0.1f, _MaxZoom = 8f, _MaxY = 6f;
    GameObject Player;
    GameObject Hitpoint;
    [SerializeField]
    private float ZoomedCameraDistanceActivation = 5f;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Hitpoint = Player.GetComponent<PlayerFreeMovement>()._Hitpoint;
    }
    // Update is called once per frame
    
    void LateUpdate()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        Hitpoint.transform.position += new Vector3(_XOffset, _YOffset, 0);
        Vector3 PlayerPosition = new Vector3(Player.transform.position.x + _XOffset, Player.transform.position.y + _YOffset,-10);
        if (Vector3.Distance(PlayerPosition, Hitpoint.transform.position) > ZoomedCameraDistanceActivation)
        {
            Vector3 CameraPosition = (PlayerPosition + Hitpoint.transform.position) / 2;
            //transform.position = new Vector3(CameraPosition.x+_XOffset,CameraPosition.y,-10f);
            float y = Mathf.Clamp(Mathf.Lerp(transform.position.y, CameraPosition.y, MoveTime *2),-3f,_MaxY);
            float x = PlayerPosition.x;
            transform.position = new Vector3(x, y, -10f);
            //transform.position = Vector3.Lerp(transform.position, new Vector3(CameraPosition.x + _XOffset, CameraPosition.y, -10f), 0.1f);
            Camera.main.orthographicSize = Mathf.Clamp(Mathf.Lerp(Camera.main.orthographicSize, Vector3.Distance(PlayerPosition, Hitpoint.transform.position)/2,ZoomTime),0,_MaxZoom);
        }
        else
        {
            transform.position = PlayerPosition;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 5,0.1f);
        }
       
        //transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x +_XOffset, GameObject.FindGameObjectWithTag("Player").transform.position.y + _YOffset, GameObject.FindGameObjectWithTag("Player").transform.position.z-12f);
        
    }
}

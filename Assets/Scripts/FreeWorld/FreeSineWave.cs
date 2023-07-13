using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeSineWave : MonoBehaviour
{
    LineRenderer _Line;
    EdgeCollider2D _Collider;
    public Vector2 StartEnd;
    private int _Length;
    private float _Amplitude;
    private List<Vector3> _LinePositions;


    private float FloatTimer;
    private void CreateLinePositions()
    {
        _Length = Mathf.RoundToInt(StartEnd.y - StartEnd.x) * 4;

        float _X = StartEnd.x;
        _Amplitude = 1;//Random.Range(0, 11);
        float _XFinish = StartEnd.y;
        _Line.positionCount = _Length;

        for (int PointNum = _LinePositions.Count; PointNum < _Length; PointNum++)
        {
            float _Progress = (float)PointNum / (_Length - 1);
            float x = Mathf.Lerp(_X, _XFinish, _Progress);
            float y = _Amplitude*Mathf.Sin(x);
            _LinePositions.Add(new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, 0));
        }

        Collisions();
        DrawLine();
    }

    private void DrawLine()
    {
        for (int i = 0; i < _Line.positionCount; i++)
        {
            _Line.SetPosition(i, _LinePositions[i]);
        }
    }
    public void DrawLineOriginal()
    {
        
        _Length = Mathf.RoundToInt(StartEnd.y - StartEnd.x)*2;
       
            float _X = StartEnd.x;
        _Amplitude = Random.Range(0, 10);
            float _XFinish = StartEnd.y;
            _Line.positionCount = _Length;
       
            for (int PointNum = 0; PointNum < _Length; PointNum++)
            {
                float _Progress = (float)PointNum / (_Length - 1);
                float x = Mathf.Lerp(_X, _XFinish, _Progress);
                float y = _Amplitude*Mathf.Sin(x);
                _Line.SetPosition(PointNum, new Vector3(transform.localPosition.x+x, transform.localPosition.y+y, 0));
            }
        Collisions();
    }
    private void Collisions()
    {
        List<Vector2> _Edges = new List<Vector2>();

        for (int i = 0; i < _Line.positionCount; i++)
        {

            Vector3 LinePoint = _Line.GetPosition(i);
            _Edges.Add(new Vector2(LinePoint.x-transform.position.x, LinePoint.y-transform.position.y));

        }
        _Collider.SetPoints(_Edges);
    }
    private IEnumerator DeleteEnd()
    {
        yield return new WaitForSeconds(Time.deltaTime * 60);
        _LinePositions.RemoveAt(0);


    }

    // Start is called before the first frame update
    void Start()
    {
        _Line = GetComponent<LineRenderer>();
        _Collider = GetComponent<EdgeCollider2D>();
        _LinePositions = new List<Vector3>();
        CreateLinePositions();
    }

    // Update is called once per frame
    void Update()
    {
        CreateLinePositions();
        DrawLine();
        StartEnd.y += 1*(Time.deltaTime*4);
        StartEnd.x += 1 * (Time.deltaTime * 4);
        _LinePositions.RemoveAt(0);

        

    }
}

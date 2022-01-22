using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public static LineController instance;
    public bool startDrawing;

    [SerializeField] GameObject debris;
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint;

    float speed = 0.025f;
    //singleton..
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        lr = GetComponent<LineRenderer>();
    }

    LineRenderer lr;
    Vector3[] points;


    public void SetupLines(Vector3[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;

        DrawLines();

        Instantiate(startPoint, points[0], Quaternion.identity);
        Instantiate(endPoint, points[points.Length-1], Quaternion.identity);

        StartCoroutine(AnimateDebris());
    }

  /*  private void Update()
    {
        if(startDrawing)
        {
            for (int i = 0; i < points.Length; i++)
            {
                lr.SetPosition(i, points[i]);
            }
        }

    }*/

    public void DrawLines()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i]);
        }
    }

    IEnumerator AnimateDebris()
    {
        GameObject deb =  Instantiate(debris, transform.position, Quaternion.identity);

        int i = 0;

        while (true)
        {
            deb.transform.position = points[i];

            i += (int)(50 * speed);

            if (i >= points.Length)
                i = points.Length - 1;

            yield return new WaitForSeconds(0f);
        }

    }

    public void ChangeSpeed(float x)
    {
        if (x == 0)
            x = 0.025f;
        speed = x;
    }
}

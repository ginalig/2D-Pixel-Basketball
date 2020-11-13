using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public GameObject dotPrefab;
    public Transform dotsParent;
    
    public int dotsNumber;
    public float dotsSpacing;

    private Transform[] dots;
    private Vector2 dotPosition;
    private float timeStamp;

    private void Start()
    {
        Hide();
        
        PrepareDots();
    }

    private void PrepareDots()
    {
        dots = new Transform[dotsNumber];

        for (int i = 0; i < dotsNumber; i++)
        {
            dots[i] = Instantiate(dotPrefab, dotsParent).transform;
        }
    }

    public void UpdateDots(Vector3 ballPos, Vector3 forceApplied, float gravityScale)
    {
        timeStamp = dotsSpacing;
        for (int i = 0; i < dotsNumber; i++)
        {
            dotPosition.x = (ballPos.x + forceApplied.x * timeStamp);
            dotPosition.y = (ballPos.y + forceApplied.y * timeStamp) -
                            (Physics2D.gravity.magnitude * gravityScale * timeStamp * timeStamp) / 2f;

            dots[i].position = dotPosition;
            timeStamp += dotsSpacing;
        }
    }
    
    public void Show()
    {
        dotsParent.gameObject.SetActive(true);
    }

    public void Hide()
    {
        dotsParent.gameObject.SetActive(false);
    }
}

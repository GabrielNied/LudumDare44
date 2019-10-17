using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderIA : MonoBehaviour
{
    public float distanceTravelled = 0, distance = 0;
    public Vector3 movePosition, lastPosition;

    void Start()
    {
        lastPosition = this.transform.position;
        movePosition = new Vector3 (Random.Range(-10000, 10000), Random.Range(-10000, 10000), 0);
        distance = Vector3.Distance(movePosition, lastPosition);
    }

    void FixedUpdate()
    {
        if (distanceTravelled >= distance)
        {
            movePosition = new Vector3(Random.Range(-10000, 10000), Random.Range(-10000, 10000), 0);
            lastPosition = this.transform.position;
            distance = Vector3.Distance(movePosition, lastPosition);
            distanceTravelled = 0f;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, movePosition, 0.01f);
            transform.Translate(new Vector3(movePosition.x, movePosition.y, 0) * Time.deltaTime / 100000);
            distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        }

        if(this.transform.position.x > movePosition.x)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
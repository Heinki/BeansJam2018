using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyObject : MonoBehaviour {

    Vector3 originalPosition;
    GameObject ice;
    float speed;
    bool isMoving;

	void Start () {
        this.ice = GameObject.FindGameObjectWithTag("Ice");
        this.speed = 1.5f;
        originalPosition = transform.position;
        isMoving = true;
	}
	
	void Update () {
        Move();
	}

    void Move()
    {
        if(this.isMoving) {
            transform.position = Vector3.MoveTowards(transform.position, ice.transform.position, Time.deltaTime * this.speed);

            float distance = Vector3.Distance(transform.position, ice.transform.position);

            if (distance < 1)
            {
                // PUT THE METER LOWER!!!!!
            }
        }
    }

    public void ResetMotion()
    {
        this.transform.position = originalPosition;
        isMoving = true;
    }

    public void setPositionOutsideOfScreen()
    {
        transform.position = new Vector3(125, 0, 0);
        setMoving(false);
    }

    public void setMoving(bool move)
    {
        isMoving = move;
    }
}

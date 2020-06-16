using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    bool isMoving;
    float MOVE_SPEED;
    Vector2 MOVE_LOCATION;
    Vector2 MOVE_UNIT_VECTOR;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        MOVE_SPEED = 0;
        MOVE_LOCATION = Vector2.zero;
    }

    public void moveBy(Vector2 distance, float speed)
    {
        isMoving = true;
        Vector2 objectTransformIn2D = gameObject.transform.position;

        MOVE_LOCATION = objectTransformIn2D + distance;
        MOVE_UNIT_VECTOR = getUnitVector(objectTransformIn2D, MOVE_LOCATION);
        MOVE_SPEED = speed;
    }

    public void moveTo(Vector2 location, float speed)
    {
        Vector2 objectPosition2D = gameObject.transform.position;
        Vector2 moveByVector = location - objectPosition2D;
        moveBy(moveByVector, speed);
    }

    Vector2 getUnitVector(Vector2 point1, Vector2 point2)
    {
        Vector2 unitVector = new Vector2(point2.x - point1.x, point2.y - point1.y);
        float magnitude = Mathf.Sqrt(unitVector.x * unitVector.x + unitVector.y * unitVector.y);
        unitVector.x /= magnitude;
        unitVector.y /= magnitude;

        return unitVector;
    }

    float getMagnitude(Vector2 point1, Vector2 point2)
    {
        Vector2 vector = new Vector2(point2.x - point1.x, point2.y - point1.y);
        float magnitude = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
        return magnitude;
    }
    float getMagnitude(Vector2 vector)
    {
        float magnitude = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
        return magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Add check and subsequent stop for when the object is arriving at position so
        // that this doesn't go on forever.
        if (isMoving)
        {
            if (getMagnitude(gameObject.transform.position, MOVE_LOCATION) <= 1)
            {
                Vector2 objectPosition2D = gameObject.transform.position;
                Vector2 vectorToTargetPosition = MOVE_LOCATION - objectPosition2D;
                gameObject.transform.Translate(vectorToTargetPosition * Time.deltaTime * MOVE_SPEED);
                isMoving = false;
            }
            else
            {
                gameObject.transform.Translate(MOVE_UNIT_VECTOR * Time.deltaTime * MOVE_SPEED);
            }
        }

    }
}

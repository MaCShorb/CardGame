using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    bool isMoving;
    bool isScaling;
    float MOVE_SPEED;
    float SCALE_SPEED;
    Vector2 MOVE_LOCATION;
    Vector2 MOVE_UNIT_VECTOR;
    Vector2 SCALE_LOCATION;
    Vector2 SCALE_UNIT_VECTOR;
    Vector3 ORIGINAL_SCALE;
    // Start is called before the first frame update
    void Awake()
    {
        isMoving = false;
        isScaling = false;
        SCALE_SPEED = 0;
        MOVE_SPEED = 0;
        MOVE_LOCATION = Vector2.zero;
        SCALE_LOCATION = Vector2.zero;
        ORIGINAL_SCALE = gameObject.transform.localScale;
    }

    public void scaleTo(float scale, float speed)
    {
        Vector2 originalScale = gameObject.transform.localScale;
        Vector2 targetScale = new Vector2(gameObject.transform.localScale.x * scale, gameObject.transform.localScale.y * scale);
        SCALE_LOCATION = targetScale;
        SCALE_UNIT_VECTOR = getUnitVector(originalScale, targetScale);
        SCALE_SPEED = speed;
        isScaling = true;
    }

    public void scaleToOriginal(float speed)
    {
        Vector2 currentScale = gameObject.transform.localScale;
        SCALE_LOCATION = ORIGINAL_SCALE;
        SCALE_UNIT_VECTOR = getUnitVector(currentScale, ORIGINAL_SCALE);
        SCALE_SPEED = speed;
        isScaling = true;
    }

    public void setNewOriginalScale(Vector3 newOriginal)
    {
        ORIGINAL_SCALE = newOriginal;
    }

    public void moveBy(Vector2 distance, float speed)
    {
        Vector2 objectTransformIn2D = gameObject.transform.position;

        MOVE_LOCATION = objectTransformIn2D + distance;
        MOVE_UNIT_VECTOR = getUnitVector(objectTransformIn2D, MOVE_LOCATION);
        MOVE_SPEED = speed;
        isMoving = true;
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
        // TODO: Test and fix issue of cards being off of their positional marks by a small margin.
        if (isMoving)
        {
            if (getMagnitude(gameObject.transform.position, MOVE_LOCATION) <= Time.deltaTime * MOVE_SPEED)
            {
                Vector2 objectPosition2D = gameObject.transform.position;
                Vector2 vectorToTargetPosition = MOVE_LOCATION - objectPosition2D;

                gameObject.transform.Translate(vectorToTargetPosition);
                isMoving = false;
            }
            else
            { 
                gameObject.transform.Translate(MOVE_UNIT_VECTOR * Time.deltaTime * MOVE_SPEED);
            }
        }

        if(isScaling)
        {
            if(getMagnitude(gameObject.transform.localScale, SCALE_LOCATION) <= Time.deltaTime * SCALE_SPEED)
            {
                gameObject.transform.localScale = new Vector3(SCALE_LOCATION.x, SCALE_LOCATION.y, ORIGINAL_SCALE.z);
                isScaling = false;
            }
            else
            {
                Vector3 adjustVector = SCALE_UNIT_VECTOR * Time.deltaTime * SCALE_SPEED;
                adjustVector.z = 0;
                gameObject.transform.localScale += adjustVector;
            }
        }
    }
}

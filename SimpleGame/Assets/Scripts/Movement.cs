using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementTime = 2;
    public float speed = 2;
    Vector3 direction;
    bool isMoving = false;
    float startMovementTime;

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Time.time - startMovementTime > movementTime)
            isMoving = false;

        if (isMoving)
        {
            var nextPos = transform.position + (direction * Time.deltaTime);
            if (CheckFieldBorders(nextPos))
                isMoving = false;
            else
                transform.Translate(direction * Time.deltaTime * speed);
        }
        else
        {
            var dir = Random.insideUnitCircle;
            dir.Normalize();
            direction = new Vector3(dir.x, 0, dir.y);
            startMovementTime = Time.time;
            isMoving = true;
            movementTime = 2 + Random.Range(-0.5f, 0.5f);
        }
    }

    bool CheckFieldBorders(Vector3 nextPos)
    {
        if (Mathf.Abs(nextPos.x) > 100 || Mathf.Abs(nextPos.z) > 100f)
            return true;
        return false;
    }
}
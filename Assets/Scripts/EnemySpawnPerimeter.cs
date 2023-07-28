using UnityEngine;

public class EnemySpawnPerimeter
{
    private Vector3 leftBottomPoint;
    private Vector3 leftTopPoint;
    private Vector3 rightTopPoint;
    private Vector3 rightBottomPoint;

    private float leftSide;
    private float topSide;
    private float rightSide;
    private float bottomSide;

    private float perimeterLength;

    public EnemySpawnPerimeter(Transform leftBottomPoint, Transform leftTopPoint, Transform rightTopPoint, Transform rightBottomPoint)
    {
        this.leftBottomPoint = leftBottomPoint.position;
        this.leftTopPoint = leftTopPoint.position;
        this.rightTopPoint = rightTopPoint.position;
        this.rightBottomPoint = rightBottomPoint.position;

        leftSide = Vector3.Distance(this.leftTopPoint, this.leftBottomPoint);
        topSide = Vector3.Distance(this.rightTopPoint, this.leftTopPoint);
        rightSide = Vector3.Distance(this.rightTopPoint, this.rightBottomPoint);
        leftSide = Vector3.Distance(this.rightBottomPoint, this.leftBottomPoint);

        perimeterLength = leftSide + rightSide + topSide + bottomSide;
    }

    public Vector3 GetRandomSpawnPoint()
    {
        float length = Random.Range(0, perimeterLength);

        if (length < leftSide)
            return Vector3.Lerp(leftBottomPoint, leftTopPoint, length / leftSide);

        else if (length < leftSide + topSide)
            return Vector3.Lerp(leftTopPoint, rightTopPoint, (length - leftSide) / topSide);

        else if (length < leftSide + topSide + rightSide)
            return Vector3.Lerp(rightTopPoint, rightBottomPoint, (length - leftSide - topSide) / rightSide);

        else
            return Vector3.Lerp(rightBottomPoint, leftBottomPoint, (length - leftSide - topSide - rightSide) / bottomSide);
    }
}

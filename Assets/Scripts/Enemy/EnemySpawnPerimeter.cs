using UnityEngine;

public class EnemySpawnPerimeter
{
    private Vector3 leftBottomPoint;
    private Vector3 leftTopPoint;
    private Vector3 rightTopPoint;
    private Vector3 rightBottomPoint;

    private float leftSide;
    private Vector3 leftMiddlePoint;

    private float topSide;
    private Vector3 topMiddlePoint;

    private float rightSide;
    private Vector3 rightMiddlePoint;

    private float bottomSide;
    private Vector3 bottomMiddlePoint;

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
        bottomSide = Vector3.Distance(this.rightBottomPoint, this.leftBottomPoint);

        leftMiddlePoint = Vector3.Lerp(this.leftBottomPoint, this.leftTopPoint, 0.5f);
        topMiddlePoint = Vector3.Lerp(this.leftTopPoint, this.rightTopPoint, 0.5f);
        rightMiddlePoint = Vector3.Lerp(this.rightTopPoint, this.rightBottomPoint, 0.5f);
        bottomMiddlePoint = Vector3.Lerp(this.rightBottomPoint, this.leftBottomPoint, 0.5f);

        perimeterLength = leftSide + rightSide + topSide + bottomSide;
    }

    public void GetRandomTransform(out Vector3 enemyPosition, out Vector3 enemyRotateTo)
    {
        float length = Random.Range(0, perimeterLength);

        if (length < leftSide)
        {
            enemyPosition = Vector3.Lerp(leftBottomPoint, leftTopPoint, length / leftSide);
            enemyRotateTo = Vector3.Lerp(topMiddlePoint, bottomMiddlePoint, Random.value);
        }

        else if (length < leftSide + topSide)
        {
            enemyPosition = Vector3.Lerp(leftTopPoint, rightTopPoint, (length - leftSide) / topSide);
            enemyRotateTo = Vector3.Lerp(leftMiddlePoint, rightMiddlePoint, Random.value);

        }

        else if (length < leftSide + topSide + rightSide)
        {
            enemyPosition = Vector3.Lerp(rightTopPoint, rightBottomPoint, (length - leftSide - topSide) / rightSide);
            enemyRotateTo = Vector3.Lerp(topMiddlePoint, bottomMiddlePoint, Random.value);
        }

        else
        {
            enemyPosition = Vector3.Lerp(rightBottomPoint, leftBottomPoint, (length - leftSide - topSide - rightSide) / bottomSide);
            enemyRotateTo = Vector3.Lerp(leftMiddlePoint, rightMiddlePoint, Random.value);
        }
    }
}

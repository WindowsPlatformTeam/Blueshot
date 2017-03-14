using UnityEngine;

public class ShotController : MonoBehaviour
{
    public Material Material;
    public float MaxDistance = 10;

    private LineRenderer line;

    private void Start()
    {
        CreateLine();
    }

    void Update()
    {
        DrawShotIndicator();
    }

    private void CreateLine()
    {
        line = new GameObject("ShotIndicator").AddComponent<LineRenderer>();
        line.material = Material;
        line.numPositions = 2;
        line.startWidth = 0.04f;
        line.endWidth = 0.04f;
        line.useWorldSpace = true;
    }

    private void DrawShotIndicator()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = mousePos - transform.position;
        var ray = new Ray2D(transform.position, direction);
        var hit = Physics2D.Raycast(transform.position, direction, MaxDistance);

        line.SetPosition(0, ray.origin);

        if (hit.collider != null)
        {
            line.SetPosition(1, hit.point);
        }
        else
        {
            line.SetPosition(1, ray.GetPoint(MaxDistance));
        }
    }
}
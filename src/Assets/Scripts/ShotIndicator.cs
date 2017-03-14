using UnityEngine;

public class ShotIndicator : MonoBehaviour
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
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = mousePos - transform.position;
        var ray = new Ray(transform.position, direction);
        RaycastHit hit;

        line.SetPosition(0, ray.origin);

        if (Physics.Raycast(transform.position, direction, out hit, MaxDistance))
        {
            line.SetPosition(1, hit.point);
        }
        else
        {
            line.SetPosition(1, ray.GetPoint(MaxDistance));
        }

        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //line.SetPosition(0, gameObject.transform.position);
        //line.SetPosition(1, mousePos);

        //line.
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
}

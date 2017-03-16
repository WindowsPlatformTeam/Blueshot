using UnityEngine;

public class ShotController : MonoBehaviour
{
    public Material Material;
    public Material ShotMaterial;
    public float MaxDistance = 10;
    public float ShotCooldown = 0.5f;

    private LineRenderer line;
    private float nextShot;

    private void Start()
    {
        CreateLine();
    }

    void Update()
    {
        var ray = RayShotIndicator();
        ShotControl(ray);
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

    private RaycastHit2D RayShotIndicator()
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

        return hit;
    }

    private void ShotControl(RaycastHit2D hit)
    {
        if (Time.time < nextShot)
        {
            line.material = ShotMaterial;
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            nextShot = Time.time + ShotCooldown;
            Shot(hit.collider);
        }

        line.material = Material;
    }

    private void Shot(Collider2D collider)
    {
        if (collider == null) return;

        Destroy(collider.gameObject);
    }
}
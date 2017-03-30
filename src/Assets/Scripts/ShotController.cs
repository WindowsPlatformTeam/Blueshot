using UnityEngine;

public class ShotController : MonoBehaviour
{
    public Material Material;
    public Material ShotMaterial;
    public float MaxDistance = 10;
    public float ShotCooldown = 0.5f;

    private LineRenderer _line;
    private float _nextShot;
    private GameController _gameController;

    private void Start()
    {
        CreateLine();
        _gameController = GameController.GetInstance();
    }

    void Update()
    {
        var ray = RayShotIndicator();
        ShotControl(ray);
    }

    private void CreateLine()
    {
        _line = new GameObject("ShotIndicator").AddComponent<LineRenderer>();
        _line.material = Material;
        _line.numPositions = 2;
        _line.startWidth = 2f;
        _line.endWidth = 2f;
        _line.sortingLayerName = "Foreground";
        _line.useWorldSpace = true;
    }

    private RaycastHit2D RayShotIndicator()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = mousePos - transform.position;
        var ray = new Ray2D(transform.position, direction);
        var hit = Physics2D.Raycast(transform.position, direction, MaxDistance);

        _line.SetPosition(0, ray.origin);

        if (hit.collider != null && !hit.collider.isTrigger)
        {
            _line.SetPosition(1, hit.point);
        }
        else
        {
            _line.SetPosition(1, ray.GetPoint(MaxDistance));
        }

        return hit;
    }

    private void ShotControl(RaycastHit2D hit)
    {
        if (Time.time < _nextShot)
        {
            _line.material = ShotMaterial;
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            _nextShot = Time.time + ShotCooldown;
            Shot(hit.collider);
        }

        _line.material = Material;
    }

    private void Shot(Collider2D collider)
    {
        if (collider == null || collider.tag != "Enemy") return;

        Destroy(collider.gameObject);
        _gameController.AddScore(1);
    }
}
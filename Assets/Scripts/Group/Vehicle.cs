using System.Collections;
using UnityEngine;

public class Vehicle : MonoBehaviour, IMovable, IDestroyable
{
    [SerializeField] private Color _vehicleColor;
    [SerializeField] private float _vehicleSpeed, _vehicleRotateSpeed;

    private PathDrawer _pathDrawer;
    private GameObject _finishZone;
    public bool IsMoving { get; set; }
    private bool _IsReady = true;

    private void Start()
    {
        _pathDrawer = FindAnyObjectByType<PathDrawer>();
        IsMoving = _IsReady = false;
        gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        if (!IsMoving && !_IsReady)
        {
            _pathDrawer.StartDrawLine(_vehicleColor); 
        }
    }

    private void OnMouseDrag()
    {
        if (!IsMoving && !_IsReady)
        {
            _pathDrawer.AddPointToLine(); 
        }
    }

    private void OnMouseUp()
    {
        if (!IsMoving && !_IsReady)
        {
            _pathDrawer.FinishDrawLine(this, out _finishZone);
            _IsReady = true;
        }
    }

    public void Move(LineRenderer path)
    {
        StopAllCoroutines();
        IsMoving = true;
        StartCoroutine(MoveRoutine(path));
    }

    private IEnumerator MoveRoutine(LineRenderer path)
    {
        for (int i = 0; i < path.positionCount; i++)
        {
            Vector3 target = path.GetPosition(i);

            while (Vector3.Distance(transform.position, target) > 0.05f)
            {
                Vector3 direction = target - transform.position;

                float angle = Mathf.Atan2(direction.normalized.y, direction.normalized.x) * Mathf.Rad2Deg - 90f;
                Quaternion objRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, objRotation, Time.deltaTime * _vehicleRotateSpeed);


                transform.position = Vector3.MoveTowards(transform.position, target, _vehicleSpeed * Time.deltaTime);
                yield return null;
            }
        }

        _finishZone.TryGetComponent<FinishZone>(out FinishZone zone);
        if(zone != null)
        {
            zone.VehicleArived(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Die();
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}

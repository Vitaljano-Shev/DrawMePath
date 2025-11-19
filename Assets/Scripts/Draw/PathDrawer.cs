using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private  float _minDistance = 0.3f;
    [SerializeField] [Range(0f, 1f)] private  float _lineWidth;
    [SerializeField] private  LayerMask _finishZone;

    private  LineRenderer _currentLine;
    private  List<Vector3> _linePoints = new List<Vector3>();

    public void StartDrawLine(Color lineColor)
    {
        Vector3 startPos = GetScreenToWorldPoint(Input.mousePosition);
        startPos.z = 0;

        _currentLine = Instantiate(_linePrefab).GetComponent<LineRenderer>();
        _linePoints.Clear();
        _linePoints.Add(startPos);

        _currentLine.positionCount = 1;
        _currentLine.startWidth = _currentLine.endWidth = _lineWidth;
        _currentLine.startColor = _currentLine.endColor = lineColor;
        _currentLine.SetPosition(0, startPos);
    }

    public void AddPointToLine()
    {
        if (_currentLine == null) return;

        Vector3 newPos = GetScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0;

        if(_linePoints.Count == 0 || Vector2.Distance(_linePoints[^1],newPos) > _minDistance)
        {
            _linePoints.Add(newPos);
            _currentLine.positionCount = _linePoints.Count;
            _currentLine.SetPosition(_linePoints.Count - 1, newPos);
        }
    }

    public void FinishDrawLine(IMovable movableObj, out GameObject finishZone)
    {
        Vector3 lastPos = GetScreenToWorldPoint(Input.mousePosition);
        lastPos.z = 0;

        Collider2D hit = Physics2D.OverlapPoint(lastPos, _finishZone);

        if(hit != null)
        {
            LevelController.Instance.PathReady(movableObj, _currentLine);
            _currentLine = null;
            finishZone = hit.gameObject;
        }
        else
        {
            Destroy(_currentLine.gameObject);
            _currentLine = null;
            finishZone = null;
        }
    }

    private Vector3 GetScreenToWorldPoint(Vector3 pos)
    {
        return Camera.main.ScreenToWorldPoint(pos);
    }
}

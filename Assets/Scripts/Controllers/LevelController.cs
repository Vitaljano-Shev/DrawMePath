using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }

    private int _groupsAmount;
    public int GroupsAmount { set => _groupsAmount = value; get => _groupsAmount; }

    private int _pathReadyAmount = 0;
    private Dictionary<IMovable, LineRenderer> _movables = new Dictionary<IMovable, LineRenderer>();

    private ButtonsController _buttonsController;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        RefreshMovablesVehicles();
        _buttonsController = FindObjectOfType<ButtonsController>();
    }

    public void PathReady(IMovable movableObj, LineRenderer path)
    {
        _pathReadyAmount++;
        _movables.Add(movableObj, path);
        if (_pathReadyAmount == _groupsAmount)
        {
            //MoveAllVehicles();
            _buttonsController.ShowMoveButton();
        }
    }

    public void MoveAllVehicles()
    {
        foreach (IMovable movableKey in _movables.Keys)
        {
            movableKey.Move(_movables[movableKey]);
        }
    }

    public void VehicleDestroyed()
    {
        Debug.Log("Vehicle destroyed. Restarting");
        RestartLevel();
    }

    public void RestartLevel()
    {
        RefreshMovablesVehicles();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        if (SceneManager.sceneCount > SceneManager.GetActiveScene().buildIndex+1)
        {
            Debug.Log("Next level");
            RefreshMovablesVehicles();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void RefreshMovablesVehicles()
    {
        _pathReadyAmount = 0;
        _movables.Clear();
    }
}

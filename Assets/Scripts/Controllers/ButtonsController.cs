using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject _moveButton;

    private void Start()
    {
        _moveButton.SetActive(false);
    }

    public void ShowMoveButton()
    {
        _moveButton.SetActive(true);
    }

    public void Move()
    {
        LevelController.Instance.MoveAllVehicles();
    }
}

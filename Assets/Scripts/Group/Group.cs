using UnityEngine;

public class Group : MonoBehaviour
{
    [SerializeField] private GameObject _vehicle, _finishZone;

    private GroupsContainer _container;

    private bool _groupCorrect;
    public bool GroupCorrect { set => _groupCorrect = value; get => _groupCorrect; }

    private void Start()
    {
        _container = GetComponentInParent<GroupsContainer>();
    }

    public void CheckGroup(GameObject checkVehicle)
    {
        _groupCorrect = (checkVehicle.GetInstanceID() == _vehicle.GetInstanceID());
        _container.AddGroup(_groupCorrect);
    }
}

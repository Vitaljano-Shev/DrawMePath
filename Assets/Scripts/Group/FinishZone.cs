using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FinishZone : MonoBehaviour
{
    public void VehicleArived(GameObject vehicle)
    {
        GetComponentInParent<Group>().CheckGroup(vehicle);
    }
}

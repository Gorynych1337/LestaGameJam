using UnityEngine;

[SelectionBase]
public class SelectionSphere : MonoBehaviour
{
    public Transform GetSphere(string name)
    {
        return new GameObject(name).transform;
    }

}

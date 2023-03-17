using UnityEngine;

public class Brick: MonoBehaviour
{
    [SerializeField] private int _durability;

    public void LooseDurability(int damage)
    {
        _durability -= damage;
        Debug.Log("PV: " + _durability);
        if (_durability <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

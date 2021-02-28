using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private List<InventoryCell> _inventoryCells = new List<InventoryCell>(6);

    private void Start()
    {
        _inventoryCells.AddRange(GetComponentsInChildren<InventoryCell>());
    }

    public void AddItemsInCell(Dictionary<ItemData, int> items)
    {
        for (int i = 0; i < _inventoryCells.Count; i++)
        {
            _inventoryCells[i].Clear();
        }
        int ciclecount = 0;
        foreach (var item in items)
        {
            _inventoryCells[ciclecount].ToAppointItem(item.Key, item.Value);
            ciclecount++;      
        }
    }
}

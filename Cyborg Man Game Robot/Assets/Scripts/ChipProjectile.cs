using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipProjectile : MonoBehaviour
{
    public GameObject weapon;
    private Chip chip;

    public void AssociateWithChip(Chip c)
    {
        chip = c;
    }

    public Chip getAssociatedChip()
    {
        return chip;
    }
}

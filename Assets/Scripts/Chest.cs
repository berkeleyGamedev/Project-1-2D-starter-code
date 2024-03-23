using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    #region GameObject_variables
    [SerializeField]
    [Tooltip("health pack")]
    private GameObject healthpack;
    #endregion

    #region Chest_functions
    IEnumerator DestroyChest()
    {
        /* TODO Part 6.2: Instantiate the health potion at the chest's location and destroy the chest. */
        yield return null;
    }

    public void Open()
    {
        StartCoroutine("DestroyChest");
    }
    #endregion
}

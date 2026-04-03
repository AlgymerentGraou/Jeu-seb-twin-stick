using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleKey : MonoBehaviour
{
    public Inventory refInventory;
	public string objectName;    

	public bool selfDestruct = false;

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			refInventory.objectList.Add(objectName);

			if(selfDestruct == true)
			{
				Destroy(gameObject);
			}
        }
    }
}

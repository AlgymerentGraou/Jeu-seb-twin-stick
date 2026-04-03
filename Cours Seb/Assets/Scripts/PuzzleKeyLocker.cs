using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleKeyLocker : MonoBehaviour
{
    public Inventory refInventory;
	public string objectName;  
    public UnityEvent completeEvent;
    private bool onTrigger = false;
    private bool isOpen = false;

    // Start is called before the first frame update
   private void OnTriggerEnter2D(Collider2D collision)
	{
		if(refInventory.objectList.Contains(objectName) && !isOpen)
		{
			completeEvent.Invoke();
            isOpen = true;
		}     
        else if(!isOpen)
        {
            onTrigger = true;
        }   
    }

    private void OnTriggerExit(Collider collision)
    {
        onTrigger = false;
    }
    
 void OnGUI()
    {
        if (onTrigger == true)
            GUI.Label(new Rect(10, 10, 200, 20), "Il vous manque la " + objectName);                             
    }

   
}

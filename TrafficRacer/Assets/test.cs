using UnityEngine;
using System.Collections;

public class test :  MonoBehaviour
{
	#region Fields

    public Vector3 screenPoint;
    public Vector3 offset;

	#endregion //Fields
	
	#region Unity Methods
	
	private void Start () 
	{
		Initalize();
	}
	
	private void Update () 
	{
	
	}

    private void OnMouseDown()
    {
        Debug.Log("Down"); 
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    private void OnMouseDrag()
    {
        Debug.Log("Drag");
        Debug.Log("Position x : " + Input.mousePosition.x);
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
       // transform.position = curPosition;

    }

    private void OnMouseUp()
    {
        Debug.Log("Up");
    }

	#endregion //Unity Methods
	
	#region Public Methods
	
	public void Initalize()
	{
	
	}
	
	#endregion // Public Methods
	
	#region Private Methods
	
	private void SamplePrivateMethods()
	{
	
	}	
	
	#endregion //Private Methods
}

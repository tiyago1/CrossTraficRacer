using System;
using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour 
{
	#region Fields

	public event Action OnClickEvent;

	#endregion //Fields
	
	#region Unity Methods
	
	private void Start () 
	{
		Initalize();
	}
	
	private void Update () 
	{
	
	}
	
	#endregion //Unity Methods
	
	#region Public Methods
	
	public void Initalize()
	{
	
	}
	
	/// <summary>
	/// OnMouseUp is called when the user has released the mouse button.
	/// </summary>
	void OnMouseUp()
	{
		if (OnClickEvent != null)
		{
			OnClickEvent();
		}
	}

	#endregion // Public Methods
	
	#region Private Methods
	
	private void SamplePrivateMethods()
	{
	
	}	
	
	#endregion //Private Methods
}

using System;
using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour 
{
	#region Fields

    public int SpawnCount;

    public Vector3 Position;
    public event Action<GameObject,SpawnPoint> TriggeredVehicle;


    #endregion //Fields
	
	#region Unity Methods
	
	private void Awake () 
	{
        Position = this.transform.position;
	}
	
	#endregion //Unity Methods
	
	#region Public Methods

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (TriggeredVehicle != null)
        {
            TriggeredVehicle(collider.gameObject,this);
        }

        ///SpawnCount--;
        //Destroy(collider.gameObject);
    }
	
	#endregion // Public Methods
	
	#region Private Methods
	
	private void SamplePrivateMethods()
	{
	
	}	
	
	#endregion //Private Methods
}

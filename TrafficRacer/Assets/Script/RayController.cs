using UnityEngine;
using System.Collections;
using System;

public class RayController : MonoBehaviour 
{
	#region Fields

    public event Action Detected;
    public event Action NotDetected;
    public Vector3 v3;
    public bool b;
    private DirectionType mType;

	#endregion //Fields
	
	#region Unity Methods
	
	private void FixedUpdate ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, v3,1.0f);
        Debug.DrawRay(this.transform.position, v3);

        if (hit.collider != null && hit.collider.tag == mType.ToString())
        {
            if (hit.distance < 0.5f)
            {
                Debug.Log(hit.collider.name);
                b = true;
                OnDetected();
            }
            else
            {
                b = false;
                OnNotDetected();
            }
        }
        else
        {
            Debug.Log("gELİYOR");
            OnNotDetected();
        }
	
	}
	
	#endregion //Unity Methods
	
	#region Public Methods
	
    private void OnDetected()
    {
        if (Detected != null)
        {
            Detected();
        }
    }

    private void OnNotDetected()
    {
        if (NotDetected != null)
        {
            NotDetected();
        }
    }

    public void SetDistance(DirectionType type)
    {
        mType = type;
        switch (type)
        {
            case DirectionType.Up:
                v3 = Vector3.down;
                break;    
            case DirectionType.Down:
                v3 = Vector3.up;
                break;
            case DirectionType.Left:
                v3 = Vector3.right;
                break;
            case DirectionType.Right:
                v3 = Vector3.left;
                break;
        }
    }

	#endregion // Public Methods
	
	#region Private Methods
	
	private void SamplePrivateMethods()
	{
	
	}	
	
	#endregion //Private Methods
}

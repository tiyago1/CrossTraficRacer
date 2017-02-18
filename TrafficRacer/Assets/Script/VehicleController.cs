
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class VehicleController : MonoBehaviour
{
    #region Constants 

    public static string SANSOR = "Sansor";
    public static string DEFAULT = "Default";

    #endregion // Constants

    #region Fields

    public bool isMoveable = true;
    public bool isDetectStop;
    private float mSpeed;

    public List<Animator> CarLightAnimator;
    public RayController RayController;
    public DirectionType mDirection;

	#endregion //Fields
	
	#region Unity Methods

    public void OnDestroy()
    {
        RayController.Detected -= RayController_Detected;
        RayController.NotDetected -= RayController_NotDetected;
    }

    private void FixedUpdate () 
	{
        if (isMoveable && !isDetectStop)
        {
            switch (mDirection)
            {
                case DirectionType.Up:
                    this.transform.position += Vector3.down * Time.deltaTime * mSpeed;
                    break;
                case DirectionType.Down:
                    this.transform.position += Vector3.up * Time.deltaTime * mSpeed;
                    break;
                case DirectionType.Left:
                    this.transform.position += Vector3.right * Time.deltaTime * mSpeed;
                    break;
                case DirectionType.Right:
                    this.transform.position += Vector3.left * Time.deltaTime * mSpeed;
                    break;
            }
        }
	}
	
	#endregion //Unity Methods
	
	#region Public Methods

    public void Initialize(float speed, DirectionType direction)
    {
        Debug.Log(direction);
        switch (direction)
        {
            case DirectionType.Up:
                this.transform.Rotate(0.0f, 0.0f, 180.0f);
                break;
            case DirectionType.Down:
                this.transform.Rotate(0.0f, 0.0f, 0.0f);
                this.transform.rotation.SetEulerRotation(0.0f, 0.0f, 0.0f);
                break;
            case DirectionType.Left:
                this.transform.Rotate(0.0f, 0.0f, -90.0f);
                break;
            case DirectionType.Right:
                this.transform.Rotate(0.0f, 0.0f, 90.0f);
                break;
        }

        mSpeed = speed;
        mDirection = direction;
        SetupToCar(false);
        RayController.Detected += RayController_Detected;
        RayController.NotDetected += RayController_NotDetected;
        RayController.SetDistance(mDirection); 
    }

    void RayController_NotDetected()
    {
        if (isMoveable != false)
        {
            isMoveable = true;
        }
        Debug.Log("ggg");
        if(isDetectStop)
        {
            isDetectStop = false;
        }
    }

    void RayController_Detected()
    {
        isDetectStop = true;
    }

    public void OnMouseDown()
    {
        SetupToCar(isMoveable);
        Debug.Log("Stop");
    }

    public void OnMouseDrag() 
    {
        mSpeed = 10;
    }

	#endregion // Public Methods
	
	#region Private Methods

    /// <summary>
    /// isStop = false -> It can to moveable
    /// isStop = true -> It can't to moveable
    /// </summary>
    /// <param name="isStop"></param>
    private void SetupToCar(bool isStop)
    {
        if (isStop)
        {
            StartCoroutine(TimeTicker());
            isMoveable = false;
        }
        else
        {
            StopCoroutine(TimeTicker());
            isMoveable = true;
        }
    }

    //private Vector3 ConvertVector3ToDirect()
    //{
    //    Vector3 v3;

    //    switch (mDirection)
    //    {
    //        case DirectionType.Up:
    //            break;
    //        case DirectionType.Down:
    //            break;
    //        case DirectionType.Left:
    //            break;
    //        case DirectionType.Right:
    //            break;
    //        case DirectionType.None:
    //            break;
    //    }

    //}

    private IEnumerator TimeTicker()
    {
        yield return new WaitForSeconds(1.5f);
        CarLightAnimator.ForEach(it => it.SetTrigger(SANSOR));
        yield return new WaitForSeconds(1.0f);
        SetupToCar(false);
    }


	#endregion //Private Methods
}

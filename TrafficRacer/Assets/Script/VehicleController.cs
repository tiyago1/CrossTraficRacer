
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
    private float mLastSpeed;

    public List<Animator> CarLightAnimator;
    public RayController RayController;
    public DirectionType mDirection;

    public event Action Crush;

    private float mDragSpeed;
    private float mDragValue;

	public float mWaitingSecond;
	
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
        mLastSpeed = mSpeed;
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
        mSpeed = mLastSpeed;

        StopCarLightSansor();
        mDragValue = Input.mousePosition.x;
        SetupToCar(true);
    }

    public void OnMouseDrag()
    {
        float currentDragValue = Input.mousePosition.x;

        if (mDragValue != currentDragValue)
        {
            float dragingValue = Mathf.Abs(mDragValue - currentDragValue);
            if (dragingValue > 10)
            {
                SetupToCar(false);
                
                mSpeed = 7;
            }
        }
    }
/* 
    public void OnMouseDown()
    {
        StopCarLightSansor();
        isMoveable = false;
        mDragValue = Input.mousePosition.x;
    }

    public void OnMouseUp()
    {
        
    }

     public void OnMouseDrag()
    {
        float curretDragValue =Input.mousePosition.x;
        if (mDragValue != curretDragValue)
        {
            float dragingValue = Mathf.Abs(mDragValue - curretDragValue);
            if (dragingValue > 5)
            {
                SetupToCar(false);
                mSpeed = 7;
            }
            else
            {
                SetupToCar(true);
            }
        }
    }
*/
    public void OnCollisionEnter2D(Collision2D coll) 
    {
        OnCrushed();
    }

    public void OnCrushed()
    {
        if (Crush != null)
        {
            Crush();
        }
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
            StartCoroutine(StartCarLightAnimator());
            isMoveable = false;
        }
        else
        {
            StopCoroutine(StartCarLightAnimator());
            isMoveable = true;
        }
    }

    private IEnumerator StartCarLightAnimator()
    {
        yield return new WaitForSeconds(mWaitingSecond);
        PlayCarLightSansor();
        yield return new WaitForSeconds(mWaitingSecond);
        SetupToCar(false);
    }

    private void PlayCarLightSansor()
    {
        CarLightAnimator.ForEach(it => it.SetTrigger(SANSOR));
    }

    private void StopCarLightSansor()
    {
        CarLightAnimator.ForEach(it => it.SetTrigger(DEFAULT));
    }

	#endregion //Private Methods
}

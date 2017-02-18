using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum DirectionType
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3,

    None
}

public class GameManager : MonoBehaviour 
{
	#region Fields

    public UIManager IngameUIManager;

    public List<GameObject> CarsPrefabs;
    public List<Vector3> SpawnTransform;
    public List<SpawnPoint> SpawnPoints;
    public List<VehicleController> SpawnedVehicles;

    private SpawnPoint mSpawnPoint;

	#endregion //Fields
	
	#region Unity Methods
	
	private void Start () 
	{
		Initialize();
        StartGame();
	}
	
	private void Update () 
	{
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CarCreate(CarsPrefabs[RandomNumberGenerat()], SpawnTransform[7], RandomSpeed());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CarCreate(CarsPrefabs[RandomNumberGenerat()], SpawnTransform[4], RandomSpeed());
        }
	}
	
	#endregion //Unity Methods
	
	#region Public Methods
	
	public void Initialize()
	{
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            SpawnTransform.Add(SpawnPoints[i].Position);
        }
	}

    public void StartGame()
    {
        Initialize();
        IngameUIManager.StartTimer();
        StartCoroutine(CarSpawner());
    }

	#endregion // Public Methods
	
	#region Private Methods

    private IEnumerator CarSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            CarCreate(CarsPrefabs[RandomNumberGenerat()], SpawnTransform[RandomTransformGenerate()], RandomSpeed());
        }
    }

    private Vector3 ReversePosition(Vector3 position, DirectionType type)
    {
        Vector3 value = new Vector3();

        switch (type)
        {
            case DirectionType.Up:
                value = new Vector3(position.x, position.y * -1, position.z);
                break;
            case DirectionType.Down:
                value = new Vector3(position.x, position.y * -1, position.z);
                break;
            case DirectionType.Left:
                value = new Vector3(position.x * -1, position.y, position.z);
                break;
            case DirectionType.Right:
                value = new Vector3(position.x * -1,position.y,position.z);
                break;
            case DirectionType.None:
                break;
        }

        return value;
    }

    private void CarCreate(GameObject carObject, Vector3 position, float speed)
    {
        DirectionType direction = SettingToDirection(position);
        Debug.Log(direction);
        Debug.Log(ReversePosition(position, direction));
        SpawnPoint spawnPoint = SpawnPoints.FirstOrDefault(it => it.Position == ReversePosition(position,direction));
        SpawnPoint forwardPoint = SpawnPoints.FirstOrDefault(it => it.Position == position);

        Debug.Log("SP : " + spawnPoint.SpawnCount + "  FW " + forwardPoint.SpawnCount);

        if (spawnPoint.SpawnCount < 2 && forwardPoint.SpawnCount == 0)
        {
            GameObject gm = Instantiate(carObject, position, Quaternion.identity) as GameObject;
            VehicleController vehicleControl = gm.GetComponent<VehicleController>();
            SpawnedVehicles.Add(vehicleControl);
            vehicleControl.Crush += vehicleControl_Crush;
            gm.tag = direction.ToString();
            gm.GetComponent<VehicleController>().Initialize(speed,direction);
            SpawnPoints.FirstOrDefault(it => it.Position == ReversePosition(position, direction)).SpawnCount++;
            spawnPoint.TriggeredVehicle += spawnPoint_TriggeredVehicle;
        }
    }

    private void vehicleControl_Crush()
    {
        SpawnedVehicles.ForEach(it => Destroy(it.gameObject));
        SpawnedVehicles.Clear();
        StopAllCoroutines();
        IngameUIManager.GameOver();
    }

    void spawnPoint_TriggeredVehicle(GameObject obj, SpawnPoint spawn)
    {
        Destroy(obj);
        spawn.SpawnCount--;
    }

    private int RandomNumberGenerat()   
    {
        return Random.Range(0, CarsPrefabs.Count);
    }

    private int RandomTransformGenerate()
    {
        return Random.Range(0, SpawnTransform.Count);
    }

    private float RandomSpeed()
    {
        return Random.Range(3, 6);
    }

    private DirectionType SettingToDirection(Vector3 position)
    {
        DirectionType type = DirectionType.None;

        Debug.Log("asdasd" +position);

        if (position.x == 9.35f && (position.y == -1.09f || position.y == 1.09f))
        {
            type = DirectionType.Right;
        }
        else if (position.x == -9.35f && (position.y == -1.09f || position.y == 1.09f))
        {
            type = DirectionType.Left;
        }

        if (position.y == -6.0f && (position.x == 1.0f || position.x == -1.0f))
        {
            type = DirectionType.Down;
        }
        else if (position.y == 6.0f && (position.x == 1.0f || position.x == -1.0f))
        {
            type = DirectionType.Up;
        }

        return type;
    }

  

	#endregion //Private Methods
}

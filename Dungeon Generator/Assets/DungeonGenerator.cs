using UnityEngine;
using System.Collections;

public class DungeonGenerator : MonoBehaviour {
	
	private int NumberOfRoomSizes = 3;
	public int AmountOfRooms;
	
	public GameObject RoomSmall;
	public GameObject RoomMedium;
	public GameObject RoomLarge;
	public GameObject Door;
	
	public GameObject CurrentRoom;
	public RoomScript CurrentRoomScript;

	public GameObject LastRoom;
	public RoomScript LastRoomScript;

	public int RoomDirection;

	public int DoorTries = 0;
	
	
	// Use this for initialization
	void Start () {
		GenerateDungeon ();

		//DGGHGFFGFGGFFGFFVGenerateStart ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void GenerateStart () {
		CurrentRoom = (GameObject) Instantiate (GenerateRandomRoomSize (), new Vector3 (0, 0, 0), Quaternion.identity);
		CurrentRoomScript = CurrentRoom.GetComponent<RoomScript> ();
	}
	
	GameObject GenerateRandomRoomSize () {
		
		float rnd = Random.Range (1, NumberOfRoomSizes + 1);
		
		if (rnd == 1) {
			return RoomSmall;
		} 
		else if (rnd == 2) {
			return RoomMedium;
		} 
		else if (rnd == 3) {
			return RoomLarge;
		} 
		else {
			return null;
		}
	}

	public void GenerateRoom () {
		LastRoom = CurrentRoom;
		LastRoomScript = CurrentRoomScript;

		GameObject Room = GenerateRandomRoomSize();

		float roomWidth = (LastRoom.transform.localScale.x * 10 / 2 + 10) + (Room.transform.localScale.x * 10 / 2);
		float roomHeight = (LastRoom.transform.localScale.z * 10 / 2 + 10) + (Room.transform.localScale.z * 10 / 2);
		float oldxPos = LastRoom.transform.position.x;
		float oldzPos = LastRoom.transform.position.z;



		if (RoomDirection == 1) {
			CurrentRoom = (GameObject) Instantiate(Room, new Vector3(oldxPos, 0, oldzPos + roomHeight), Quaternion.identity);
			CurrentRoomScript = CurrentRoom.GetComponent<RoomScript>();
			CurrentRoomScript.SouthExit = true;
		}
		if (RoomDirection == 2) {
			CurrentRoom = (GameObject) Instantiate(Room, new Vector3(oldxPos + roomWidth, 0, oldzPos), Quaternion.identity);
			CurrentRoomScript = CurrentRoom.GetComponent<RoomScript>();
			CurrentRoomScript.WestExit = true;
		}
		if (RoomDirection == 3) {
			CurrentRoom = (GameObject) Instantiate(Room, new Vector3(oldxPos, 0, oldzPos - roomHeight), Quaternion.identity);
			CurrentRoomScript = CurrentRoom.GetComponent<RoomScript>();
			CurrentRoomScript.NorthExit = true;
		}
		if (RoomDirection == 4) {
			CurrentRoom = (GameObject) Instantiate(Room, new Vector3(oldxPos - roomWidth, 0, oldzPos), Quaternion.identity);
			CurrentRoomScript = CurrentRoom.GetComponent<RoomScript>();
			CurrentRoomScript.EastExit = true;
		}

		//CurrentRoom = Instantiate(GenerateRandomRoomSize, new Vector3
	}
	
	public void GenerateDoor () {
		RoomDirection = Random.Range (1, 5);
		Debug.Log (RoomDirection);

		DoorTries++;
		//Debug.Log (RoomDirection);
		
		float roomHeight = (CurrentRoom.transform.localScale.z * 10 / 2 + Door.transform.localScale.z * 10 / 2);
		float roomWidth = (CurrentRoom.transform.localScale.x * 10 / 2 + Door.transform.localScale.x * 10 / 2);
		float oldzPos = CurrentRoom.transform.position.z;
		float oldxPos = CurrentRoom.transform.position.x;
		
		if (RoomDirection == 1) {
			if (!CurrentRoomScript.NorthExit) {
				Instantiate(Door, new Vector3(oldxPos, 0, oldzPos + roomHeight), Quaternion.identity);
				CurrentRoomScript.NorthExit = true;
			}
			else {
				GenerateDoor();
			}
		}
		if (RoomDirection == 2) {

			if (!CurrentRoomScript.EastExit) {
				Instantiate(Door, new Vector3(oldxPos + roomWidth, 0, oldzPos), Quaternion.identity);
				CurrentRoomScript.EastExit = true;
				DoorTries = 0;
			}
			else {
				GenerateDoor();
			}
		}
		if (RoomDirection == 3) {

			if (!CurrentRoomScript.SouthExit) {
				Instantiate(Door, new Vector3(oldxPos, 0, oldzPos - roomHeight), Quaternion.identity);
				CurrentRoomScript.SouthExit = true;
				DoorTries = 0;
			}
			else {
				GenerateDoor();
			}
		}
		if (RoomDirection == 4) {

			if (!CurrentRoomScript.WestExit) {
				Instantiate(Door, new Vector3(oldxPos - roomWidth, 0, oldzPos), Quaternion.identity);
				CurrentRoomScript.WestExit = true;
				DoorTries = 0;
			}
			else {
				GenerateDoor();
			}
		}
	}

	void GenerateDungeon () {
		GenerateStart ();
		GenerateDoor ();

		for (int i = 0; i < AmountOfRooms; i++) {
			GenerateRoom();
			GenerateDoor();
		}
	}
	
}

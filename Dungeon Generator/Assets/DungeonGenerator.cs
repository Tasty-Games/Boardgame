using UnityEngine;
using System.Collections;

public class DungeonGenerator : MonoBehaviour {

	private int NumberOfRoomSizes = 3;

	public GameObject RoomSmall;
	public GameObject RoomMedium;
	public GameObject RoomLarge;
	public GameObject Door;

	public GameObject LastRoom;



	// Use this for initialization
	void Start () {
		GenerateStart ();
		GenerateDoor ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GenerateStart () {
		LastRoom = (GameObject) Instantiate (GenerateRandomRoomSize (), new Vector3 (0, 0, 0), Quaternion.identity);
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

	void GenerateDoor () {
		float rnd = Random.Range (1, 5);

		float zPos = LastRoom.transform.localScale.z * 10 / 2 + Door.transform.localScale.z * 10 / 2;
		float xPos = LastRoom.transform.localScale.x * 10 / 2 + Door.transform.localScale.x * 10 / 2;

		if (rnd == 1) {

			Instantiate(Door, new Vector3(0, 0, zPos), Quaternion.identity);
		}

		if (rnd == 2) {
			Instantiate(Door, new Vector3(xPos, 0, 0), Quaternion.identity);
		}
		if (rnd == 3) {
			Instantiate(Door, new Vector3(0, 0, -zPos), Quaternion.identity);
		}
		if (rnd == 4) {
			Instantiate(Door, new Vector3(-xPos, 0, 0), Quaternion.identity);
		}

	}

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomData", menuName = "RoomData", order = 1)]
public class CreateRooms : ScriptableObject
{
    public string roomName;

    public List<Frage> fragen;
}

[System.Serializable]
public class Frage 
{
    public enum AntwortType { Text, Buttons }

    public string frage;
    public AntwortType antwortType;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Colyseus;
using GameDevWare.Serialization;

public class MatchMaker : MonoBehaviour
{
    // Start is called before the first frame update

    public Client client;

    public GameObject contentListRooms;
    public GameObject panelCreateNewRoom;
    public TMP_InputField newRoomInput;
    public Button createNewRoomBTN;
    public Button cancelCreateNewRoom;

    public Button createRoom;
    private Room<LobbyState> lobbyRoom;

    public GameObject prefabRoomButton;

    public Dictionary<string, GameObject> roomLabels = new Dictionary<string, GameObject>();

    public Button MatchMakerButton;
    public GameObject MatchMakerCanvas;

    void Start()
    {


        createRoom.onClick.AddListener(() =>
        {
            panelCreateNewRoom.SetActive(true);
        });
        cancelCreateNewRoom.onClick.AddListener(() =>
        {
            panelCreateNewRoom.SetActive(false);
        });

        createNewRoomBTN.onClick.AddListener(clickCreateRoom);

        MatchMakerButton.onClick.AddListener(()=>{
            MatchMakerCanvas.SetActive(!MatchMakerCanvas.activeSelf);
        });

        joinLobby();
    }

    void clickCreateRoom()
    {
        Debug.Log("Click Create Room");
         client.createRoom(newRoomInput.text);
    }

    async void joinLobby()
    {
        this.lobbyRoom = await client.client.Join<LobbyState>("Lobby");



        this.lobbyRoom.State.rooms.OnAdd += onAddRoom;
        this.lobbyRoom.State.rooms.OnRemove += onRemoveRoom;

    }

    void onRemoveRoom(MRoom room, string i)
    {
        DestroyImmediate(roomLabels[room.id]);
        roomLabels.Remove(i);

    }

    void onAddRoom(MRoom room, string i)
    {
        Debug.Log("Added room");
        GameObject btn = Instantiate(prefabRoomButton);
        btn.transform.SetParent(contentListRooms.transform);
        TextMeshProUGUI text = btn.GetComponentInChildren<TextMeshProUGUI>();
        text.text = room.name + " / " + room.id;

        Button bC = btn.GetComponent<Button>();

        roomLabels.Add(room.id, btn);
        bC.onClick.AddListener(() =>
        {
            client.joinRoom(room.id);
        });

    }

    // Update is called once per frame
    void Update()
    {

    }
}

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class CustomNetwork : NetworkManager {

    /**
     * 
     * 
     * 
     **/
    public class ChosenCharacter : MessageBase {
        public int classIndex;
    }

    [SerializeField]
    private GameObject[] prefabs; // A list containing all playable character prefabs
    private static int chosenPlayer = 0; // Players 1 - 4. Pick a character! There are buttons in UI for these

    /**
     * Once the client start, this method registers all the player
     * prefabs we are going to use for the players.
     **/
    public override void OnStartClient(NetworkClient client) {
        foreach (GameObject charac in prefabs) {
            ClientScene.RegisterPrefab(charac);
        }
        base.OnStartClient(client);
    }
    /**
     * When a new client joins a host lobby, add this client to the player to the
     * player list of game objects in the server.
     * 
     **/
    public override void OnClientConnect(NetworkConnection conn) {

        ChosenCharacter character = new ChosenCharacter {
            classIndex = chosenPlayer
        };

        IntegerMessage msg = new IntegerMessage(character.classIndex);
        ClientScene.AddPlayer(conn, 0, msg);
    }



    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extra) {
        ChosenCharacter message = extra.ReadMessage<ChosenCharacter>();
        var playerFab = (GameObject)Instantiate(prefabs[message.classIndex], Vector2.zero, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, playerFab, playerControllerId);
        NetworkServer.Spawn(playerFab);
    }

    public void OnGUI() {
        if (GUI.Button(new Rect(65, 340,100, 20), "Finn")) {
            chosenPlayer = 0;
        }
        if (GUI.Button(new Rect(65, 390, 100, 20), "Gringo")) {
            chosenPlayer = 1;
        }
        if (GUI.Button(new Rect(65, 430, 100, 20), "Rango")) {
            chosenPlayer = 2;
        }
        if (GUI.Button(new Rect(65, 470, 100, 20), "Zapdos")) {
            chosenPlayer = 3;
        }
    }

}
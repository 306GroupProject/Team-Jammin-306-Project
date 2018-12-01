using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;

public class CustomNetwork : NetworkManager {

    public Camera main;
    public Camera start;
    /*
     * Message base that stores the player's selected character into the server.
     */
    public class ChosenCharacter : MessageBase {
        public int classIndex;
    }

    [SerializeField]
    private GameObject[] prefabs; // A list containing all playable character prefabs
    private static int chosenPlayer = 0; // Players 1 - 4. Pick a character! There are buttons in UI for these

	GameObject currentPrefabSpawning; // this is the current Prefab which is being spawned into the Server!

    /*
     * Once the client start, this method registers all the player
     * prefabs we are going to use for the players.
     */
    public override void OnStartClient(NetworkClient client) {
        GameObject.Find("Main Camera").GetComponent<Camera>().transform.position = GameObject.Find("Room1 Center").transform.position;
        foreach (GameObject charac in prefabs) {
            ClientScene.RegisterPrefab(charac);
        }
        base.OnStartClient(client);
    }

   

    /*
     * When a new client joins a host lobby, add this client to the player to the
     * player list of game objects in the server.
     * 
     */
    public override void OnClientConnect(NetworkConnection conn) {

        ChosenCharacter character = new ChosenCharacter {
            classIndex = chosenPlayer
        };

        
        IntegerMessage msg = new IntegerMessage(character.classIndex);
        ClientScene.AddPlayer(conn, 0, msg);

        GetComponent<NetworkManagerHUD>().enabled = false;
        showPlayerButtons = false;
        
    }


    /*
     * Overrides the built in OnServerAddPlayer, to instead use a custom script that allows us to select
     * a character, instead of using the single built-in playerPrefab tab in the default net manager.
     */
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extra) {
        this.playerSpawnMethod = PlayerSpawnMethod.RoundRobin;
		// Obtain the message stored in the server containing the selected player character
        ChosenCharacter message = extra.ReadMessage<ChosenCharacter>(); 
        
        // Instantiate the selected player model based on the character player selected
        var playerFab = (GameObject)Instantiate(prefabs[message.classIndex], Vector2.zero, Quaternion.identity);

        // Get the ChangePlayerIdentity componenent so that we can set the player's tag into a unique one
        ChangePlayerIdentity playerTag = playerFab.GetComponent<ChangePlayerIdentity>();
        playerTag.playerTag = "Player" + (message.classIndex + 1);
		

        if (playerTag.playerTag == "Player1") {
            playerTag.playerName = "Finn";
        } 
        else if (playerTag.playerTag == "Player2") {
            playerTag.playerName = "Spike";
        } 
        else if (playerTag.playerTag == "Player3") {
            playerTag.playerName = "Lizz";
        } 
        else if (playerTag.playerTag == "Player4") {
            playerTag.playerName = "Zap";
        }

        // Add the player, and spawn it!
        NetworkServer.AddPlayerForConnection(conn, playerFab, playerControllerId);
        NetworkServer.Spawn(playerFab);

		currentPrefabSpawning = playerFab;
    }

	public GameObject returnCurrentPrefab(){
		return currentPrefabSpawning;
	}

    /*
     * Test Gui buttons for character select. Subject to change 
     */

    bool showPlayerButtons = true;

    public void OnGUI() {
        if (showPlayerButtons)
        {
            if (GUI.Button(new Rect(65, 340, 100, 20), "Finn"))
            {
                chosenPlayer = 0;
                showPlayerButtons = false;
            }

            if (GUI.Button(new Rect(65, 390, 100, 20), "Spike"))
            {
                chosenPlayer = 1;
                showPlayerButtons = false;
            }
            if (GUI.Button(new Rect(65, 430, 100, 20), "Liz"))
            {
                chosenPlayer = 2;
                showPlayerButtons = false;
            }
            if (GUI.Button(new Rect(65, 470, 100, 20), "Zap"))
            {
                chosenPlayer = 3;
                showPlayerButtons = false;
            }
        }       
    }

    // It would be good to set up a function that just sets showPlayerButtons to true after a client leaves
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<NetworkManagerHUD>().enabled = !showPlayerButtons;
            showPlayerButtons = !showPlayerButtons;
        }
    }
}
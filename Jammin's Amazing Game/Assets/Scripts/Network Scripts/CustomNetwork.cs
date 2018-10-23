using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class CustomNetwork : NetworkManager {

    /**
     * Message base that stores the player's selected character into the server.
     **/
    public class ChosenCharacter : MessageBase {
        public int classIndex;
    }

    [SerializeField]
    private GameObject[] prefabs; // A list containing all playable character prefabs
    private static int chosenPlayer = 0; // Players 1 - 4. Pick a character! There are buttons in UI for these

	[SerializeField] private playerStorage yeet; 

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



	public override void OnStartServer(){
		
		yeet = new playerStorage (4); 
	
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


    /**
     * Overrides the built in OnServerAddPlayer, to instead use a custom script that allows us to select
     * a character, instead of using the single built-in playerPrefab tab in the default net manager.
     */ 
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extra) {

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
            playerTag.playerName = "Sal";
        } 
        else if (playerTag.playerTag == "Player4") {
            playerTag.playerName = "Zap";
        }

        // Add the player, and spawn it!
        NetworkServer.AddPlayerForConnection(conn, playerFab, playerControllerId);
        NetworkServer.Spawn(playerFab);
		yeet.storePlayers (playerFab, message.classIndex); 
    }

	public playerStorage returnPlayers(){


		return yeet; 

	}

    /**
     * Test Gui buttons for character select. Subject to change 
     **/
    public void OnGUI() {
        if (GUI.Button(new Rect(65, 340,100, 20), "Finn")) {
            chosenPlayer = 0;
        }
        if (GUI.Button(new Rect(65, 390, 100, 20), "Spike")) {
            chosenPlayer = 1;
        }
        if (GUI.Button(new Rect(65, 430, 100, 20), "Liz")) {
            chosenPlayer = 2;
        }
        if (GUI.Button(new Rect(65, 470, 100, 20), "Zap")) {
            chosenPlayer = 3;
        }
    }


}
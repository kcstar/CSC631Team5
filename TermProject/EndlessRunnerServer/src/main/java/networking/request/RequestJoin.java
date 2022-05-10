package networking.request;

// Java Imports
import java.io.IOException;
import java.util.List;

// Other Imports
import core.GameServer;
import core.NetworkManager;
import model.Player;
import networking.response.ResponseJoin;
import utility.Log;

/**
 * The RequestLogin class authenticates the user information to log in. Other
 * tasks as part of the login process lies here as well.
 */

public class RequestJoin extends GameRequest {
    // Data
    private Player player;

    // Responses
    private ResponseJoin responseJoin;

    public RequestJoin() {
        responses.add(responseJoin = new ResponseJoin());
    }

    @Override
    public void parse() throws IOException {
        // Eventually parse a game_id or auto-assign one
    }

    @Override
    public void doBusiness() throws Exception {

        System.out.println("Incoming join request!");

        GameServer gs = GameServer.getInstance();
        int id = gs.getID();    // Give the client a user id, 0 if none available
        if(id != 0) {

            // Player join success! Return join success

            player = new Player(id, "Player " + id);
            player.setID(id);
            gs.setActivePlayer(player);
            player.setClient(client);
            // Pass Player reference into thread
            client.setPlayer(player);
            // Set response information

            responseJoin.setStatus((short) 0); // Login is a success
            responseJoin.setPlayer(player);
            responseJoin.setIsCurrentPlayer(true);

            List<Player> activePlayers = gs.getActivePlayers();

            for(Player p : activePlayers) {
                if(p.getID() != player.getID()) {
                    responseJoin = new ResponseJoin();
                    responseJoin.setStatus((short) 0); // Login is a success
                    responseJoin.setPlayer(player);
                    responseJoin.setIsCurrentPlayer(false);
                    p.getClient().addResponseForUpdate(responseJoin);

                    responseJoin = new ResponseJoin();
                    responseJoin.setStatus((short) 0); // Login is a success
                    responseJoin.setPlayer(p);
                    responseJoin.setIsCurrentPlayer(false);
                    responses.add(responseJoin);
                }
            }

            // Inform all other players of the join
            /*
            ResponseJoin connectedResponse = new ResponseJoin();
            connectedResponse.setStatus((short) 0);
            connectedResponse.setIsCurrentPlayer(false);
            NetworkManager.addResponseForAllOnlinePlayers(player.getID(), connectedResponse);
             */

            Log.printf("User '%s' has successfully logged in.", player.getName());

        } else {
            Log.printf("A user has tried to join, but failed to do so.");
            responseJoin.setStatus((short) 1);
        }
    }
}

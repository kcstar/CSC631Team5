package networking.response;

// Other Imports
import core.GameServer;
import metadata.Constants;
import model.Player;
import utility.GamePacket;
import java.util.List;

/**
 * The ResponseLogin class contains information about the authentication
 * process.
 */
public class ResponseJoin extends GameResponse {

    private short status;
    private Player player;
    private boolean currentClient;
    
    public ResponseJoin() {
        responseCode = Constants.SMSG_JOIN;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addShort16(status);
        packet.addInt32((status == 0) ? player.getID() : 0);
        packet.addBoolean(currentClient);

        return packet.getBytes();
    }

    public void setStatus(short status) {
        this.status = status;
    }

    public void setPlayer(Player player) {
        this.player = player;
    }

    public void setIsCurrentPlayer(boolean isPlayer) {
        this.currentClient = isPlayer;
    }
}
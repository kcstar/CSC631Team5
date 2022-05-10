package networking.response;

// Other Imports
import metadata.Constants;
import model.Player;
import utility.GamePacket;
import utility.Log;
/**
 * The ResponseLogin class contains information about the authentication
 * process.
 */
public class ResponseMove extends GameResponse {
    private Player player;
    private float posX;
    private float posY;
    private float velX;
    private float velY;
    private float inputX;
    private boolean jumping;

    public ResponseMove() {
        responseCode = Constants.SMSG_MOVE;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(player.getID());
        packet.addFloat(posX);
        packet.addFloat(posY);
        packet.addFloat(velX);
        packet.addFloat(velY);
        packet.addFloat(inputX);
        packet.addBoolean(jumping);
 
        return packet.getBytes();
    }

    public void setPlayer(Player player) {
        this.player = player;
    }

    public void setData(float posX, float posY, float velX, float velY, float inputX, boolean jumping) {
        this.posX = posX;
        this.posY = posY;
        this.velX = velX;
        this.velY = velY;
        this.inputX = inputX;
        this.jumping = jumping;
    }
}
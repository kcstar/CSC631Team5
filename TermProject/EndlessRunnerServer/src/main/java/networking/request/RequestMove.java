package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import model.Player;
import networking.response.ResponseMove;
import utility.DataReader;
import core.NetworkManager;

public class RequestMove extends GameRequest {
    private float posX, posY, posZ, velX, velY, velZ, walkSpeed, walkX, walkZ;
    private boolean jumping;
    // Responses
    private ResponseMove responseMove;

    public RequestMove() {
        responses.add(responseMove = new ResponseMove());
    }

    @Override
    public void parse() throws IOException {
        posX = DataReader.readFloat(dataInput);
        posY = DataReader.readFloat(dataInput);
        posZ = DataReader.readFloat(dataInput);
        velX = DataReader.readFloat(dataInput);
        velY = DataReader.readFloat(dataInput);
        velZ = DataReader.readFloat(dataInput);
        walkSpeed = DataReader.readFloat(dataInput);
        walkX = DataReader.readFloat(dataInput);
        walkZ = DataReader.readFloat(dataInput);
        jumping = DataReader.readBoolean(dataInput);
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();
        System.out.println("INCOMING MOVE REQUEST FROM PLAYER " + player.getID());

        responseMove.setPlayer(player);
        responseMove.setData(posX, posY, posZ, velX, velY, velZ, walkSpeed, walkX, walkZ, jumping);
        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseMove);
    }
}
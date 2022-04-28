package metadata;

/**
 * The Constants class stores important variables as constants for later use.
 */
public class Constants {
    // Constants
	final public static String CLIENT_VERSION = "1.00";
	final public static String REMOTE_HOST = "localhost";
    final public static int REMOTE_PORT = 9252;
    final public static int TIMEOUT_SECONDS = 3;
	final public static int MAX_PLAYERS = 4;
    
    // Request (1xx) + Response (2xx)
	final public static short CMSG_JOIN = 101;
	final public static short SMSG_JOIN = 201;
	final public static short CMSG_LEAVE = 102;
	final public static short SMSG_LEAVE = 202;
	final public static short CMSG_MOVE = 103;
	final public static short SMSG_MOVE = 203;

	final public static short CMSG_HEARTBEAT = 111;

	final public static int USER_ID = -1;
	final public static int OP_ID = -1;
}
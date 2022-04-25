using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkConfiguration : MonoBehaviour
{
    public string hostIP = Constants.REMOTE_HOST;
    public int hostPort = Constants.REMOTE_PORT;
    private static NetworkConfiguration networkConfig;

    void Awake() {
        DontDestroyOnLoad(gameObject);

        if (networkConfig) {
            Destroy(gameObject);
        } else {
            networkConfig = this;
        }
    }
}

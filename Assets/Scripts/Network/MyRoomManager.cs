using Mirror;
using UnityEngine;

public class MyRoomManager : NetworkRoomManager
{
    /// <summary>
    ///     Called just after GamePlayer object is instantiated and just before it replaces RoomPlayer object.
    ///     This is the ideal point to pass any data like player name, credentials, tokens, colors, etc.
    ///     into the GamePlayer object as it is about to enter the Online scene.
    /// </summary>
    /// <param name="roomPlayer"></param>
    /// <param name="gamePlayer"></param>
    /// <returns>true unless some code in here decides it needs to abort the replacement</returns>
    public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnectionToClient conn, GameObject roomPlayer,
        GameObject gamePlayer)
    {
        gamePlayer.GetComponent<Player>().Initialize();
        return true;
    }

    private bool showStartButton;

    public override void OnRoomServerPlayersReady()
    {
        // calling the base method calls ServerChangeScene as soon as all players are in Ready state.
#if UNITY_SERVER
            base.OnRoomServerPlayersReady();
#else
        showStartButton = true;
#endif
    }

    public override void OnGUI()
    {
        base.OnGUI();

        if (allPlayersReady && showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
        {
            // set to false to hide it in the game scene
            showStartButton = false;

            ServerChangeScene(GameplayScene);
        }
    }
}
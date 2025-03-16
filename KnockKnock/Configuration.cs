using Dalamud.Configuration;
using Dalamud.Plugin;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using System;
using System.Collections.Generic;

namespace SamplePlugin;

[Serializable] 
public class PlayerDataStorageContainer
{
    private string playerName;
    private string playerHomeWorld;
    private bool isPlayerFriend;
    public PlayerDataStorageContainer(string playerName, string playerHomeWorld, bool isFriend)
    { 
        this.playerName = playerName;
        this.playerHomeWorld = playerHomeWorld;
        this.isPlayerFriend = isFriend;
    }
    public string Name
    {
        get { return playerName; }
        set { playerName = value; }
    }
    public string HomeWorld
    { 
        get { return playerHomeWorld; } 
        set { playerHomeWorld = value; } 
    }
    public bool IsPlayerFriend
    {
        get { return isPlayerFriend; }
        set { isPlayerFriend = value; }
    }
}
[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;

    public bool UseWhitelist { get; set; } = true;
    public bool IsConfigWindowMovable { get; set; } = true;

    public Dictionary<ulong, PlayerDataStorageContainer> StoredPlayers = new Dictionary<ulong, PlayerDataStorageContainer>();
    // the below exist just to make saving less cumbersome
    public void Save()
    {
        Plugin.PluginInterface.SavePluginConfig(this);
    }
}

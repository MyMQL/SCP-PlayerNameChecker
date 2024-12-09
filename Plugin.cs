using System;
using Exiled.API.Features;
using CommandSystem;
using RemoteAdmin;

namespace CustomCommands
{
    public class Plugin : Plugin<EmptyConfig>
    {
        public override string Name => "CustomNameCommand";
        public override string Author => "MyMQL";
        public override Version RequiredExiledVersion => new Version(8, 14, 0);

        public override void OnEnabled()
        {
            Log.Info($"{Name} has been successfully enabled!");
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Log.Info($"{Name} has been disabled.");
            base.OnDisabled();
        }
    }

    [CommandHandler(typeof(ClientCommandHandler))]
    public class NameCommand : ICommand
    {
        public string Command => "name";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Displays your current name (player nickname) from the scoreboard.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is PlayerCommandSender playerSender)
            {
                var player = Player.Get(playerSender.ReferenceHub);
                if (player != null)
                {
                    response = $"Your name is: {player.DisplayNickname}";
                    return true;
                }
                else
                {
                    response = "Failed to find your character.";
                    return false;
                }
            }
            else
            {
                response = "This command is only available to players.";
                return false;
            }
        }
    }
}

public class EmptyConfig : Exiled.API.Interfaces.IConfig
{
    public bool IsEnabled { get; set; } = true; // Required by Exiled
    public bool Debug { get; set; } = false;    // Added Debug property
}




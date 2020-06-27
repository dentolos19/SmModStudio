using DiscordRPC;

namespace SmModStudio.Core.Features
{

    public class RichPresence
    {

        private DiscordRpcClient _client;
        private Timestamps _currentTimestamp;
        
        public bool IsActivated { get; private set; }

        public RichPresence()
        {
            _client = new DiscordRpcClient(Constants.DiscordClientId.ToString());
        }

        public void Activate()
        {
            if (!_client.IsDisposed && !_client.IsInitialized)
            {
                _client.Initialize();
                IsActivated = true;
                SetIdlePresence("Created by @dentolos19", true);
            }
                
        }

        public void Deactivate()
        {
            if (!_client.IsDisposed && _client.IsInitialized)
            {
                _client.Deinitialize();
                IsActivated = false;
            }
                
        }

        public void SetIdlePresence(string state = null, bool resetTimestamp = false)
        {
            if (!IsActivated)
                return;
            if (resetTimestamp)
                _currentTimestamp = Timestamps.Now;
            var presence = new DiscordRPC.RichPresence
            {
                Details = "Currently idle",
                State = state,
                Timestamps = _currentTimestamp,
                Assets = new Assets
                {
                    LargeImageKey = "default",
                    LargeImageText = "Scrap Mechanic Mod Studio"
                }
            };
            _client.SetPresence(presence);
        }
        
        public void SetWorkPresence(string projectName, string fileName, bool resetTimestamp = false)
        {
            if (!IsActivated)
                return;
            if (resetTimestamp)
                _currentTimestamp = Timestamps.Now;
            var presence = new DiscordRPC.RichPresence
            {
                Details = $"Working on {projectName}",
                State = $"Editing {fileName}",
                Timestamps = _currentTimestamp,
                Assets = new Assets
                {
                LargeImageKey = "default",
                LargeImageText = "Scrap Mechanic Mod Studio"
                }
            };
            _client.SetPresence(presence);
        }

        public void SetCustomPresence(string details, string state, bool resetTimestamp = false)
        {
            if (!IsActivated)
                return;
            if (resetTimestamp)
                _currentTimestamp = Timestamps.Now;
            var presence = new DiscordRPC.RichPresence
            {
                Details = details,
                State = state,
                Timestamps = _currentTimestamp,
                Assets = new Assets
                {
                LargeImageKey = "default",
                LargeImageText = "Scrap Mechanic Mod Studio"
                }
            };
            _client.SetPresence(presence);
        }

    }

}
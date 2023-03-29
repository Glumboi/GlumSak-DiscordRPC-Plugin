using DiscordRPC;

namespace DiscordRPCPlugin
{
    internal class Rpc
    {
        public DiscordRpcClient client;
        private bool initalized = false;

        public void UpdatePresence(
            string details,
            string state,
            string largeImageName,
            string smallImageName,
            string smallImageHoverText)
        {
            if (!initalized)
            {
                return;
            }
            else
            {
                client.SetPresence(new RichPresence()
                {
                    Details = details,
                    State = state,
                    Timestamps = Timestamps.Now,
                    Assets = new Assets()
                    {
                        LargeImageKey = largeImageName,
                        LargeImageText = smallImageHoverText,
                    }
                });
            }
        }

        public void Init()
        {
            initalized = true;
            client = new DiscordRpcClient("1041087529778167948");
            client.Initialize();
        }
    }
}
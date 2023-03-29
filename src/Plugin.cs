using DiscordRPC.GlumSak;
using DiscordRPCPlugin;
using System;
using System.Reflection;
using System.Windows;

namespace DiscordRPC
{
    public class Plugin
    {
        public static int exitCode;

        public static int PluginEntryPoint(IntPtr mainWindowhandle, object[] args) //--> Default Entrypoint of the Plugin
        {
            if (args.Length > 0 &&
                args[0] is bool &&
                (bool)args[0] == true)
            {
                AppDomain.CurrentDomain.ProcessExit
                    += OverrrideExitEvent; //--> Use custom ExitEvent if first param is bool and true
            }

            //Your custom code here, you can also use WindowsForms with this (WPF might be possible too)

            //UI access demo
            MainWindowDummy mainWindowDummy = new MainWindowDummy(mainWindowhandle);

            //Invoke to the UI Thread of GlumSak
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                //Show a notification in GlumSak
                mainWindowDummy.Notification_SnackBar.Content = "Initializing Discord RPC...";
                mainWindowDummy.Notification_SnackBar.Show();
            }));

            //Your custom code here, you can also use WindowsForms with this (WPF might be possible too)
            Rpc rPC = new Rpc();
            rPC.Init();
            rPC.UpdatePresence((string)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);

            exitCode = 0;
            return exitCode;
        }

        private static void OverrrideExitEvent(object sender, EventArgs e)
        {
            Console.WriteLine($"Plugin {Assembly.GetExecutingAssembly().FullName} exited with code: {exitCode}");
            Console.ReadKey();
        }
    }
}
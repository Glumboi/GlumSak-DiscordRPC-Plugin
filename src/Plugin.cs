﻿using System;
using System.Reflection;
using System.Threading;
using DiscordRPC;

namespace DiscordRPCPlugin
{
    public class Plugin
    {
        public static int exitCode;

        public static int PluginEntryPoint(object[] args) //--> Default Entrypoint of the Plugin
        {
            if (args.Length > 0 &&
                args[0] is bool &&
                (bool)args[0] == true)
            {
                AppDomain.CurrentDomain.ProcessExit
                    += OverrrideExitEvent; //--> Use custom ExitEvent if first param is bool and true
            }

            //Your custom code here, you can also use WindowsForms with this (WPF might be possible too)
            RPC rPC = new RPC();
            rPC.Init();
            rPC.UpdatePresence("Doing things in GlumSak", "GlumSak", "big", "small", "GlumSak by Glumboi");

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
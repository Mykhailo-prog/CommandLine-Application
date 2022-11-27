﻿using CommandLine_App.Commands;
using CommandLine_App.HelperService;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommandLine_App.GlobalCommands.ShowCommandChildren
{
    public class ShowNameCommand : ShowCommand
    {
        public new string Name { get; set; }
        public override string ArgumentDescription { get; set; }
        public ShowNameCommand()
        {
            Name = "show name";
            ArgumentDescription = "Show name (string value)," +
                "like [show name firefox].\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Length != 1)
                {
                    Log.Warning("[{1}] User inputs incorrect count of parameters, [params = '{0}']", param, this.GetType());
                    PrintArgumentTip();
                    return false;
                }

                return ShowByName(param.First());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[{0}] Exeption has been thrown from Execute!", this.GetType());
                return false;
            }
        }

        public override void PrintBaseToString()
        {
            Console.WriteLine(base.ToString());
        }

        public override string ToString()
        {
            return $"\t'{Name}' [name_value] - shows all running processes with specified name";
        }
        private bool ShowByName(string arg)
        {
            try
            {
                var processes = Process.GetProcessesByName(arg).OrderBy(e => e.Id);

                if (processes.Count() == 0)
                {
                    Log.Warning("[{1}] No existing process with current name, [arg = '{0}']", arg, this.GetType());
                    Console.WriteLine("No existing processes with '{0}' name!", arg);
                }
                else
                {
                    Console.WriteLine(ProcessesToString(processes));
                }

                Log.Information("[{0}] Execute has been finished successfully!", this.GetType());
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[{0}] Exeption has been thrown from ShowByName!", this.GetType());
                return false;
            }
        }
    }
}

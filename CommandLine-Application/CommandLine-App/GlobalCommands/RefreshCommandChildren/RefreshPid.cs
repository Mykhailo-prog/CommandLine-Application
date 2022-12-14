﻿using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using CommandLine_App.Pools;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommandLine_App.GlobalCommands.RefreshCommandChildren
{
    public class RefreshPid : Refresh
    {
        public RefreshPid()
        {
            ArgumentDescription = "Refresh pid (string value), like [refresh name 242].\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                RefreshByPID(int.Parse(param[0]));
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Class:{0}][Method:{1}][Parameters = {2}]", this.GetType(), MethodBase.GetCurrentMethod().Name, param);
                return false;
            }
        }

        public new string ToString()
        {
            return $"\tCommand 'refresh pid' [pid_value] - refresh running process with specified process identifier";
        }

        private void RefreshByPID(int arg)
        {
            try
            {
                var process = _processWrapper.GetProcessById(arg);

                _processWrapper.Refresh(process);

                Console.WriteLine($"{process.ProcessName} was refreshed");

                Log.Information($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] finished successfully!");
            }
            catch (ArgumentException ex)
            {
                Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] No existing processes with {arg} id!");
                Console.WriteLine("No existing processes with [{0}] id!", arg);
                throw ex;
            }
        }
    }
}

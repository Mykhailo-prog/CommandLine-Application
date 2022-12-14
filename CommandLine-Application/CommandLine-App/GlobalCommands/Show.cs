﻿using CommandLine_App.Abstraction;
using CommandLine_App.HelperService;
using CommandLine_App.Pools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommandLine_App.Commands
{
    public abstract class Show : Command
    {
        public override abstract bool Execute(params string[] param);

        public override string ToString()
        {
            return $"\nCommand '{this.GetType().Name}' - shows running processes.";
        }

        protected string ProcessesToString(IEnumerable<Process> processes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(new string('-', 54));
            sb.Append(string.Format("\n|{0,-5}|{1,-36}|{2, 6}/Kb|\n", "Id", "Process Description", "Memory"));
            sb.Append(new string('-', 54));

            foreach (var p in processes)
            {
                sb.Append(string.Format("\n|{0,-5}|{1,-36}|{2, 6}/Kb|", p.Id, p.ProcessName, p.PrivateMemorySize64 / 1024));
            }

            return sb.ToString();
        }
    }
}

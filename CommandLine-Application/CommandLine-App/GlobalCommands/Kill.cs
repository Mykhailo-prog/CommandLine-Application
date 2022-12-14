﻿using CommandLine_App.Abstraction;
using CommandLine_App.Pools;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Commands
{
    public abstract class Kill : Command
    {
        public override abstract bool Execute(params string[] param);

        public override string ToString()
        {
            return $"\nCommand '{this.GetType().Name}' - stop specifeied processes.";
        }

    }
}

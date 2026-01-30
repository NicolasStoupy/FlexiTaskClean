using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Constants
{
    public static class TaskStatusCodes
    {
        public const string Ready = "READY";
        public const string InProgress = "INPROG";
        public const string Completed = "COMPLETED";

        public const string NotStarted = "NOTSTARTED";
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelesBot.Helpers
{
    [Serializable]
    public class OperationCompletedHelper
    {
        private bool actionCompleted { get; set; }

        public OperationCompletedHelper(bool actionCompleted)
        {
            this.actionCompleted = actionCompleted;
        }

        public bool WasSuccessfull() => actionCompleted;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weekly.DB.Models;

namespace Weekly.API
{
    public class TaskService
    {
        private WeeklyContext weeklyContext;

        public TaskService(WeeklyContext weeklyContext)
        {
            this.weeklyContext = weeklyContext;
        }


    }
}

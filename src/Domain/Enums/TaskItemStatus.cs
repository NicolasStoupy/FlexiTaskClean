using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum TaskItemStatus
    {
        Ready=1,
        InProgress= 2,
        Completed =3,
        Waiting=4,
        NotStarted=0
    }
}

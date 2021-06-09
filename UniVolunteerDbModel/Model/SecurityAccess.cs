using System;
using System.Collections.Generic;

namespace UniVolunteerDbModel.Model
{
    /// <summary>
    /// Флаговое перечисление возможных прав доступа к объектам
    /// </summary>
    [Flags]
    public enum SecurityAccess
    {
        None = 0,
        CreateEvents = 1 << 0,
        EditNotOwnedEvents = 1 << 1,
        ManageUsers = 1 << 2,
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TimeSheet.Bll.Interfaces
{
    public interface IManageToken
    {
        /// <summary>
        /// Get Currrent Identity Employee Email.
        /// </summary>
        string Email { get; }
    }
}

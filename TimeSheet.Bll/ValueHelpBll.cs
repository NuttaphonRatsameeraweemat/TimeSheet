using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll.Models;

namespace TimeSheet.Bll
{
    public class ValueHelpBll : IValueHelpBll
    {

        #region [Fields]


        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueHelpBll" /> class.
        /// </summary>
        public ValueHelpBll()
        {

        }

        #endregion

        #region [Methods]

        public List<ValueHelpViewModel> Get(string type)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}

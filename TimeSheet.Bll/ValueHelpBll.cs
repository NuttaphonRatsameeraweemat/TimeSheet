using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll.Models;
using TimeSheet.Data.Repository.Interfaces;

namespace TimeSheet.Bll
{
    public class ValueHelpBll : IValueHelpBll
    {

        #region [Fields]

        /// <summary>
        /// The utilities unit of work for manipulating utilities data in database.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// The auto mapper.
        /// </summary>
        private readonly IMapper _mapper;

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueHelpBll" /> class.
        /// </summary>
        public ValueHelpBll(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Get ValueHelp List by type.
        /// </summary>
        /// <param name="type">The type of value.</param>
        /// <returns></returns>
        public IEnumerable<ValueHelpViewModel> Get(string type)
        {
            return _mapper.Map<IEnumerable<TimeSheet.Data.Pocos.ValueHelp>, IEnumerable<ValueHelpViewModel>>(
                _unitOfWork.GetRepository<TimeSheet.Data.Pocos.ValueHelp>().Get(x => x.ValueType == type));
        }

        #endregion

    }
}

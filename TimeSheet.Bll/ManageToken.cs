using Microsoft.AspNetCore.Http;
using TimeSheet.Bll.Interfaces;

namespace TimeSheet.Bll
{
    public class ManageToken : IManageToken
    {

        #region [Fields]

        /// <summary>
        /// The httpcontext.
        /// </summary>
        private readonly HttpContext _httpContext;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentBll" /> class.
        /// </summary>
        /// <param name="httpContextAccessor">The httpcontext value.</param>
        public ManageToken(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Get Currrent Aduser.
        /// </summary>
        public string EmpNo => _httpContext.User.Identity.Name;

        #endregion

    }
}

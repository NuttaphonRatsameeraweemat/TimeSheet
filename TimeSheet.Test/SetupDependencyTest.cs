using Microsoft.Extensions.DependencyInjection;
using System;
using TimeSheet.Bll.Interfaces;
using Xunit;

namespace TimeSheet.Test
{
    /// <summary>
    /// The SetupDependencyTest class testing install and load serivces.
    /// </summary>
    public class SetupDependencyTest : IClassFixture<IoCConfig>
    {

        #region [Fields]

        /// <summary>
        /// The timesheet manager provides Login functionality.
        /// </summary>
        private ITimeSheetBll _timeSheet;
        /// <summary>
        /// The Login manager provides Login functionality.
        /// </summary>
        private ILoginBll _login;

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupDependencyTest" /> class.
        /// </summary>
        /// <param name="io">The IoCConfig class provide installing all components needed to use.</param>
        public SetupDependencyTest(IoCConfig io)
        {
            _timeSheet = io.ServiceProvider.GetRequiredService<ITimeSheetBll>();
            _login = io.ServiceProvider.GetRequiredService<ILoginBll>();
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Test load configuration appsetting. 
        /// </summary>
        [Fact]
        public void TestConfigurationDenpendency()
        {
            try
            {
                var data = _login.Authenticate(new Bll.Models.LoginViewModel { Username = "admin@leaderplanet.co.th", Password = "admin" }, new Bll.Models.EmployeeViewModel());
                Assert.True(data, "Unauthorized");
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }

        /// <summary>
        /// Test load auto mapper.
        /// </summary>
        [Fact]
        public void TestAutoMapperDenpendency()
        {
            try
            {
                var data = _timeSheet.Get("nuttaphon@leaderplanet.co.th", "2019-05-01");
                Assert.NotNull(data);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }

        #endregion

    }
}

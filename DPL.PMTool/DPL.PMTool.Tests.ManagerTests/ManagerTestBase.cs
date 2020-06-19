using DPL.PMTool.Accessors;
using DPL.PMTool.Accessors.Shared.EntityFramework;
using DPL.PMTool.Common.Contracts;
using DPL.PMTool.Managers;
using DPL.PMTool.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DPL.PMTool.Tests.ManagerTests
{
    public abstract class ManagerTestBase
    {
        public ManagerTestBase()
        {
            ManagerFactory = new ManagerFactory(Context);
            PlanningManager = ManagerFactory.CreateManager<IPlanningManager>();
        }
        
        [TestInitialize()]
        public void Init()
        {
            CreateGlobalContext();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            CancelGlobalTransaction();
        }

        public static void CreateGlobalContext()
        {
            DatabaseContext.UnitTestContext = DatabaseContext.Create(false);
            DatabaseContext.UnitTestContext.Database.BeginTransaction();
        }

        public static void CancelGlobalTransaction()
        {
            if (DatabaseContext.UnitTestContext != null)
            {
                DatabaseContext.UnitTestContext.Database.RollbackTransaction();
                DatabaseContext.UnitTestContext.AllowDispose = true;
                DatabaseContext.UnitTestContext.Dispose();
                DatabaseContext.UnitTestContext = null;
            }
        }

        protected AmbientContext Context { get; } = new AmbientContext();

        protected ManagerFactory ManagerFactory { get; set; }
        
        protected IPlanningManager PlanningManager { get; set; } 
    }
}

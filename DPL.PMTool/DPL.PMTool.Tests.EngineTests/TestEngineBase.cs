using DPL.PMTool.Accessors;
using DPL.PMTool.Common.Contracts;
using DPL.PMTool.Engines;
using DPL.PMTool.Managers;
using DPL.PMTool.Utilities;

namespace DPL.PMTool.Tests.EngineTests
{
    public abstract class EngineTestBase
    {
        public EngineTestBase()
        {
            AccessorFactory = new AccessorFactory(Context, new UtilityFactory(Context));
            EngineFactory = new EngineFactory(Context,AccessorFactory, new UtilityFactory(Context));

            ScheduleEngine = EngineFactory.CreateEngine<IScheduleEngine>();
        }
        
        protected AccessorFactory AccessorFactory { get; set; }
        protected EngineFactory EngineFactory { get; set; }
        
        protected AmbientContext Context { get; } = new AmbientContext();

        protected IScheduleEngine ScheduleEngine { get; set; } 
    }
}

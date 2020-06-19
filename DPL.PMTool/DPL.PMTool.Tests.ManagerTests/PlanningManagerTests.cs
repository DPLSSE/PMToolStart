using System;
using DPL.PMTool.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DPL.PMTool.Tests.ManagerTests
{
    [TestClass]
    public class PlanningManagerTests : ManagerTestBase
    {
        [TestMethod]
        public void PlanningManager_SaveProject_Test()
        {
            // arrange / given
            var project = new Project()
            {
                Name = "TEST-" + Guid.NewGuid().ToString(),
                Start = DateTime.Now,
                Activities = new []
                {
                    new Activity()
                    {
                        Id = 0,
                        TaskName = "T1",
                        Estimate = 1,
                        Start = DateTime.Now,
                        Finish = DateTime.Now,
                        Priority = 1M,
                        Resource = "Doug",
                        Predecessors = "1",
                    }
                }
            };

            // act / when
            var saved = PlanningManager.SaveProject(project);
            
            // assert / then
            Assert.IsNotNull(saved);
            Assert.IsTrue(saved.Id > 0);
            Assert.IsTrue(saved.Activities.Length > 0);
        }
    }
}
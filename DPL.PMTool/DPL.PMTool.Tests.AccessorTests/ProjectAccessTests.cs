using System;
using DPL.PMTool.Accessors.Shared.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DPL.PMTool.Tests.AccessorTests
{
    [TestClass]
    public class ProjectAccessTests : TestAccessorBase
    {
        [TestMethod]
        public void ProjectAccess_BasicProject_Test()
        {
            // arrange / given
            var project = new Project()
            {
                Name = "TEST-" + Guid.NewGuid().ToString()
            };

            // act / when
            var saved = ProjectAccess.SaveProject(project);
            var loaded = ProjectAccess.Project(saved.Id);
            
            // assert / then
            Assert.IsNotNull(loaded);
            Assert.IsTrue(loaded.Id > 0);
        }
        
        [TestMethod]
        public void ProjectAccess_BasicActivity_Test()
        {
            // arrange / given
            var activity = new Activity()
            {
                TaskName = "TEST-" + Guid.NewGuid().ToString(),
                Estimate = 0.5M,
                Start = DateTime.Now,
                Finish = DateTime.Now.AddDays(1),
                Predecessors = "",
                Resource = "Dev1",
                ProjectId = 0,
            };

            // act / when
            var saved = ProjectAccess.SaveActivity(activity);
            var loaded = ProjectAccess.Activity(saved.Id);
            
            // assert / then
            Assert.IsNotNull(loaded);
            Assert.IsTrue(loaded.Id > 0);
        }

        [DataTestMethod]
        [DataRow(0.5, true, DisplayName= "Estimate must be greater than 0")]
        [DataRow(-0.05, false, DisplayName= "Estimates less than 0 should error")]
        public void ProjectAccess_DataChecks(decimal estimate, bool result)
        {
            // arrange / given
            var activity = new Activity()
            {
                TaskName = "",
                Estimate = estimate,
                Start = DateTime.Now,
                Finish = DateTime.Now.AddDays(1),
                Predecessors = "",
                Resource = "Dev1",
                ProjectId = 0,
            };

            // act / when
            var saved = ProjectAccess.SaveActivity(activity);
            
        }
    }
}
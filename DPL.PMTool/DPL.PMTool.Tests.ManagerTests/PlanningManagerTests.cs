using System;
using AutoMapper.QueryableExtensions;
using DPL.PMTool.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DPL.PMTool.Tests.ManagerTests
{
    [TestClass]
    public class PlanningManagerTests : ManagerTestBase
    {
        [TestMethod]
        public void PlanningManager_CreateProject_Test()
        {
            // arrange / given
            var project = CreateProject();

            // act / when
            var saved = PlanningManager.SaveProject(project);
            
            // assert / then
            Assert.IsNotNull(saved);
            Assert.IsTrue(saved.Id > 0);
            Assert.IsTrue(saved.Activities.Length > 0);
        }
        
        [TestMethod]
        public void PlanningManager_UpdateProject_Test()
        {
            // arrange / given
            var project = CreateProject();
            var inserted = PlanningManager.SaveProject(project);
            
            // act / when
            inserted.Name = "updated";
            inserted.Activities[0].TaskName = "updated activity";
            var updated = PlanningManager.SaveProject(inserted);
            
            // assert / then
            Assert.IsNotNull(updated);
            Assert.AreEqual(inserted.Id, updated.Id);
            Assert.AreEqual(inserted.Name, updated.Name);
            Assert.AreEqual(inserted.Activities[0].TaskName, updated.Activities[0].TaskName);
        }

        [TestMethod]
        public void PlanningManager_Projects_Test()
        {
            // arrange / given
            var project = CreateProject();

            // act / when
            var before = PlanningManager.Projects();
            var saved = PlanningManager.SaveProject(project);
            var after = PlanningManager.Projects();
            
            // assert / then
            Assert.IsTrue(before.Length < after.Length);
        }
        
        [TestMethod]
        public void PlanningManager_NewProject_Test()
        {
            // act / when
            var project = PlanningManager.NewProject();
            
            // assert / then
            Assert.IsNotNull(project);
        }
        
        
        private Project CreateProject()
        {
            return new Project()
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
        }
    }
}
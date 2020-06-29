using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DPL.PMTool.Managers;

namespace DPL.PMTool.Tests.EngineTests
{
    [TestClass]
    public class ScheduleEngineTests : EngineTestBase
    {
        int Add(int a, int b)
        {
            return a + b;
        }

        [TestMethod]
        public void Add_Test()
        {
            Assert.AreEqual(3, Add(1, 2));
        }
        
        [DataTestMethod]
        [DataRow(1, 2, 3)]
        public void Add_Test2(int a, int b, int result)
        {
            Assert.AreEqual(result, Add(a, b));
        }
        
        
        [TestMethod]
        public void ScheduleEngine_Basic_Test()
        {
            // Arrange
            var before = new Project()
            {
                Start = DateTime.Parse("2020-06-01"),
                Activities = new []
                {
                    new Activity()
                    {
                        Estimate = 1,
                        Resource =  "Dev1",
                        Sequence = 1,
                    },
                    new Activity()
                    {
                        Estimate = 1,
                        Resource =  "Dev1",
                        Sequence = 2,
                    },
                    new Activity()
                    {
                        Estimate = 1,
                        Resource =  "Dev1",
                        Sequence = 3,
                    }                    
                }
            };
            
            // Act
            var after = ScheduleEngine.CalculateSchedule(before);
            
            // Assert
            Assert.AreEqual(DateTime.Parse("2020-06-01"), 
                after.Activities.First(a => a.Sequence == 1).Start);
            Assert.AreEqual(DateTime.Parse("2020-06-02"), 
                after.Activities.First(a => a.Sequence == 2).Start);
            Assert.AreEqual(DateTime.Parse("2020-06-03"), 
                after.Activities.First(a => a.Sequence == 3).Start);
        }

        [DataTestMethod]
        [DynamicData(nameof(ProjectTestData), DynamicDataSourceType.Method)]
        public void ScheduleEngine_Data_Driven(Project project, Dictionary<int, DateTime> finishDates)
        {
            Assert.IsNotNull(project);
            
            // Act
            var after = ScheduleEngine.CalculateSchedule(project);
            
            // Assert
            foreach (var finishDate in finishDates)
            {
                // find activity with same sequence
                var activity = after.Activities.First(a => a.Sequence == finishDate.Key);
                Assert.AreEqual(finishDate.Value, activity.Finish);
            }
        }

        public static IEnumerable<object[]> ProjectTestData()
        {
            yield return new Object[]
            {
                new Project()
                {
                    Start = DateTime.Parse("2020-06-01"),
                    Activities = new[]
                    {
                        new Activity(1, 1, "Dev1", 1, "0"),
                        new Activity(2, 1, "Dev1", 1, "1"),
                        new Activity(3, 1, "Dev1", 1, "2")
                    }
                },
                new Dictionary<int, DateTime>()
                {
                    [1] = DateTime.Parse("2020-06-02"),
                    [2] = DateTime.Parse("2020-06-03"),
                    [3] = DateTime.Parse("2020-06-04")
                }
            };
            yield return new Object[]
            {
                new Project()
                {
                    Start = DateTime.Parse("2020-06-01"),
                    Activities = new[]
                    {
                        new Activity(1, 1, "Dev1", 1, "2"),
                        new Activity(2, 1, "Dev1", 3, "3"),
                        new Activity(3, 1, "Dev1", 0, "0")
                    }
                },
                new Dictionary<int, DateTime>()
                {
                    [1] = DateTime.Parse("2020-06-02"),
                    [2] = DateTime.Parse("2020-06-03"),
                    [3] = DateTime.Parse("2020-06-04")
                }
            };
        }
    }
}
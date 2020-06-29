using System;

namespace DPL.PMTool.Managers
{
    public class Activity
    {
        public Activity()
        {
        }

        public Activity(int sequence, decimal estimate, string resource, decimal priority, string predecessor)
        {
            Sequence = sequence;
            Estimate = estimate;
            Resource = resource;
            Priority = priority;
            Predecessors = predecessor;
        }

        public int Id { get; set; }
        public int Sequence { get; set; }
        public string TaskName { get; set; }
        public decimal Estimate { get; set; }
        public string Predecessors { get; set; }
        public string Resource { get; set; }
        public decimal Priority { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
    }
}
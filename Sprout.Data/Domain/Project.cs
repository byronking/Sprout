using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Data.Domain
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectOriginator { get; set; }
        public string TitleThumbImageLink { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int StageId { get; set; }
        public bool Active { get; set; }
        public DateTime OriginationDate { get; set; }
        public decimal FundingGoal { get; set; }
        public decimal PercentageFunded { get; set; }
        public decimal AmountFunded { get; set; }
        public DateTime LastPledgeDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Data.Domain
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage="Please enter a name for your project")]
        public string ProjectName { get; set; }
        public int ProjectOriginatorId { get; set; }
        public string TitleThumbImageLink { get; set; }

        [Required(ErrorMessage = "Please enter a short summary for your project")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Please enter a description for your project")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a location for your project")]
        public string Location { get; set; }
        public int StageId { get; set; }
        public bool Active { get; set; }
        public DateTime OriginationDate { get; set; }

        [Required(ErrorMessage = "Please enter a funding goal for your project")]
        public decimal FundingGoal { get; set; }
        public decimal PercentageFunded { get; set; }
        public decimal AmountFunded { get; set; }
        public DateTime? LastPledgeDate { get; set; }
    }
}

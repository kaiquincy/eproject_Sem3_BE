using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CareerGuidancePlatform.Models
{
    public class Job
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
        public string Salary { get; set; }
        public string Description { get; set; }
        public DateTime PostedDate { get; set; }
        public string Url { get; set; }

        public int CompanyId { get; set; }

        [JsonIgnore]
        public Company Company { get; set; }

        public string Category { get; set; }

        [JsonIgnore]
        public ICollection<EmployerReview> Reviews { get; set; } = new List<EmployerReview>();
    }
}

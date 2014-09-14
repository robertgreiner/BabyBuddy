using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyBuddy.Infrastructure.Models
{
    [Serializable]
    public class SampleMessage
    {
        public string DeviceId { get; set; }
        public string SampleType { get; set; }
    }
}

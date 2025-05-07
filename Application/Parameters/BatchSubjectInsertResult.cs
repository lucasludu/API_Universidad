using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Parameters
{
    public class BatchSubjectInsertResult
    {
        public int InsertedCount { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}

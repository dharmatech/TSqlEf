using System;
using System.Collections.Generic;

#nullable disable

namespace TSqlEf
{
    public partial class Test
    {
        public Test()
        {
            Scores = new HashSet<Score>();
        }

        public string Testid { get; set; }

        public virtual ICollection<Score> Scores { get; set; }
    }
}

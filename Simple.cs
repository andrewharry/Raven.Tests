using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raven.Tests 
{
    public class Simple {
        public Simple() {
            stamp = DateTime.UtcNow.Second;
        }

        public int Id { get; set; }
        public string key { get; set; }
        public int stamp { get; set; }
    }

    public class Reduced {
        public string key { get; set; }
        public int stamp { get; set; }
    }


    public class Simple_Index : AbstractIndexCreationTask<Simple> {
        public Simple_Index() {
            Map = entities => from entity in entities
                              select new { entity.key, entity.stamp };
        }
    }
}

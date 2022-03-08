using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyEditor.Models
{
    public class Object
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<IntegerValue> IntegerValues { get; set; }
        public ICollection<StringValue> StringValues { get; set; }

        Object()
        {
            IntegerValues = new List<IntegerValue>();
            StringValues = new List<StringValue>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyEditor.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Object> Objects { get; set; }

        public ICollection<IntegerProperty> IntegerProperties { get; set; }
        public ICollection<StringProperty> StringProperties { get; set; }

        Category()
        {
            Objects = new List<Object>();

            IntegerProperties = new List<IntegerProperty>();
            StringProperties = new List<StringProperty>();
        }
    }
}

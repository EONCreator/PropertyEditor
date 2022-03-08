using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyEditor.Models
{
    #region IntegerProperties

    /// <summary>
    /// Целочисленное свойство
    /// </summary>
    public class IntegerProperty
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }

    public class IntegerValue
    {
        public int Id { get; set; }
        
        public int PropertyId { get; set; }
        public IntegerProperty Property { get; set; }

        public int Value { get; set; }
    }

    #endregion

    #region StringProperties

    /// <summary>
    /// Строковое свойство
    /// </summary>
    public class StringProperty
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int CategoryId { get; set; }
    }

    public class StringValue
    {
        public int Id { get; set; }
        
        public int PropertyId { get; set; }
        public StringProperty Property { get; set; }

        public string Value { get; set; }
    }

    #endregion
}

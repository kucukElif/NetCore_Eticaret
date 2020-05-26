using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entity.Base
{
   public interface IEntity<T>
    {
        T ID { get; set; }
    }
}

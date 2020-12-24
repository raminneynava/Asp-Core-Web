using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{

    public interface IEntity
    {

    }
    public class BaseEntity: IEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsDelete { get; set; } = false;

    }
}

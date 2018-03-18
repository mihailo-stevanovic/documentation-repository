using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    public interface IDocRepoEntity<T> : IComparable<T>, IEquatable<T>
    {
        int Id { get; set; }        
    }
}

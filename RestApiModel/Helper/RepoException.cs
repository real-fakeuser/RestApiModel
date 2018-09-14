using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestApiModel.Model;

namespace RestApiModel.Helper
{
    [Serializable]
     public class RepoException : Exception
     {
         
         public EnumResultTypes Type { get; set; }
         public RepoException(EnumResultTypes type)
         {
             Type = type;
         }
         public RepoException(string message, EnumResultTypes type) : base(message)
         {
             Type = type;
         }
         public RepoException(string message, Exception inner) : base(message, inner) { }
         protected RepoException(
             System.Runtime.Serialization.SerializationInfo info,
             System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
     }
}

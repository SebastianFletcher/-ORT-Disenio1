using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.DTO
{
    [Serializable]
    public class AuthorListItem
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public String Value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductChainManagement.ViewModels
{
    public partial class ClientChainViewModel
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Zero_Hunger.DB;

namespace Zero_Hunger.Models
{
    public class donation_Class
    {
        public List<Employee> emp { get; set; }

        public List<Donation> don { get; set; }
        public List<Collection> col { get; set; }

    }
}
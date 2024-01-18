using SoulShare_Group06.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoulShare_Group06.Map
{
    public class MapLogin
    {
        public Customer Detail(string useremail)
        {
            try
            {
                DBContextSoulShare db = new DBContextSoulShare();
                var model = db.Customers.SingleOrDefault(m => m.customer_email == useremail.ToLower());
                return model;
            }
            catch
            {
                return null;
            }
        }
    }

}
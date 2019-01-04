using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book.Models
{
    public class BookModal
    {
        public string id { get; set; }

        public string name { get; set; }

        public int? numberOfPages { get; set; }

        public int? dateOfPublication { get; set; }

        public int? createDate { get; set; }

        public int? updateDate { get; set; }

        public string[] authors { get; set; }
    }
}
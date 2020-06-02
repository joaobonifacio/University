using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.ViewModels
{
    public class TypeOfTeacherReplacementViewModel
    {
        public Teacher Teacher { get; set; }

        public string theChoice { get; set; }

        public List<string> Choice { get; set; }
    }
}
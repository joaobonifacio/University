using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.Models;

namespace University_II.ViewModels
{
    public class TeachersToReplaceViewModel
    {
        public int Id { get; set; }

        public List<Teacher> teachers { get; set; }
    }
}
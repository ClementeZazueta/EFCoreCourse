using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizLib_Model.Models;

namespace WizLib_Model.ViewModels
{
    public class BookAuthorViewModel
    {
        public Fluent_BookAuthor BookAuthor { get; set; }
        public Fluent_Book Book { get; set; }
        public IEnumerable<Fluent_BookAuthor> BookAuthorList { get; set; }
        public IEnumerable<SelectListItem> AuthorList { get; set; }
    }
}

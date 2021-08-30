using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;
using WizLib_Model.Models;

namespace WizLib_Model.ViewModels
{
    public class BookViewModel
    {
        public Fluent_Book Book { get; set; }
        public IEnumerable<SelectListItem> PublisherList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Model.Models
{
    [Table("Db_Genre")]
    public class Genre
    { 
        public int GenreId { get; set; }
        [Column("Name")]
        public string GenreName { get; set; }
        //public int DisplayOrder { get; set; }
    }
}

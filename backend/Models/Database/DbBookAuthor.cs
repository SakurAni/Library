using System.ComponentModel.DataAnnotations;

namespace SakurAni_Lib.Models.Database {

    public class DbBookAuthor {
        [Key]
        public int Id {get; set;}
        public string AuthorId { get; set; }
        public int Isbn { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace SakurAni_Lib.Models.Database {

    public class DbAuthor {
        [Key]
        public string Id {get; set;}
        public string Name {get; set;}
        public bool IsArtist {get; set;}
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WouterFennis.ChatApp.FrontEnd.ViewModels
{
    public class MessageViewModel
    {
        public long? Id { get; set; }

        [Display(Name = "Bericht")]
        [Required]
        public string Content { get; set; }

        [Display(Name = "Verzender")]
        [Required]
        public string Sender { get; set; }

        [Display(Name = "Gemaakt op")]
        public DateTime? CreatedOnUtc { get; set; }
    }
}

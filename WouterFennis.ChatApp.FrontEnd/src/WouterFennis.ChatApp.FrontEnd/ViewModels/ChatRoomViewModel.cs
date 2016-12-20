using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WouterFennis.ChatApp.FrontEnd.ViewModels
{
    public class ChatRoomViewModel
    {
        public long? Id { get; set; }

        [Display(Name = "Naam")]
        [Required]
        public string Name { get; set; }
    }
}

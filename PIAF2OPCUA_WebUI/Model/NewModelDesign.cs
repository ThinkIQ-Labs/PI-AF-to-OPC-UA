using System.ComponentModel.DataAnnotations;

namespace PIAF2OPCUA_WebUI.Pages.Model
{
    public class NewModelDesign
    {
        [Required(ErrorMessage = "Namespace Domain is required")]
        public string? mdDomain { get; set; }

        [Required(ErrorMessage = "Namespace Name is required")]
        public string? mdName { get; set; }

        public NewModelDesign()
        {

        }

    }
}

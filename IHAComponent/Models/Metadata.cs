using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHAComponent.Models
{
    public class CategoryMetadata
    {
        [Required]
        [Display(Name = "Kategori")]
        public string Name { get; set; }
    }

    public class ComponentMetaData
    {
        [Required]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Komponent")]
        public string Name { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Billed URL")]
        public string Image { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Mere info")]
        public string Info { get; set; }

        [DataType(DataType.Url)]
        [Url]
        [Display(Name = "Link til datablad")]
        public string Datasheet { get; set; }

        [DataType(DataType.Url)]
        [Url]
        [Display(Name = "Link til producent")]
        public string ManufacturerLink { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(200)]
        [Display(Name = "Kommentar")]
        public string AdminComment { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(200)]
        [Display(Name = "Brugerkommentar")]
        public string UserComment { get; set; }
        public byte[] ImageBytes { get; set; }
    }

    public class LoanInformationMetadata
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Udlånsdato")]
        public Nullable<System.DateTime> LoanDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Afleveringsdato")]
        public Nullable<System.DateTime> ReturnDate { get; set; }

        [Display(Name = "Send email")]
        public Nullable<bool> IsEmailSend { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Reservationsdato")]
        public Nullable<System.DateTime> ReservationDate { get; set; }

        [Display(Name = "Studienr.")]
        public int ReservationId { get; set; }
        public int SpecificComponentId { get; set; }
    }

    public class SpecificComponentMetadata
    {
        [Required]
        [Range(0, 1000)]
        [Display(Name = "Komponentnummer")]
        public int Number { get; set; }

        [Display(Name = "Serienr.")]
        public string SerieNr { get; set; }

        [Display(Name = "Komponent")]
        public int Component { get; set; }
    }

    public class StudentMetadata
    {
        [Required]
        [Display(Name = "Navn")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Efternavn")]
        public string Efternavn { get; set; }

        [Required]
        [Display(Name = "Studienr.")]
        public string StudentId { get; set; }

        [Display(Name = "Mobilnr.")]
        public string MobileNr { get; set; }
    }
}

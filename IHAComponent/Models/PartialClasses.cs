using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHAComponent.Models
{
    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {
    }

    [MetadataType(typeof(ComponentMetaData))]
    public partial class Component
    {
    }

    [MetadataType(typeof(LoanInformationMetadata))]
    public partial class LoanInformation
    {
    }

    [MetadataType(typeof(SpecificComponentMetadata))]
    public partial class SpecificComponent
    {
    }

    [MetadataType(typeof(StudentMetadata))]
    public partial class Student
    {
    }
}

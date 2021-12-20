using System;
using System.Collections.Generic;

namespace Academy.Core.DTO
{
    public class MemberCategoryDto : IEquatable<MemberCategoryDto>
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public List<MemberSkillDto> Skill { get; set; }

        public bool Equals(MemberCategoryDto other)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;
            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;
            //Check whether the products' properties are equal.
            return Id.Equals(other.Id) && Name.Equals(other.Name);
        }
        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.
        public override int GetHashCode()
        {
            //Get hash code for the Name field if it is not null.
            int hashProductName = Name == null ? 0 : Name.GetHashCode();
            //Get hash code for the Code field.
            int hashProductCode = Id.GetHashCode();
            //Calculate the hash code for the product.
            return hashProductName ^ hashProductCode;
        }
    }
}
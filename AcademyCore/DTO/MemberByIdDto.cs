

namespace Academy.Core.DTO
{
    public class MemberByIdDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public string Email{get; set; }
        public string Username{get; set;}
        public string Comments{get; set;}
        public string LastUpdate{get;set;}
    
    }
}
namespace StudentRecordManagementApp.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string? FullName { get; set; }
        public string? EmailAddress { get; set; }
        public string? City { get; set; }
        public DateTime CreateOn { get; set; }

        public int CreateBy { get; set; }


        public string? Response { get; set; }
        //StudentID	FullName	EmailAddress	City	CreateOn	CreatedBy
    }
}

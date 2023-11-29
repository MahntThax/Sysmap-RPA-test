namespace Sysmap_udemy_test.DatabaseModule
{
    internal class CourseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructors { get; set; }
        public string TotalHours { get; set; }

        public CourseModel(string name, string description, string instructors, string totalHours)
        {
            this.Name = name;
            this.Description = description;
            this.Instructors = instructors;
            this.TotalHours = totalHours;
        }
    }
}

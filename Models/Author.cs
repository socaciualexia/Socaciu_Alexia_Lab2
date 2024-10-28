namespace Socaciu_Alexia_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Book>? Books { get; set; }

        // Proprietate calculată pentru afișarea numelui complet
        public string FullName => $"{FirstName} {LastName}";
    }
}

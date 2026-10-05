using ASP_StudentApi.Models;

namespace ASP_StudentApi.Services
{
    public class StudentsService
    {
        private List<Student> _students;
        private int _nextId = 0;

        public StudentsService()
        {
            // LoadingData or any initialization logic:
            _students = new List<Student>
            {
                new Student { Id = 1, Name = "Oleg", Age = 20 },
                new Student { Id = 2, Name = "Anna", Age = 22 },
                new Student { Id = 3, Name = "Ivan", Age = 21 }
            };

            _nextId = _students.Max(s => s.Id) + 1;
        }

        public List<Student> GetAllStudents()
        {
            return _students;
        }

        public bool TryGetStudentByID(int id, out Student student)
        {
            student = _students.FirstOrDefault(s => s.Id == id);
            return student != null;
        }

        public int AddStudent(Student student)
        {
            student.Id = _nextId++;
            _students.Add(student);

            return student.Id;
        }

        public bool TryUpdateStudent(int id, Student updatedStudent)
        {
            var existingStudent = _students.FirstOrDefault(s => s.Id == id);

            if (existingStudent == null)
                return false;

            existingStudent.Name = updatedStudent.Name;
            existingStudent.Age = updatedStudent.Age;

            return true;
        }

        public bool TryDeleteStudent(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return false;

            _students.Remove(student);

            return true;
        }

        public List<Student> GetStudentsOlderThan(int age)
        {
            return _students.Where(s => s.Age > age).ToList();
        }
    }
}

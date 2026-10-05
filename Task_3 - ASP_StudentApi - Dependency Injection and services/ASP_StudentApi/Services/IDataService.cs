namespace ASP_StudentApi.Services
{
    // Можно выделить общий интерфейс для сервисов, которые работают с данными.
    // Это позволит легко менять реализацию сервиса, например, на работу с базой данных вместо in-memory списка.
    public interface IDataService<T>
    {
        public List<T> GetAll();
        public T GetByID(int id);

        // и т.д. - можно добавить методы для добавления, обновления и удаления данных.
    }
}

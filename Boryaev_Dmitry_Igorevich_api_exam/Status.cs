using System.ComponentModel.DataAnnotations;
namespace Boryaev_Dmitry_Igorevich_api_exam
{
    public class Status
    {
        private static int Count = 0;
        public int Id { get; private set; }
        [RegularExpression("^(Создана|Отменена|Выполнена)$")]
        public string Name { get; set; }
        public Status(string name) 
        {
            Count++;
            Id = Count;
            Name = name;
        }
    }
}
